using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Balda
{
    public class SearchData
    {
        public int X;
        public int Y;
        public bool EmptyCellUsed;
        public char?[,] Field;
        public Node Node;
        public Word Word;
        public HashSet<Word> Words;
        public HashSet<int> Visited;

        public SearchData Clone(int x, int y, bool emptyCellUsed, Node node)
        {
            bool used = EmptyCellUsed;

            var clone = new SearchData()
            {
                X = x,
                Y = y,
                EmptyCellUsed = emptyCellUsed,
                Field = !used && emptyCellUsed ? (char?[,])this.Field.Clone() : this.Field, //клонируем только тогда, когда пустая клетка будет использована
                Node = node,
                Word = (Word)this.Word.Clone(),
                Words = this.Words,
                Visited = new HashSet<int>(this.Visited)
            };

            return clone;
        }
    }


    public class AI : IPlayer
    {
        public event Action<IPlayer> OnLetterSet;
        public event Action<IPlayer> OnMoveCompleted;
        public event Action<IPlayer> OnResetMoveState;
        public event Action<IPlayer, SubState> OnError;

        public PlayerSide PlayerSide { get; set; }

        private IFieldPresenter _field;
        private IWordListProvider _wordListProvider;

        private List<Vector2Int> _dxdy;

        public bool InputLocking => true;

        public AI(IFieldPresenter field, IWordListProvider wordListProvider)
        {
            _field = field;
            _wordListProvider = wordListProvider;

            _dxdy = new List<Vector2Int>()
            {
                new Vector2Int(-1, 0),
                new Vector2Int(1, 0),
                new Vector2Int(0, 1),
                new Vector2Int(0, -1)
            };
        }

        public void Initialize()
        {
            _wordListProvider.Get(PlayerSide).Initialize();
        }

        public void Move()
        {
            Routiner.Instance.StartCoroutine(MoveRoutine());
        }

        private IEnumerator MoveRoutine()
        {
            var words = GetWords().OrderByDescending(w => w.Letters.Count).ToList();

            Debug.Log($"Number of words found: {words.Count}");

            if (words.Count == 0)
            {
                OnError?.Invoke(this, SubState.WordNotFound);
                yield break;
            }
            
            words = words.Where(w => w.Letters.Count == words.First().Letters.Count).ToList();
            var word = words[UnityEngine.Random.Range(0, words.Count)];

            Debug.Log($"AI Answer: {word} ({word.Letters.Count})");

            var letter = word.Letters.FirstOrDefault(l => l.IsNew);

            _field.SetChar(letter.Pos, letter.Char, true);

            yield return new WaitForSeconds(Constants.AI.NEW_LETTER_SHOW_DURATION);

            OnLetterSet?.Invoke(this);

            yield return Routiner.Instance.StartCoroutine(_field.ShowWord(word, Constants.AI.WORD_LETTER_SHOW_DURATION, () =>
            {
                _wordListProvider.Get(PlayerSide).AddWord(word.ToString());
                OnMoveCompleted?.Invoke(this);
            }));
        }

        public HashSet<Word> GetWords()
        {
            var words = new HashSet<Word>();
            var allData = new List<SearchData>();

            int size = _field.GetModel().GetSize();
            var trie = _field.GetModel().GetTrie();
            char?[,] field = _field.GetModel().GetField();

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (field[x, y] != null)
                    {
                        var @char = field[x, y].Value;

                        if (trie.Root.Children.ContainsKey(@char))
                        {
                            var data = new SearchData()
                            {
                                X = x,
                                Y = y,
                                Field = (char?[,])field.Clone(),
                                EmptyCellUsed = false,
                                Node = trie.Root.Children[@char],
                                Word = new Word(),
                                Words = words,
                                Visited = new HashSet<int>()
                            };

                            allData.Add(data);
                        }

                        for (int i = 0; i < _dxdy.Count; i++)
                        {
                            int dx = x + _dxdy[i].x;
                            int dy = y + _dxdy[i].y;

                            if (InBounds(dx, dy))
                            {
                                if (field[dx, dy] == null)
                                {
                                    foreach (var kvp in trie.Root.Children)
                                    {
                                        var data = new SearchData()
                                        {
                                            X = dx,
                                            Y = dy,
                                            Field = (char?[,])field.Clone(),
                                            EmptyCellUsed = true,
                                            Node = kvp.Value,
                                            Word = new Word(),
                                            Words = words,
                                            Visited = new HashSet<int>()
                                        };

                                        allData.Add(data);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            float startTime = Time.realtimeSinceStartup;

            #if UNITY_WEBGL
            foreach (var data in allData)
            {
                DFS(data);
            }        
            #else
            Parallel.ForEach(allData, data =>
            {
                DFS(data);
            });
            #endif

            float secs = Time.realtimeSinceStartup - startTime;
            Debug.Log($"Search duration: {secs} secs");

            foreach (string word in _field.GetModel().ExcludedWords)
            {
                words.RemoveWhere(w => w.ToString() == word);
            }

            return words;
        }

        private bool IsSubWord(Word word)
        {
            var field = _field.GetModel().GetField();

            foreach (var letter in word.Letters)
            {
                if (field[letter.Pos.x, letter.Pos.y] == null)
                {
                    return false;
                }
            }

            return true;
        }

        private void DFS(SearchData data)
        {
            int x = data.X;
            int y = data.Y;
            char?[,] field = _field.GetModel().GetField();

            bool isNew = data.Field[x, y] == null;
            data.Field[x, y] = data.Node.Letter;
            data.Word.Letters.Add(new Letter(data.Node.Letter, new Vector2Int(x, y), isNew));
            data.Visited.Add(CoordToIndex(x, y));

            if (data.Node.IsTerminal)
            {
                data.Word.Cache();

                if (!data.Words.Contains(data.Word))
                {
                    if (!IsSubWord(data.Word))
                    {
                        lock (data.Words)
                        {
                            data.Words.Add(data.Word);
                        }
                    }
                }
            }

            for (int i = 0; i < _dxdy.Count; i++)
            {
                int dx = x + _dxdy[i].x;
                int dy = y + _dxdy[i].y;

                if (InBounds(dx, dy))
                {
                    if (data.Visited.Contains(CoordToIndex(dx, dy)))
                    {
                        continue;
                    }

                    if (field[dx, dy] != null)
                    {
                        var @char = field[dx, dy].Value;

                        if (data.Node.Children.ContainsKey(@char))
                        {
                            var clone = data.Clone(dx, dy, data.EmptyCellUsed, data.Node.Children[@char]);
                            DFS(clone);
                        }
                    }
                    else if (!data.EmptyCellUsed)
                    {
                        foreach (var kvp in data.Node.Children)
                        {
                            var clone = data.Clone(dx, dy, true, kvp.Value);
                            DFS(clone);
                        }
                    }
                }
            }
        }

        private int CoordToIndex(int x, int y)
        {
            return y * _field.GetModel().GetSize() + x;
        }

        private bool InBounds(int x, int y)
        {
            int size = _field.GetModel().GetSize();
            return x >= 0 && y >= 0 && x < size && y < size;
        }

        public void Dispose()
        {
            
        }
    }
}

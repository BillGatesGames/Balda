using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField]
        private FieldView _fieldView;

        [SerializeField]
        private AlphabetView _alphabetView;

        [SerializeField]
        private MessageView _messageView;

        [SerializeField]
        private WordListView _wordListLeftView;

        [SerializeField]
        private WordListView _wordListRightView;

        [Header("Debug")]
        [SerializeField]
        private bool _firstPlayerIsHuman;

        void Start()
        {
            var field = new FieldPresenter(new FieldModel(), _fieldView);
            var alphabet = new AlphabetPresenter(new AlphabetModel(), _alphabetView);
            var message = new MessagePresenter(new MessageModel(), _messageView);

            var wordList1 = new WordListPresenter(new WordListModel(), _wordListLeftView);
            var wordList2 = new WordListPresenter(new WordListModel(), _wordListRightView);

            var stateMachineModel = new StateMachineModel();

            IPlayer player1 = null;

            if (_firstPlayerIsHuman)
            {
                player1 = new Human(field, alphabet, message, wordList1, () =>
                {
                    return (stateMachineModel.State, stateMachineModel.SubState);
                });
            }
            else
            {
                player1 = new AI(field, wordList1);
            }

            var player2 = new AI(field, wordList2);

            var stateMachine = new StateMachinePresenter(player1, player2, field, message, stateMachineModel);
        }
    }
}

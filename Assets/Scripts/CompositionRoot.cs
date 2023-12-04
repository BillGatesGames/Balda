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
        private MessageView _popupView;

        [SerializeField]
        private WordListView _wordListLeftView;

        [SerializeField]
        private WordListView _wordListRightView;

        [Header("Debug")]
        [SerializeField]
        private PlayerType _player1;

        [SerializeField]
        private PlayerType _player2;

        private IFieldPresenter _field;
        private IAlphabetPresenter _alphabet;
        private IMessagePresenter _message;
        private IMessagePresenter _popup;
        private IWordListPresenter _wordList1;
        private IWordListPresenter _wordList2;
        private IStateMachinePresenter _stateMachine;

        private IStateMachineModel _stateMachineModel;

        private IPlayer CreatePlayer(PlayerType type, IWordListPresenter wordList)
        {
            if (type == PlayerType.Human)
            {
                return new Human(_field, _alphabet, _message, wordList, () =>
                {
                    return (_stateMachineModel.State, _stateMachineModel.SubState);
                });
            }
            
            return new AI(_field, wordList);
        }

        public void Build()
        {
            _field = new FieldPresenter(new FieldModel(), _fieldView);
            _alphabet = new AlphabetPresenter(new AlphabetModel(), _alphabetView);
            _message = new MessagePresenter(new MessageModel(), _messageView);
            _popup = new PopupMessagePresenter(new MessageModel(), _popupView);

            _wordList1 = new WordListPresenter(new WordListModel(), _wordListLeftView);
            _wordList2 = new WordListPresenter(new WordListModel(), _wordListRightView);

            _stateMachineModel = new StateMachineModel();

            var player1 = CreatePlayer(_player1, _wordList1);
            var player2 = CreatePlayer(_player2, _wordList2);

            _stateMachine = new StateMachinePresenter(player1, player2, _field, _popup, _stateMachineModel);
        }
    }
}

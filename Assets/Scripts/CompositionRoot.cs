using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        private bool _firstPlayerIsHuman;

        public void Build()
        {
            #if !UNITY_EDITOR
             _firstPlayerIsHuman = false;
            #endif

            var field = new FieldPresenter(new FieldModel(), _fieldView);
            var alphabet = new AlphabetPresenter(new AlphabetModel(), _alphabetView);
            var message = new MessagePresenter(new MessageModel(), _messageView);
            var popup = new PopupMessagePresenter(new MessageModel(), _popupView);

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
            /*
            var player2 = new Human(field, alphabet, message, wordList2, () =>
            {
                return (stateMachineModel.State, stateMachineModel.SubState);
            });
            */
            var player2 = new AI(field, wordList2);

            var stateMachine = new StateMachinePresenter(player1, player2, field, popup, stateMachineModel);
        }
    }
}

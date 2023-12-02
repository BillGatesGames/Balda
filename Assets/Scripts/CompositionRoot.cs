using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        var field = new FieldPresenter(new FieldModel(), _fieldView);
        var alphabet = new AlphabetPresenter(new AlphabetModel(), _alphabetView);
        var message = new MessagePresenter(new MessageModel(), _messageView);

        var wordList1 = new WordListPresenter(new WordListModel(), _wordListLeftView);
        var wordList2 = new WordListPresenter(new WordListModel(), _wordListRightView);

        var stateMachineModel = new StateMachineModel();
    
        /*
        var player1 = new Human(field, alphabet, message, wordList1, () =>
        {
            return (stateMachineModel.State, stateMachineModel.SubState);
        });
        */

        var player1 = new AI(field, wordList1);
        var player2 = new AI(field, wordList2);

        var stateMachinePresenter = new StateMachinePresenter(player1, player2, field, stateMachineModel);
    }
}

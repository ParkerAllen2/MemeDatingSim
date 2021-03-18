using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogUIController))]
public class DialogManager : MonoBehaviour
{
    DialogUIController dialogUI;
    Dialog dialog, nextDialog;
    bool hasNextDialog;


    Queue<Sentence> sentences;
    Sentence currentSentence;

    private void Awake()
    {
        dialogUI = GetComponent<DialogUIController>(); 
        sentences = new Queue<Sentence>();
    }

    public void StartDialog(Dialog d)
    {
        dialogUI.ClearButtons();
        dialog = d;
        nextDialog = null;
        hasNextDialog = false;

        if (dialog == null)
        {
            return;
        }

        dialogUI.PlaceCharacter(dialog.character, dialog.characterPosition);

        foreach (Sentence s in dialog.sentences)
        {
            sentences.Enqueue(s);
        }
        DisplayDialog();
    }

    public void DisplayDialog()
    {
        if (dialogUI.isTyping)
        {
            dialogUI.CompleteSentence();
        }
        else if(sentences.Count == 0)
        {
            EndDialog();
        }
        else
        {
            dialogUI.StartTypingSentence(sentences.Dequeue());
        }
    }

    public void EndDialog()
    {
        if (hasNextDialog)
        {
            StartDialog(nextDialog);
        }
        else if(dialog.options.Length == 0)
        {
            print("end");
        }
        else if(dialog.options.Length == 1)
        {
            StartDialog(dialog.options[0].nextDialog);
        }
        else
        {
            dialogUI.CreateButtons(dialog.options);
        }
    }

    public void SelectOption(int i)
    {
        dialog.character.affection += dialog.options[i].affectionGiven;
        foreach (Sentence s in dialog.options[i].response)
        {
            sentences.Enqueue(s);
        }

        nextDialog = dialog.options[i].nextDialog;
        hasNextDialog = true;

        DisplayDialog();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    DialogUIController dialogUI;
    Dialog dialog;
    Queue<Sentence> sentences;
    Sentence currentSentence;

    public void StartDialog(Dialog d)
    {
        dialogUI.ClearButtons();
        dialog = d;

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
            dialogUI.CompleteSentence(currentSentence);
        }
        else if(sentences.Count == 0)
        {

        }
        else
        {
            dialogUI.StartTypingSentence(sentences.Dequeue());
        }
    }

    public void EndDialog()
    {
        if(dialog.options.Length == 0)
        {

        }
        else if(dialog.options.Length == 1)
        {

        }
        else
        {
            dialogUI.CreateButtons(dialog.options);
        }
    }

    public void SelectOption(int i)
    {
        foreach (Sentence s in dialog.options[i].response)
        {
            sentences.Enqueue(s);
        }

        StartDialog(dialog.options[i].nextDialog);
    }
}

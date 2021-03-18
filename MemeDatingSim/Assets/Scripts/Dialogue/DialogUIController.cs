using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogUIController : MonoBehaviour
{
    public float typingSpeed = .02f;
    [HideInInspector] public bool isTyping;

    public void PlaceCharacter(Character character, int position)
    {

    }

    public void CompleteSentence(Sentence sentence)
    {
        StopAllCoroutines();
        isTyping = false;
    }

    public void StartTypingSentence(Sentence sentence)
    {
        StartCoroutine(TypeSentence(sentence.sentence));
        isTyping = true;
    }

    IEnumerator TypeSentence(string sentence)
    {

        yield return null;
        isTyping = false;
    }

    public void CreateButtons(Option[] options)
    {

    }

    public void ClearButtons()
    {

    }
}

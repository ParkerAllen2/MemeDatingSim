using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScriptReader : MonoBehaviour
{
    public Commands shortcuts;

    UIController uiController;

    TextAsset script;
    Queue<string> line;
    int currentline;

    private void Awake()
    {
        uiController = GetComponent<UIController>();
        line = new Queue<string>();
    }

    public void StartScript(TextAsset s)
    {
        script = s;
        ReadNextLine();
        ReadNextWord();
    }

    void ReadNextLine()
    {
        currentline++;
    }

    public void ReadNextWord()
    {
        if(line.Count == 0)
        {
            ReadNextLine();
            return;
        }

        string word = line.Dequeue();

        if (prefixCheck(ref word))
        {
            uiController.StartTyping(word);
        }

        Character c;
        if(Overlord.Instance.HasCharacter(word, out c))
        {
            uiController.Speaker = c;
        }

        if (word.Equals(shortcuts.expression))
        {

        }
        else if (word.Equals(shortcuts.option))
        {

        }
        else if (word.Equals(shortcuts.changeAffection))
        {

        }
        else if (word.Equals(shortcuts.skipTo))
        {

        }
        else if (word.Equals(shortcuts.changeBackground))
        {

        }
        else if (word.Equals(shortcuts.nextScene))
        {

        }
    }

    bool prefixCheck(ref string word)
    {
        if (word.StartsWith(shortcuts.prefix))
        {
            word = word.Substring(shortcuts.prefix.Length);
            return false;
        }
        return true;
    }

    void commandExpression()
    {

    }

    void commandOption()
    {

    }

    void commandChangeAffection()
    {

    }

    void commandSkipTo()
    {

    }

    void commandChangeBackground()
    {

    }

    void commandNextScene()
    {

    }

    public void ScriptError(string word, string message)
    {
        Debug.LogError(message + "\n on line " + currentline + " at " + word);
    }
}

public class Response
{
    string reply;
    int affection;
    string skipTo;
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScriptReader : MonoBehaviour
{
    public Commands shortcuts;
    [HideInInspector] public bool wait;

    UIController uiController;

    TextAsset script;
    Queue<string> line;
    string word;
    int currentline;

    List<Response> responses;

    private void Awake()
    {
        uiController = GetComponent<UIController>();
        line = new Queue<string>();
        responses = new List<Response>();
    }

    public void StartScript(TextAsset s)
    {
        script = s;
        ReadNextLine();
        TypeNextWord();
    }

    void ReadNextLine()
    {
        wait = true;
        currentline++;
    }

    public void TypeNextWord()
    {
        if (wait)
        {
            return;
        }
        uiController.StartTyping(ReadWord());
    }

    public void ReadArray(string[] arr)
    {
        foreach(string s in arr)
        {
            ReadWord(s);
        }
    }

    public string ReadWord()
    {
        if (line.Count == 0)
        {
            ReadNextLine();
            return "";
        }
        return ReadWord(line.Dequeue());
    }

    public string ReadWord(string w)
    {
        word = w;

        if (prefixCheck(ref word))
        {
            return word;
        }

        Character c;
        if(Overlord.Instance.HasCharacter(word, out c))
        {
            uiController.Speaker = c;
        }

        if (word.Equals(shortcuts.expression))
        {
            commandExpression();
        }
        else if (word.Equals(shortcuts.usePlayerName))
        {
            return Overlord.Instance.playerName;
        }
        else if (word.Equals(shortcuts.option))
        {
            commandOption();
        }
        else if (word.Equals(shortcuts.changeAffection))
        {
            commandChangeAffection();
        }
        else if (word.Equals(shortcuts.skipLines))
        {
            commandSkipLines();
        }
        else if (word.Equals(shortcuts.changeBackground))
        {
            commandChangeBackground();
        }
        else if (word.Equals(shortcuts.nextScene))
        {
            commandNextScene();
        }

        return ReadWord();
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
        string[] par = GetParameters(1);
        uiController.ChangeExpression(ConvertToInt(par[0]));
    }

    void commandOption()
    {
        Response rep = new Response(line, shortcuts.prefix);
        wait = true;
        responses.Add(rep);

        ReadNextLine();
        if(line.Peek().Equals(shortcuts.prefix + shortcuts.option))
        {
            ReadWord();
            return;
        }

        uiController.CreateButton(responses.ToArray());
        responses.Clear();
    }

    void commandChangeAffection()
    {
        string[] par = GetParameters(1);
        uiController.ChangeAffection(ConvertToInt(par[0]));
    }

    void commandSkipLines()
    {
        string[] par = GetParameters(1);
        int num = ConvertToInt(par[0]);
        for(int i = 0; i < num; i++)
        {
            ReadNextLine();
        }
        wait = false;
    }

    void commandChangeBackground()
    {
        string[] par = GetParameters(1);
        int num = ConvertToInt(par[0]);
        Debug.Log("Feature Not Implemented");
    }

    void commandNextScene()
    {
        string[] par = GetParameters(1);
        Overlord.Instance.LoadScene(par[0]);
    }

    string[] GetParameters(int x)
    {
        if (line.Count >= x)
        {
            ScriptError("Missing aurgument");
            return null;
        }
        string[] rtn = new string[x];
        for(int i = 0; i < x; i++)
        {
            rtn[i] = line.Dequeue();
        }
        return rtn;
    }

    int ConvertToInt(string s)
    {
        int x = 0;
        if(!int.TryParse(s, out x))
        {
            ScriptError("Requires integer, not \"" + s + "\"");
        }
        return x;
    }

    float ConvertToFloat(string s)
    {
        float x = 0;
        if (!float.TryParse(s, out x))
        {
            ScriptError("Requires float, not \"" + s + "\"");
        }
        return x;
    }

    public void ScriptError(string message)
    {
        Debug.LogError(message + "\n on line " + currentline + " at " + word);
    }
}

public class Response
{
    public string reply;
    public List<string> commands;

    public Response(Queue<string> line, string prefix)
    {
        commands = new List<string>();
        foreach(string s in line)
        {
            if (s.StartsWith(prefix))
            {
                commands.Add(s);
                continue;
            }
            reply += s;
        }
    }
}

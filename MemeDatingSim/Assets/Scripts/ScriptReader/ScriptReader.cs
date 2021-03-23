using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class ScriptReader : MonoBehaviour
{
    public Commands shortcuts;
    [HideInInspector] public bool wait;

    UIController uiController;

    public string[] lines;
    public Queue<string> line;
    string word;
    public int currentline;

    List<Response> responses;

    private void Awake()
    {
        uiController = GetComponent<UIController>();
        line = new Queue<string>();
        responses = new List<Response>();
    }

    public void StartScript(TextAsset script)
    {
        lines = Regex.Split(script.text, "\n");
        uiController.ClickTextBox();
    }

    public void ReadNextLine()
    {
        if (currentline >= lines.Length)
        {
            print("Finish");
            return;
        }
        line.Clear();
        foreach(string s in lines[currentline].Split(' '))
        {
            line.Enqueue(s);
        }
        currentline++;
        wait = false;
    }

    public void ReadArray(string[] arr)
    {
        foreach (string s in arr)
        {
            line.Enqueue(s);
        }
        wait = false;
        while (TypeNextWord()) { }
        uiController.ClickTextBox();
        uiController.ClearButtons();
    }

    //return false if sentence done
    public bool TypeNextWord()
    {
        if (wait)
        {
            return false;
        }
        uiController.StartTyping(ReadWord());
        return true;
    }

    public string ReadWord()
    {
        if (line.Count == 0)
        {
            wait = true;
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
        else if (word.StartsWith(shortcuts.usePlayerName))
        {
            return Overlord.Instance.playerName + 
                word.Substring(shortcuts.usePlayerName.Length);
        }
        else if (word.Equals(shortcuts.option))
        {
            commandOption();
            return "";
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
        else if (word.Equals(shortcuts.position))
        {
            commandPosition();
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
        responses.Add(rep);

        if(lines[currentline].StartsWith(shortcuts.prefix + shortcuts.option))
        {
            ReadNextLine();
            ReadWord();
            return;
        }
        wait = true;
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
        currentline += num;
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

    void commandPosition()
    {
        string[] par = GetParameters(1);
        uiController.MoveCharacter(ConvertToInt(par[0]));
    }

    string[] GetParameters(int x)
    {
        if (line.Count < x)
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
        Debug.LogError(message + "\n on line " + (currentline - 1) + " at " + word);
    }
}

public class Response
{
    public string reply;
    public List<string> commands;

    public Response(Queue<string> line, string prefix)
    {
        commands = new List<string>();
        while(line.Count > 0)
        {
            string s = line.Dequeue();
            if (s.StartsWith(prefix))
            {
                commands.Add(s);
                commands.Add(line.Dequeue());
                continue;
            }
            reply += s;
        }
    }
}

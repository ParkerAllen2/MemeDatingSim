using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ScriptReader : MonoBehaviour
{
    public CommandList shortcuts;
    UIController uiController;
    CommandController command;
    [HideInInspector] public bool wait;


    string[] lines;
    Queue<string> line;
    int currentline;

    private void Awake()
    {
        uiController = GetComponentInChildren<UIController>();
        command = new CommandController(shortcuts, this, uiController);
        line = new Queue<string>();
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
        if (string.IsNullOrWhiteSpace(lines[currentline]))
        {
            currentline++;
            ReadNextLine();
            return;
        }

        EnqueueLine(lines[currentline].Split(null));
        currentline++;
    }

    public void ReadArray(string[] arr)
    {
        EnqueueLine(arr);
        while (TypeNextWord()) { }
        uiController.ClickTextBox();
        uiController.ClearButtons();
    }

    void EnqueueLine(string[] arr)
    {
        line.Clear();
        foreach (string s in arr)
        {
            line.Enqueue(s);
        }
        wait = false;
    }

    //return false if sentence done
    public bool TypeNextWord()
    {
        if (wait || line.Count == 0)
        {
            wait = true;
            return false;
        }
        string type;
        if(ReadWord(line.Dequeue(), out type))
        {
            uiController.StartTyping(type);
            return true;
        }

        return TypeNextWord();
    }

    public bool ReadWord(string word, out string rtn)
    {
        rtn = "";
        if (prefixCheck(ref word))
        {
            rtn = word + " ";
            return true;
        }

        Character c;
        if (Overlord.Instance.HasCharacter(word, out c))
        {
            uiController.Speaker = c;
            return TypeNextWord();
        }
        if (Overlord.Instance.GetPlayer(word, out rtn)) 
        {
            return true;
        }

        command.Command(word);
        return TypeNextWord();
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

    public bool StartingShortcutCheck(string s)
    {
        return lines[currentline].StartsWith(shortcuts.prefix + s);
    }

    public string[] GetParameters(int x)
    {
        if (line.Count < x)
        {
            ScriptError("Missing aurgument");
            return null;
        }
        string[] rtn = new string[x];
        for (int i = 0; i < x; i++)
        {
            rtn[i] = line.Dequeue();
        }
        return rtn;
    }

    public int ConvertToInt(string s)
    {
        int x = 0;
        if (!int.TryParse(s, out x))
        {
            ScriptError("Requires integer, not \"" + s + "\"");
        }
        return x;
    }

    public float ConvertToFloat(string s)
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
        Debug.LogError(message + "\n on line " + (currentline - 1) + " at " + line.Peek());
    }

    public Response GetResponse()
    {
        return new Response(line, shortcuts.prefix);
    }

    public int CurrentLine
    {
        get { return currentline; }
        set { currentline = value; }
    }
}

public class Response
{
    public string reply;
    public List<string> commands;

    public Response(Queue<string> line, string prefix)
    {
        commands = new List<string>();
        while (line.Count > 0)
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


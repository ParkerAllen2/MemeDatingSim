using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController
{
    Dictionary<string, Command> commands = new Dictionary<string, Command>();
    ScriptReader sr;
    UIController uic;

    public CommandController (CommandList cs, ScriptReader sr, UIController uic)
    {
        this.sr = sr;
        this.uic = uic;
        foreach(Command c in cs.commands)
        {
            commands.Add(c.shortcut, c);
        }
    }

    public void Command(string cmd)
    {
        Debug.Log(cmd);
        commands[cmd].Action(sr, uic);
    }
}

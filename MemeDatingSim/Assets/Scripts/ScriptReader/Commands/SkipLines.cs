using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skip Lines", menuName = "Command/Skip Lines")]
public class SkipLines : Command
{
    public override void Action(ScriptReader sr, UIController uic)
    {
        string[] par = sr.GetParameters(1);
        int counter = sr.ConvertToInt(par[0]);
        int i = 0;
        while (i < counter)
        {
            if (!string.IsNullOrWhiteSpace(sr.GetLine(sr.CurrentLine)))
            {
                i++;
            }
            sr.CurrentLine++;
        }
    }
}

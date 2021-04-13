using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Option", menuName = "Command/Option")]
public class Option : Command
{
    List<Response> responses = new List<Response>();

    public override void Action(ScriptReader sr, UIController uic)
    {
        Response rep = sr.GetResponse();
        responses.Insert(0, rep);

        if (sr.StartingShortcutCheck(shortcut))
        {
            sr.ReadNextLine();
            sr.TypeNextWord();
            return;
        }
        uic.CreateButton(responses.ToArray());
        responses.Clear();
    }
}

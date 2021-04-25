using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "If Affection Greater Than", menuName = "Command/If Affection Greater Than")]
public class IfAffectionGreaterThan : Command
{
    public override void Action(ScriptReader sr, UIController uic)
    {
        int amount = sr.ConvertToInt(sr.GetParameters(1)[0]);
        if (uic.Speaker.affection >= amount)
        {
            sr.ReadNextLine();
        }
    }
}

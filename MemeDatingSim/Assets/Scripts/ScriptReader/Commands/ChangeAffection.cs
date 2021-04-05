using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Affection", menuName = "Command/Change Affection")]
public class ChangeAffection : Command
{
    public override void Action(ScriptReader sr, UIController uic)
    {
        string[] par = sr.GetParameters(1);
        uic.Speaker.affection += sr.ConvertToInt(par[0]);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Affection", menuName = "Command/Change Affection")]
public class ChangeAffection : Command
{
    public override void Action(ScriptReader sr, UIController uic)
    {
        string[] par = sr.GetParameters(1);
        uic.ChangeAffection(sr.ConvertToInt(par[0]));
    }
}

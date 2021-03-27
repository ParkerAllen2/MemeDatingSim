using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Expression", menuName = "Command/Change Expression")]
public class ChangeExpression : Command
{
    public override void Action(ScriptReader sr, UIController uic)
    {
        string[] par = sr.GetParameters(1);
        uic.ChangeExpression(sr.ConvertToInt(par[0]));
    }
}

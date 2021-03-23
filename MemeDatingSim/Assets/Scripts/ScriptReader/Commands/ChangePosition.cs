using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Position", menuName = "Command/Change Position")]
public class ChangePosition : Command
{
    public override void Action(ScriptReader sr, UIController uic)
    {
        string[] par = sr.GetParameters(1);
        uic.MoveCharacter(sr.ConvertToInt(par[0]));
    }
}

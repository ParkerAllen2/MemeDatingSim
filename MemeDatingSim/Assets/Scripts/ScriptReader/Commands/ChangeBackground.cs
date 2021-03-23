using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Background", menuName = "Command/Change Background")]
public class ChangeBackground : Command
{
    public override void Action(ScriptReader sr, UIController uic)
    {
        string[] par = sr.GetParameters(1);
        int num = sr.ConvertToInt(par[0]);
        Debug.Log("Feature Not Implemented");
    }
}

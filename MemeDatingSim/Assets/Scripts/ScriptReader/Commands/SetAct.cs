using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Set Act", menuName = "Command/Set Act")]
public class SetAct : Command
{
    [Header("Takes Integer Parameter")]
    [Header("The next act is selected from the current Act's nextAct array.")]
    [Header("Sets the act of the current speaker")]
    public bool useless;

    public override void Action(ScriptReader sr, UIController uic)
    {
        int nextAct = sr.ConvertToInt(sr.GetParameters(1)[0]);
        Act a = Overlord.Instance.currentAct.nextActs[nextAct];
        uic.Speaker.act = a;
    }
}

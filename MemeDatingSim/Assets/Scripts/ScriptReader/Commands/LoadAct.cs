using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Load Act", menuName = "Command/Load Act")]
public class LoadAct : Command
{
    public override void Action(ScriptReader sr, UIController uic)
    {
        Overlord.Instance.LoadScene(uic.GetSpeaker());
    }
}

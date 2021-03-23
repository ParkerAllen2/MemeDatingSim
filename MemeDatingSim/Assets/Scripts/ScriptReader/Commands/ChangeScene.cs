using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scene", menuName = "Command/Change Scene")]
public class ChangeScene : Command
{
    public override void Action(ScriptReader sr, UIController uic)
    {
        string[] par = sr.GetParameters(1);
        Overlord.Instance.LoadScene(par[0]);
    }
}

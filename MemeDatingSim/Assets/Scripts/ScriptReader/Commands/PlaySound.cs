using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Play Sound", menuName = "Command/Play Sound")]
public class PlaySound : Command
{
    public override void Action(ScriptReader sr, UIController uic)
    {
        string[] par = sr.GetParameters(1);
        if (!SoundManager.Instance.Play(par[0]))
        {
            sr.ScriptError("Sound " + par[0] + " Does Not Exists");
        }
    }
}

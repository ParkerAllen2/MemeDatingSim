using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Play Theme", menuName = "Command/Play Theme")]
public class PlayThemeMusic : Command
{
    public float duration;
    public override void Action(ScriptReader sr, UIController uic)
    {
        string[] par = sr.GetParameters(1);
        if (!SoundManager.Instance.StartPlayingTheme(par[0], duration))
        {
            sr.ScriptError("Sound " + par[0] + " Does Not Exists");
        }
    }
}

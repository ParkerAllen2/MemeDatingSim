using UnityEngine;

[CreateAssetMenu(fileName = "Background", menuName = "Command/Change Background")]
public class ChangeBackground : Command
{
    public override void Action(ScriptReader sr, UIController uic)
    {
        string[] par = sr.GetParameters(1);
        uic.ChangeBackground(par[0]);
    }
}

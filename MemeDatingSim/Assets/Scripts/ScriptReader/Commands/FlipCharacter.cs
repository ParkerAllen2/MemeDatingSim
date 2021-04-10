using UnityEngine;

[CreateAssetMenu(fileName = "Flip Character", menuName = "Command/Flip Character")]
public class FlipCharacter : Command
{
    public override void Action(ScriptReader sr, UIController uic)
    {
        Transform t = uic.GetImageOfCharacter(uic.Speaker.characterName).transform;
        float y = (t.rotation.y == 0) ? 180 : 0;
        t.rotation = Quaternion.Euler(0, y, 0);
    }
}

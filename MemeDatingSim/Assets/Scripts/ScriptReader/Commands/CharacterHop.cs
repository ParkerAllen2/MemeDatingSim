using UnityEngine;

[CreateAssetMenu(fileName = "Character Hop", menuName = "Command/Character Hop")]
public class CharacterHop : Command
{
    public float gravity;
    public float jumpForce;

    public override void Action(ScriptReader sr, UIController uic)
    {
        uic.StartCharacterHop(gravity, jumpForce);
    }
}

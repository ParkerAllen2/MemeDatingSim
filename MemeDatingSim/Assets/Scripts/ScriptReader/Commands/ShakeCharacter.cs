using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shake Character", menuName = "Command/Shake Character")]
public class ShakeCharacter : Command
{
    public float magnitude;
    public float duration;

    public override void Action(ScriptReader sr, UIController uic)
    {
        uic.StartShakeCharacter(magnitude, duration);
    }


}

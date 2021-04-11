using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Affection", menuName = "Command/Change Affection")]
public class ChangeAffection : Command
{
    public override void Action(ScriptReader sr, UIController uic)
    {
        int amount = sr.ConvertToInt(sr.GetParameters(1)[0]);
        uic.Speaker.affection += amount;

        amount = (amount > 0) ? 1 : 0;
        ParticleSystem ps = uic.AffectionParticles[amount];
        ps.Play();
        Vector3 pos = Camera.main.ScreenToWorldPoint(uic.GetImageOfCharacter().rectTransform.position);
        pos.z = 0;
        ps.transform.position = pos;
    }
}

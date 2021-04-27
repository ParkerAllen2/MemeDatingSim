using UnityEngine;

public class PlaySoundButton : MonoBehaviour
{
    public void PlaySound()
    {
        SoundManager.Instance.Play("button");
    }
}

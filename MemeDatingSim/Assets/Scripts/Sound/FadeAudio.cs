using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAudio : MonoBehaviour
{
    public void StartFading(Sound sound, float duration, bool fadeIn = false)
    {
        float to = 0;
        float from = SoundManager.Instance.GetMaxVolume(sound); ;
        if (fadeIn)
        {
            to = from;
            from = 0;
        }
            
        StartCoroutine(Fade(sound, from, to, duration));
    }

    IEnumerator Fade(Sound sound, float from, float to, float duration)
    {
        float elapsed = 0;
        while(elapsed < duration)
        {
            sound.source.volume = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
            elapsed += Time.deltaTime;
        }
        sound.source.volume = SoundManager.Instance.GetMaxVolume(sound);
        if (to == 0)
            sound.source.Stop();
    }
}

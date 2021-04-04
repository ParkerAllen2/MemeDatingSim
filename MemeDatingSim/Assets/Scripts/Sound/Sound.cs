using UnityEngine;

[CreateAssetMenu]
public class Sound : ScriptableObject
{
    public string soundName;
    public string type;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float maxVolume = 1f;

    [Range(.1f, 3f)]
    public float pitch = 1f;

    public bool isLoop;
    public bool hasCooldown;
    public AudioSource source;
}

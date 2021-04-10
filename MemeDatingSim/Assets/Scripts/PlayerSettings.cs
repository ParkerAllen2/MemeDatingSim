using UnityEngine;

[CreateAssetMenu]
public class PlayerSettings : ScriptableObject
{
    public string playerName;
    public string shortcut;

    [Range(0f, .3f)]
    public float textSpeed = .1f;
    [Space(10)]

    [Range(0f, 1f)]
    public float master = .5f;
    [Range(0f, 1f)]
    public float music = 1;
    [Range(0f, 1f)]
    public float sfx = 1;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerSettings : ScriptableObject
{
    public string playerName;
    public string shortcut;

    [Range(0f, .5f)]
    public float textSpeed = .5f;
    [Space(10)]

    [Range(0f, 1f)]
    public float music = 1;
    [Range(0f, 1f)]
    public float sfx = 1;
    [Range(0f, 1f)]
    public float master = 1;
}

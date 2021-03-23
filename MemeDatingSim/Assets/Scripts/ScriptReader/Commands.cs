using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Commands : ScriptableObject
{
    public string prefix;
    public string[] characters;
    public string expression;
    public string position;
    public string usePlayerName;
    [Space(10)]
    public string option;
    public string changeAffection;
    public string skipLines;
    [Space(10)]
    public string changeBackground;
    public string nextScene;
}

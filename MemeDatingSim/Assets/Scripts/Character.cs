using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ScriptableObject
{
    public string characterName;
    public int affection;
    public Sprite[] expressions;
    [HideInInspector] public Act nextAct;
}

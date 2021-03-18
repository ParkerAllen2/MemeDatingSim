using UnityEngine;

[CreateAssetMenu]
public class Character : ScriptableObject
{
    public string characterName;
    public int affection;
    public Sprite[] expressions;
    public Act act;
}

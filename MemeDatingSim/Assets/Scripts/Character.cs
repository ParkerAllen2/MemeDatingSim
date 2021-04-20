using UnityEngine;

[CreateAssetMenu]
public class Character : ScriptableObject
{
    public string characterName;
    public string shortcut;
    public int affection;
    public Sprite[] expressions;
    public Act act;
    public Font font;
    public Color[] textboxColors;
}

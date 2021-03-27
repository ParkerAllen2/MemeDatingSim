using UnityEngine;

[CreateAssetMenu]
public class Act : ScriptableObject
{
    public string location;
    public Act[] nextActs;
    public TextAsset script;
}

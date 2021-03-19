using System.IO;
using UnityEngine;

[CreateAssetMenu]
public class Act : ScriptableObject
{
    public Act nextAct;
    public Dialog firstDialog;
    public TextAsset script;
}

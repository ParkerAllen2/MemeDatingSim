using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CommandList : ScriptableObject
{
    public string prefix;
    public List<Command> commands;
}

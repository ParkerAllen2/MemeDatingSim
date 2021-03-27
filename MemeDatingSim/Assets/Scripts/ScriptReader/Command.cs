using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command : ScriptableObject
{
    public string shortcut;

    public virtual void Action(ScriptReader sr, UIController uic) 
    {
        Debug.Log("Fail");
    }
}

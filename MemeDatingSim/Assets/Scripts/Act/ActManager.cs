using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(DialogManager))]
[RequireComponent(typeof(UIController))]
public class ActManager : MonoBehaviour
{
    public Character mainCharacter;
    ScriptReader scriptReader;

    private void Start()
    {
        scriptReader = GetComponent<ScriptReader>();
        LoadAct();
    }

    public void LoadAct()
    {
        if(mainCharacter.act != null)
        {
            scriptReader.StartScript(mainCharacter.act.script);
            mainCharacter.act = mainCharacter.act.nextAct;
        }
    }
}

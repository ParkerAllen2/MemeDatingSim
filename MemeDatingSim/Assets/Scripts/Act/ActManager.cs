using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogManager))]
public class ActManager : MonoBehaviour
{
    public Character mainTarget;
    DialogManager dialogManager;

    private void Start()
    {
        dialogManager = GetComponent<DialogManager>();
        LoadAct();
    }

    public void LoadAct()
    {
        dialogManager.StartDialog(mainTarget.act.firstDialog);
        mainTarget.act = mainTarget.act.nextAct;
    }
}

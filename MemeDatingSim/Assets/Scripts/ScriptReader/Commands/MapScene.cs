using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map Scene", menuName = "Command/Map Scene")]
public class MapScene : Command
{
    public string mapScene;
    public string endScene;
    public override void Action(ScriptReader sr, UIController uic)
    {
        if (Overlord.Instance.NoActsLeft())
        {
            Overlord.Instance.LoadScene(endScene);
            return;
        }
        Overlord.Instance.LoadScene(mapScene);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map Scene", menuName = "Command/Map Scene")]
public class MapScene : Command
{
    public string mapScene;
    public override void Action(ScriptReader sr, UIController uic)
    {
        Overlord.Instance.LoadScene(mapScene);
    }
}

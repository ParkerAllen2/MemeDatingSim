using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScriptReader))]
public class ActManager : MonoBehaviour
{
    ScriptReader scriptReader;
    public Background[] bakgrounds;
    Act[] nextActs;

    /*
     * Given a charcter, get the characters act
     * Start reading the act's script
     * Store list of next possible acts
     */
    public void LoadAct(Act act)
    {
        scriptReader = GetComponent<ScriptReader>();
        if (act != null)
        {
            scriptReader.StartScript(act.script);
            nextActs = act.nextActs;
        }
    }

    public bool HasBackground(string shortcut, ref SpriteRenderer background)
    {
        foreach (Background b in bakgrounds)
        {
            if (b.shortcut.Equals(shortcut))
            {
                background.sprite = b.sprite;
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public class Background
{
    public Sprite sprite;
    public string shortcut;
}

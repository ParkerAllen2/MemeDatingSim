using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Overlord : Singleton<Overlord>
{
    protected Overlord() { }
    public Character[] allCharacters;

    public override void Awake()
    {
        base.Awake();
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public bool HasCharacter(string shortcut, out Character character)
    {
        foreach(Character c in allCharacters)
        {
            if (c.shortcut.Equals(shortcut))
            {
                character = c;
                return true;
            }
        }
        character = null;
        return false;
    }
}

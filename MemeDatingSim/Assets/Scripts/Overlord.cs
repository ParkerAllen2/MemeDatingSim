using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Overlord : Singleton<Overlord>
{
    protected Overlord() { }
    public Character[] characters;
    public Background[] bakgrounds;
    public Player player;

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
        foreach(Character c in characters)
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

    public bool HasBackground(string shortcut, out Sprite background)
    {
        foreach (Background b in bakgrounds)
        {
            if (b.shortcut.Equals(shortcut))
            {
                background = b.sprite;
                return true;
            }
        }
        background = null;
        return false;
    }

    public bool GetPlayer(string shortcut, out string pname)
    {
        pname = "";
        if (shortcut.StartsWith(player.shortcut))
        {
            pname = player.playerName +
                shortcut.Substring(player.shortcut.Length) + " ";
            return true;
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

[System.Serializable]
public class Player
{
    public string playerName;
    public string shortcut;
}

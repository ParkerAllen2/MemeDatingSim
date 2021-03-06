using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Overlord : Singleton<Overlord>
{
    protected Overlord() { }
    public Character[] characters;
    public Location[] scenes;
    public PlayerSettings player;
    public Act currentAct;

    TransitionController sceneTransition;

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        if (sceneTransition == null)
            sceneTransition = GetComponentInChildren<TransitionController>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /*
     * Loads in the act to act Manager
     * !Happens Before Start!
     */
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ActManager am = FindObjectOfType<ActManager>();
        if (am != null)
        {
            am.LoadAct(currentAct);
        }
    }

    public void LoadScene(Character character)
    {
        currentAct = character.act;
        character.act = null;
        LoadScene(GetScene(currentAct.location));
    }

    public void LoadScene(string scene)
    {
        sceneTransition.StartHideTransition(scene);
    }

    public void Exit()
    {
        Application.Quit();
    }

    //returns a true if character exsits and outs character
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

    //returns a true if shortcut for player name and outs player name
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

    // Returns scene name of location or first location's scene name
    public string GetScene(string shortcut)
    {
        foreach (Location s in scenes)
        {
            if (s.shortcut.Equals(shortcut) || s.sceneName.Equals(shortcut))
            {
                return s.sceneName;
            }
        }
        return scenes[0].sceneName;
    }

    public bool NoActsLeft()
    {
        foreach(Character c in characters)
        {
            if (c.act != null)
                return false;
        }
        return true;
    }
}

[System.Serializable]
public class Location
{
    public string sceneName;
    public string shortcut;
}

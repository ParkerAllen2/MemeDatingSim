using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUIController : MonoBehaviour
{
    public string themeMusic;
    public AvatarHolderPanel holderPanel;


    void Start()
    {
        holderPanel = GetComponentInChildren<AvatarHolderPanel>();
        AddCharacters();
        PlayTheme();
    }

    void PlayTheme()
    {
        SoundManager.Instance.StartPlayingTheme(themeMusic);
    }

    void AddCharacters()
    {
        bool complete = true;
        foreach(Character c in Overlord.Instance.characters)
        {
            if(c.act != null)
            {
                holderPanel.AddCharcter(c);
                complete = false;
            }
        }

        if (complete)
        {
            Overlord.Instance.LoadScene("EndScene");
        }
    }
}

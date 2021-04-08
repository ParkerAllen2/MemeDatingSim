using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    public float avatarScaleInc = 1.1f;
    public Color normal;
    public Color higlight;

    [SerializeField] Image avatar, avatarHolder;
    Character character;
    public PanelSizer panelSizer;

    public Character Character
    {
        get { return character; }
        set
        {
            character = value;
            avatar.sprite = character.expressions[0];
        }
    }

    public void Highlight()
    {
        avatarHolder.transform.localScale = Vector3.one * avatarScaleInc;
        avatarHolder.color = higlight;
    }

    public void DeHighlight()
    {
        avatarHolder.transform.localScale = Vector3.one;
        avatarHolder.color = normal;
    }
}

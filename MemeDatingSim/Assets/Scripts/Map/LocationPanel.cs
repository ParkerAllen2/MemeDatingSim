using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationPanel : MonoBehaviour
{
    public string location;
    public AvatarHolderPanel holderPanel;

    public void ShowCharactersHere()
    {
        holderPanel.ShowForLocation(location);
    }
}

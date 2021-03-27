using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUIController : MonoBehaviour
{
    LocationPanel[] locationPanels;


    void Start()
    {
        locationPanels = GetComponentsInChildren<LocationPanel>();
        AddCharacters();
    }

    void AddCharacters()
    {
        foreach(Character c in Overlord.Instance.characters)
        {
            FindLocation(c);
        }
    }

    void FindLocation(Character c)
    {
        foreach(LocationPanel lp in locationPanels)
        {
            if (lp.location.Equals(c.act.location))
            {
                lp.AddChar(c);
            }
        }
    }
}

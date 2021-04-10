using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarHolderPanel : MonoBehaviour
{
    public CharacterPanel charPanPrefab;
    List<CharacterPanel> characterPanels = new List<CharacterPanel>();

    public void AddCharcter(Character c)
    {
        CharacterPanel temp = Instantiate(charPanPrefab, transform);
        temp.Character = c;
        temp.gameObject.SetActive(false);
        characterPanels.Add(temp);

        Character cha = c;
        temp.GetComponent<Button>().onClick.AddListener(delegate
        {
            Overlord.Instance.LoadScene(cha);
        });
    }

    public void ShowForLocation(string location)
    {
        foreach (CharacterPanel cp in characterPanels)
        {
            if (cp.Character.act.location.Equals(location))
            {
                cp.panelSizer.PanelShow();
            }
            else
            {
                cp.panelSizer.PanelWidth(0);
            }
        }
    }

    public void ClearPanel()
    {

    }
}

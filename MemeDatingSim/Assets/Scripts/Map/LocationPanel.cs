using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationPanel : MonoBehaviour
{
    public string location;
    public CharacterPanel charPanPrefab;
    public Transform charListPanel;
    List<RectTransform> charPans = new List<RectTransform>();

    public float spacing;
    public float showSpeed;
    bool lockOpen;

    public void AddChar(Character c)
    {
        CharacterPanel temp = Instantiate(charPanPrefab, charListPanel);
        temp.SetInfo(c);
        charPans.Add(temp.GetComponent<RectTransform>());
        temp.gameObject.SetActive(false);

        Character cha = c;
        temp.GetComponent<Button>().onClick.AddListener(delegate
        {
            Overlord.Instance.LoadScene(cha);
        });
    }

    public void LockCharList()
    {
        lockOpen = !lockOpen;
    }

    public void ShowCharacters()
    {
        StopAllCoroutines();
        float s = -spacing;
        foreach(RectTransform t in charPans)
        {
            t.gameObject.SetActive(true);
            StartCoroutine(MoveCharacters(t, s, true));
            s -= spacing;
        }
    }

    public void HideCharacters()
    {
        if (lockOpen)
            return;

        StopAllCoroutines(); 
        foreach (RectTransform t in charPans)
        {
            StartCoroutine(MoveCharacters(t, 0, false));
        }
    }

    IEnumerator MoveCharacters(RectTransform tran, float to, bool active)
    {
        Vector3 current = tran.anchoredPosition;
        float from = current.y;
        float t = 0;
        while (t < showSpeed)
        {
            current.y = Mathf.Lerp(from, to, t / showSpeed);
            tran.anchoredPosition = current;
            yield return null;
            t += Time.deltaTime;
        }
        current.y = to;
        tran.anchoredPosition = current;
        tran.gameObject.SetActive(active);
    }
}

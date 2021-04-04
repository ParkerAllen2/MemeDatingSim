using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDown : MonoBehaviour
{
    List<RectTransform> panels = new List<RectTransform>();

    public Vector2 spacing;
    public float showSpeed;
    bool lockOpen;

    private void Start()
    {
        foreach(RectTransform rt in transform)
        {
            panels.Add(rt);
            rt.gameObject.SetActive(false);
        }
    }

    public void ListLock()
    {
        lockOpen = !lockOpen;
    }

    public void ListShow()
    {
        StopAllCoroutines();
        Vector2 s = -spacing;
        foreach (RectTransform t in panels)
        {
            t.gameObject.SetActive(true);
            StartCoroutine(MoveCharacters(t, s, true));
            s -= spacing;
        }
    }

    public void ListHide()
    {
        if (lockOpen)
            return;

        StopAllCoroutines();
        foreach (RectTransform t in panels)
        {
            StartCoroutine(MoveCharacters(t, Vector2.zero, false));
        }
    }

    IEnumerator MoveCharacters(RectTransform tran, Vector2 to, bool active)
    {
        Vector2 current = tran.anchoredPosition;
        Vector2 from = current;
        float t = 0;
        while (t < showSpeed)
        {
            current = Vector2.Lerp(from, to, t / showSpeed);
            tran.anchoredPosition = current;
            yield return null;
            t += Time.deltaTime;
        }

        tran.anchoredPosition = to;
        tran.gameObject.SetActive(active);
    }
}

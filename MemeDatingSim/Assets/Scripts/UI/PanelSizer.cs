using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSizer : MonoBehaviour
{
    public float showSpeed;
    public bool startZero;
    RectTransform panel;
    Vector2 defaultSize;
    Vector2 current;
    Vector2 to;

    private void Awake()
    {
        panel = GetComponent<RectTransform>();
        defaultSize = panel.sizeDelta;
        if (startZero)
            current = Vector2.zero;
        else
            current = defaultSize;
        to = defaultSize;
    }

    public void PanelShow()
    {
        to = defaultSize;
        StopAllCoroutines();
        gameObject.SetActive(true);
        StartCoroutine(ChangeSize(true));
    }

    public void PanelWidth(float width)
    {
        to.x = width;
        StopAllCoroutines();
        panel.gameObject.SetActive(true);
        StartCoroutine(ChangeSize(true));
    }

    public void PanelHeight(float height)
    {
        to.y = height;
        StopAllCoroutines();
        panel.gameObject.SetActive(true);
        StartCoroutine(ChangeSize(true));
    }

    public void PanelHide()
    {
        to = Vector2.zero;
        StopAllCoroutines();
        StartCoroutine(ChangeSize(false));
    }

    IEnumerator ChangeSize(bool active)
    {
        Vector2 from = current;
        float t = 0;
        while (t < showSpeed)
        {
            current = Vector2.Lerp(from, to, t / showSpeed);
            panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, current.x);
            panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, current.y);
            yield return null;
            t += Time.deltaTime;
        }

        panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, to.x);
        panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, to.y);
        panel.gameObject.SetActive(active);
    }
}

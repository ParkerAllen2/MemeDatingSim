using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    public RectTransform hidePanel, showPanel;
    public Vector2 toPositionHide, toPostionShow;
    Vector2 initialHidePosition, initialShowPosition;
    public float speed;
    HeartMovements[] hearts;

    void Start()
    {
        initialHidePosition = hidePanel.anchoredPosition;
        initialShowPosition = showPanel.anchoredPosition;
        hearts = GetComponentsInChildren<HeartMovements>();
        StartCoroutine(Show());
    }

    void StartShowTransition()
    {
        hidePanel.anchoredPosition = initialHidePosition;
        showPanel.anchoredPosition = initialShowPosition;
        StartCoroutine(Show());
    }

    IEnumerator Show()
    {
        hearts[1].StartMoving(speed);
        Vector2 from = initialShowPosition;
        showPanel.anchoredPosition = from;
        float ellapsed = 0;
        while(ellapsed < speed)
        {
            showPanel.anchoredPosition = Vector2.Lerp(from, toPostionShow, ellapsed / speed);
            yield return null;
            ellapsed += Time.deltaTime;
        }
    }

    public void StartHideTransition(string scene)
    {
        StartCoroutine(Hide(scene));
    }

    IEnumerator Hide(string scene)
    {
        hearts[0].StartMoving(speed);
        Vector2 from = initialHidePosition;
        float ellapsed = 0;
        while (ellapsed < speed)
        {
            hidePanel.anchoredPosition = Vector2.Lerp(from, toPositionHide, ellapsed / speed);
            yield return null;
            ellapsed += Time.deltaTime;
        }
        SceneManager.LoadScene(scene);
        StartShowTransition();
    }
}

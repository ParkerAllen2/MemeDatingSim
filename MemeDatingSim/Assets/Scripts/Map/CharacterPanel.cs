using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    public float avatarScaleInc = 1.1f;
    public float nameTagWidth;
    public float showSpeed;
    public Color normal;
    public Color higlight;

    [SerializeField] Image avatar, avatarHolder, textBack;
    [SerializeField] Text charName;
    [SerializeField] RectTransform nameTag;
    [SerializeField] Image backgrounds;

    public void SetInfo(Character c)
    {
        avatar.sprite = c.expressions[0];
        charName.text = c.characterName;
    }

    public void ShowName()
    {
        StopAllCoroutines();

        avatarHolder.transform.localScale = Vector3.one * avatarScaleInc;
        avatarHolder.color = higlight;
        textBack.color = higlight;

        nameTag.gameObject.SetActive(true);
        StartCoroutine(ChangeSize(nameTagWidth, true));
    }

    public void HideName()
    {
        StopAllCoroutines();

        avatarHolder.transform.localScale = Vector3.one;
        avatarHolder.color = normal;
        textBack.color = normal;

        StartCoroutine(ChangeSize(0, false));
    }

    IEnumerator ChangeSize(float to, bool active)
    {
        float current = nameTag.rect.width;
        float from = current;
        float t = 0;
        while(t < showSpeed)
        {
            current = Mathf.Lerp(from, to, t / showSpeed);
            nameTag.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, current);
            yield return null;
            t += Time.deltaTime;
        }
        nameTag.gameObject.SetActive(active);
    }
}

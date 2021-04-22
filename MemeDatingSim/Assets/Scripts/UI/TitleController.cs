using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    RectTransform[] transforms;
    Vector3[] positions;

    public float magnitude;
    public float frequency;

    private void Start()
    {
        transforms = GetComponentsInChildren<RectTransform>();
        positions = new Vector3[transforms.Length];
        for(int i = 0; i < transforms.Length; i++)
        {
            positions[i] = transforms[i].anchoredPosition;
        }
    }

    private void Update()
    {
        float f = 0;
        for (int i = 1; i < transforms.Length; i++)
        {
            Vector3 y = new Vector3(0, Mathf.Sin(Time.time + f) * magnitude);
            transforms[i].anchoredPosition = positions[i] + y;
            f += frequency;
        }
    }
}

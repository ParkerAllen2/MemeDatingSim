using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartMovements : MonoBehaviour
{
    RectTransform[] transforms;
    Vector3[] positions;
    public float spinMagnitude;

    private void Awake()
    {
        transforms = GetComponentsInChildren<RectTransform>();
        positions = new Vector3[transforms.Length];

        for (int i = 1; i < transforms.Length; i++)
        {
            positions[i] = transforms[i].anchoredPosition;
            if(spinMagnitude != 0)
                transforms[i].Rotate(0, Random.Range(-spinMagnitude, spinMagnitude), 0);
        }
    }

    public void StartMoving(float time)
    {
        // StartCoroutine(Move(time));
    }

    IEnumerator Move(float time)
    {
        float ellapsed = 0;
        while (ellapsed < time)
        {
            float sine = Mathf.Sin(Time.time * 5);
            print(sine);
            for (int i = 1; i < positions.Length; i++)
            {
                transforms[i].rotation = Quaternion.Euler(0, sine * spinMagnitude, 0);
            }
            yield return null;
            ellapsed += Time.deltaTime;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxColorer : MonoBehaviour
{
    public Image[] outer;
    public Image[] inner;
    public Image[] fill;

    public void ChangeColors(Color[] colors)
    {
        ChangeColor(outer, colors[0]);
        ChangeColor(inner, colors[1]);
        ChangeColor(fill, colors[2]);
    }

    void ChangeColor(Image[] imgs, Color c)
    {
        foreach(Image i in imgs)
        {
            i.color = c;
        }
    }
}

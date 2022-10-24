using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public static HealthUI instance { get; private set; }
    public Image hp;
    float maxWidth;
    // Start is called before the first frame update

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        maxWidth = hp.rectTransform.rect.width;
    }

    // Update is called once per frame
    public void UpdateMask(float value)
    {
        Debug.Log(value);
        hp.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWidth * value);
    }
}

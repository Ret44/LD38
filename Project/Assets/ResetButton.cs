using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;

public class ResetButton : MonoBehaviour
{
    public static bool isShow = false;
    public static ResetButton instance;
    public static void ShowMe()
    {
        ((RectTransform)instance.transform).DOAnchorPosX(-36, 1f).SetEase(Ease.InOutBack);
        isShow = true;
    }

    public static void HideMe()
    {
        ((RectTransform)instance.transform).DOAnchorPosX(300, 1f).SetEase(Ease.InOutBack);
        isShow = false;
    }

    // Use this for initialization
    void Start()
    {
        instance = this;
    }
}
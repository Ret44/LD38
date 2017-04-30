using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class FadeToBlack : MonoBehaviour {

    public static FadeToBlack instance;

    public Image back;

    public static void FadeTo(float val, float time = 1.0f)
    {
        instance.back.DOColor(new Color(0f, 0f, 0f, val), time);
    }
    
    void Awake()
    {
        instance = this;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TaskGuide : MonoBehaviour {
    public static TaskGuide instance;
    public static void ShowMe()
    {
        ((RectTransform)instance.transform).DOAnchorPosY(170, 1f).SetEase(Ease.InOutBack);
    }

    public static void HideMe()
    {
        ((RectTransform)instance.transform).DOAnchorPosY(353, 1f).SetEase(Ease.InOutBack);
    }

    // Use this for initialization
    void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

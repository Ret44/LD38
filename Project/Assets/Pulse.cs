using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Pulse : MonoBehaviour {

    public Text text;

    void GoOut()
    {
        text.DOColor(Color.white, 1f).OnComplete(GoIn);
    }

    void GoIn()
    {
        text.DOColor(new Color(1f, 1f, 1f, 0.45f), 1f).OnComplete(GoOut);
    }

	// Use this for initialization
	void Start () {
        GoIn();
	}
}

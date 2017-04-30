using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LogoCard : MonoBehaviour {

    public Image background;
    public Image logo;
    public TitleCard titleCard;

    void OnLogoCompleted()
    {
        SoundManager.MainTheme.Play();
        Invoke("OnWaitCompleted", 1.5f);
    }

    void OnWaitCompleted()
    {
        background.DOColor(new Color(0, 0, 0, 0), 1f);
        logo.DOColor(new Color(0, 0, 0, 0), 1f);
        titleCard.isActive = true;
    }
	// Use this for initialization
	void Start () {
        logo.DOColor(Color.white, 1.0f).OnComplete(OnLogoCompleted);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

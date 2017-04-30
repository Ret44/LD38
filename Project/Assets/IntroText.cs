using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IntroText : MonoBehaviour {
    RectTransform rTransform;
    public IntroText introText;
    public bool isActive;
    public bool quitOnHide;    
    public void ShowMe()
    {
        rTransform.DOLocalMoveY(0f, 1f).OnComplete(OnShowDone);
    }
       
    void OnShowDone()
    {
        isActive = true;
    }

	// Use this for initialization
	void Start () {
        rTransform = (RectTransform)transform;
        rTransform.DOLocalMoveY(1900f, 0.001f);
    }

    void quitHideDone()
    {
#if UNITY_WEBGL
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
#else
        Application.Quit();
#endif
    }

    void onDoneHiding()
    {
        GameManager.instance.currentState = GameState.GAME;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown && isActive)
        {
            isActive = false;
           
            SoundManager.Click.Play();
            if (introText == null)
            {
                if (!quitOnHide)
                {
                    rTransform.DOLocalMoveY(1900, 1f).OnComplete(onDoneHiding);
                    FadeToBlack.FadeTo(0);
                              
                }
                else
                {
                    rTransform.DOLocalMoveY(1900, 1f).OnComplete(quitHideDone);
                }

            }
            else
            {
                rTransform.DOLocalMoveY(1900, 1f);
                introText.ShowMe();
            }
        }	
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCard : MonoBehaviour {

    public bool isActive = false;
    public IntroText introText;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && isActive)
        {
            isActive = false;
            SoundManager.Click.Play();
            FadeToBlack.FadeTo(0.75f, 0.001f);
            introText.ShowMe();
            Destroy(gameObject);
        }
    }
}

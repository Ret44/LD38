using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    INTRO,
    GAME,
    MIDGAME,
    OUTRO,

}

public enum GamePhase
{
    SIMS,
    PUZZLE
}

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    [SerializeField]
    private GamePhase _currentPhase;

    public IntroText outro;
    public IntroText midText;
    public GameState currentState;

    public UnityEngine.UI.Text taskGuide;
    public Day currentDay;

    public delegate void ResetGameEvent();
    public FollowMouse[] items;

    public event ResetGameEvent onResetGameEvent;

    public static GamePhase currentPhase
    {
        get
        {
            return instance._currentPhase;
        }
        set
        {
            instance._currentPhase = value;
        }
    }

    public static void EndGame()
    {
        SoundManager.Fanfare.Play();
        SoundManager.MainTheme.Stop();
        SoundManager.RadioTheme.Stop();
        instance.currentState = GameState.OUTRO;
        FadeToBlack.FadeTo(1);
        instance.outro.ShowMe();
        ResetButton.HideMe();
    }

    public void OnDoneClick()
    {
        currentState = GameState.MIDGAME;
        _currentPhase = GamePhase.PUZZLE;
        currentDay.Begin();
        TaskGuide.ShowMe();
        FadeToBlack.FadeTo(0.5f);
        ResetButton.ShowMe();
        StartButton.HideMe();
        midText.ShowMe();
        SoundManager.Click.Play();
    }

    void Awake()
    {
        instance = this;
        currentState = GameState.INTRO;
    }

    public void ResetGame()
    {
        ResetButton.HideMe();
        SoundManager.Click.Play();
        _currentPhase = GamePhase.SIMS;
        Apartment.Reset();
        currentDay.Reset();
        TaskGuide.HideMe();
        onResetGameEvent();
    }

	// Use this for initialization
	void Start () {
		
	}


    public static void CheckItems()
    {
        bool allPlaced = true;
        for (int i = 0; i < instance.items.Length; i++)
        {
            if (!instance.items[i].isPlaced)
                allPlaced = false;
        }
        if (allPlaced && !StartButton.isShow)
            StartButton.ShowMe();
        else if(!allPlaced && StartButton.isShow)
            StartButton.HideMe();
    }

	// Update is called once per frame
	void Update () {
		if(currentPhase == GamePhase.PUZZLE && currentDay != null)
        {
            taskGuide.text = currentDay.GetDesc();
        }

        if(Input.GetKey(KeyCode.Escape))
        { 
            ResetGame();
        }

	}
}

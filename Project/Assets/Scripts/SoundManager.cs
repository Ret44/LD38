using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;

    [SerializeField]
    private AudioSource _thump;
    public static AudioSource Thump { get { return instance._thump; } }
    
    [SerializeField]
    private AudioSource _click;
    public static  AudioSource Click { get { return instance._click; } }
    
    [SerializeField]
    private AudioSource _rotate;
    public static AudioSource Rotate { get { return instance._rotate; } }
    
    [SerializeField]
    private AudioSource _fail;
    public static AudioSource Fail { get { return instance._fail; } }

    [SerializeField]
    private AudioSource _grunt;
    public static AudioSource Grunt { get { return instance._grunt; } }


    [SerializeField]
    private AudioSource _mainTheme;
    public static AudioSource MainTheme { get { return instance._mainTheme; } }

    [SerializeField]
    private AudioSource _radioTheme;
    public static AudioSource RadioTheme { get { return instance._radioTheme; } }

    [SerializeField]
    private AudioSource _fanfare;
    public static AudioSource Fanfare { get { return instance._fanfare; } }


    [SerializeField]
    private AudioSource _taskDone;
    public static AudioSource TaskDone { get { return instance._taskDone; } }



    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}

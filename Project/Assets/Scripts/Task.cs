using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour {
    public string desc;
    public FollowMouse character;
    public FollowMouse target;

    public Task[] dependingOn;

    public bool isActive;
    public bool isDone;

    public void Update()
    {
        if (isActive)
            isDone = character.IsNeighboredTo(target);
    }
}

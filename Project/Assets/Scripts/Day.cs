using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day : MonoBehaviour {

    public List<Task> tasks;

    public List<Task> currentTasks;

    public List<Task> tasksDone;

	// Use this for initialization
	void Start () {
		
	}
	
    void NextTasks()
    {       
        if (tasks.Count == 0)
            GameManager.EndGame();
        else {
            SoundManager.TaskDone.Play();
            if (currentTasks[0].target.customUseSound != null)
                currentTasks[0].target.customUseSound.Play();
            tasksDone.Add(currentTasks[0]);
            currentTasks.Clear();
            currentTasks.Add(tasks[0]);
            tasks.RemoveAt(0);
            for (int i = 0; i < currentTasks.Count; i++)
            {
                currentTasks[i].isActive = true;
            }
        }
    }

    public void Reset()
    {
        for (int i = 0; i < currentTasks.Count; i++)
            tasksDone.Add(currentTasks[i]);
        currentTasks.Clear();
        for (int i = 0; i < tasks.Count; i++)
            tasksDone.Add(tasks[i]);
        tasks.Clear();
        for (int i = 0; i < tasksDone.Count; i++)
            tasks.Add(tasksDone[i]);
        tasksDone.Clear();        
    }

    public void Begin()
    {
        for (int i = 0; i < currentTasks.Count; i++)
        {
            currentTasks[i].isActive = true;
        }
    }
    public string GetDesc()
    {
        string desc = "";
        for (int i = 0; i < currentTasks.Count; i++)
        {
            desc = string.Format("{0}{1}{2}", (i == 0 ? "" : desc), (i == 0 ? "" : Environment.NewLine), currentTasks[i].desc);
        }
        return desc;
    }

    public void CheckTask()
    {
        if (currentTasks.Count > 0)
        {
            bool allDone = true;
            for (int i = 0; i < currentTasks.Count; i++)
            {
                if (!currentTasks[i].isDone)
                    allDone = false;
            }
            if (allDone)
                NextTasks();

        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}

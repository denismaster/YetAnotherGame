using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task
{
    public abstract string taskName { get; set; }
    public float interval { get; set; }

    public IEnumerator ExecuteTask()
    {
        while(true)
        {
            Execute();
            yield return new WaitForSecondsRealtime(interval);
        }
    }

    public abstract void Execute();
}
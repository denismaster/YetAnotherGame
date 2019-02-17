using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class TaskRunner : MonoBehaviour
{
    private const string _tasksConfigFilename = "tasks.config";
    private void Awake()
    {
        try
        {
            RegisterTasks();

            foreach(var taskSettings in Settings.taskRunnerSettings.tasks)
            {
                Task task = RegisteredTasks.GetTask(taskSettings.taskName);
                if(task == null)
                {
                    Debug.LogError($"Task with name {taskSettings.taskName} is not registered in TaskRunner.");
                }
                task.interval = taskSettings.interval;
                StartCoroutine(task.ExecuteTask());
            }
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    private void RegisterTasks()
    {
        RegisteredTasks.Register(new GenerateCoinTask());
    }

    private void OnApplicationQuit()
    {
        StopAllCoroutines();
    }
}
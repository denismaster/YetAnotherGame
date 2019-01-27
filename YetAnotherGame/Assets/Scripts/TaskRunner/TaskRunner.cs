using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class TaskRunner : MonoBehaviour
{
    private void Awake()
    {
        try
        {
            RegisterTasks();

            var path = $"{Application.dataPath}/tasks.config";
            if(!File.Exists(path))
            {
                Debug.LogError($"Filepath {path} does not exist.");
                return;
            }

            string settings = File.ReadAllText(path);
            if(settings.Length == 0)
            {
                Debug.LogError($"File {path} is empty.");
                return;
            }
            var taskSettingsList = JsonUtility.FromJson<Tasks>(settings);

            foreach(var taskSettings in taskSettingsList.tasks)
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
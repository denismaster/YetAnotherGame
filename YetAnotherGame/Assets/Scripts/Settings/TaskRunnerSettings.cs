using System;
using System.Collections.Generic;

[Serializable]
public class TaskRunnerSettings
{
    public List<TaskSettings> tasks;

    [Serializable]
    public class TaskSettings
    {
        public string taskName;
        public float interval;
    }
}
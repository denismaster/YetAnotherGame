using System;
using System.Collections.Generic;

[Serializable]
public class Tasks
{
    public List<TaskSettings> tasks;

    [Serializable]
    public class TaskSettings
    {
        public string taskName;
        public float interval;
    }
}
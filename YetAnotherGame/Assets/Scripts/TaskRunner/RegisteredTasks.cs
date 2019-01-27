using System.Collections;
using System.Collections.Generic;

public static class RegisteredTasks
{
    private static Dictionary<string, Task> _tasksDictionary = new Dictionary<string, Task>();

    public static void Register(Task task)
    {
        Task value;
        if (!_tasksDictionary.TryGetValue(task.taskName, out value))
        {
            _tasksDictionary[task.taskName] = task;
        }
    }

    public static Task GetTask(string taskName)
    {
        Task value;
        if (!_tasksDictionary.TryGetValue(taskName, out value))
        {
            return null;
        }

        return value;
    }
}
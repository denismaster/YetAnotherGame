using System;
using UnityEngine;

[Serializable]
public class Settings : MonoBehaviour
{
    private static GameSettings _gameSettings = null;
    public static GameSettings gameSettings
    {
        get
        {
            if(_gameSettings == null)
            {
                _gameSettings = JsonUtils.ParseFromFile<GameSettings>("game.config");
            }
            return _gameSettings;
        }
    }

    private static TaskRunnerSettings _taskRunnerSettings = null;
    public static TaskRunnerSettings taskRunnerSettings
    {
        get
        {
            if(_taskRunnerSettings == null)
            {
                _taskRunnerSettings = JsonUtils.ParseFromFile<TaskRunnerSettings>("tasks.config");
            }
            return _taskRunnerSettings;
        }
    }

    //private static UserSettings _userSettings = null;
    //public static UserSettings userSettings { get {} }

    private Settings()
    {

    }
}
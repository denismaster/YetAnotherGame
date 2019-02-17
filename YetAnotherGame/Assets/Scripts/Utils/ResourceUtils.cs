using UnityEngine;

public class ResourceUtils
{
    public static UnityEngine.Object Load(string prefabName)
    {
        return Resources.Load($"Prefabs/{prefabName}");
    }

    public static AudioClip LoadSound(string name)
    {
        return (AudioClip) Resources.Load($"Audio/Sounds/{name}");
    }
}
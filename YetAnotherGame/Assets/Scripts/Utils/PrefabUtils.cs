using System;
using UnityEngine;

public class PrefabUtils
{
    public static UnityEngine.Object Load(string prefabName)
    {
        return Resources.Load($"Prefabs/{prefabName}");
    }
}
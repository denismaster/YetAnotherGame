using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[Serializable]
public class GameSettings
{
    public ObjectGeneration objectGeneration;
}

[Serializable]
public class ObjectGeneration
{
    public int coinsCount;
    public float gameObjectsGenerationAreaXsize;
    public float gameObjectsGenerationAreaYsize;
}
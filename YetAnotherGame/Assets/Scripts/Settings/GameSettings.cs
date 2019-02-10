using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[Serializable]
public class GameSettings
{
    public ObjectGeneration objectGeneration;
    public Damage damage;
    public Heal heal;
    public Player player;
}

[Serializable]
public class ObjectGeneration
{
    public int coinsCount;
    public float gameObjectsGenerationAreaXsize;
    public float gameObjectsGenerationAreaYsize;
}

[Serializable]
public class Damage
{
    public int mine;
}

[Serializable]
public class Heal
{
    public int turkey;
}

[Serializable]
public class Player
{
    public int startingHp;
}
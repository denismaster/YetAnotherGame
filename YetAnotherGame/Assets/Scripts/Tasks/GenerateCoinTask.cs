using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCoinTask : Task
{
    public override string taskName { get; set; } = "GenerateCoinTask";
    private static int _coinsCount = Settings.gameSettings.objectGeneration.coinsCount;
    private static Rect _generationArea;
    private static UnityEngine.Object _coinPrefab = ResourceUtils.Load(PrefabConstants.Coin);

    public GenerateCoinTask()
    {
        try
        {
            _generationArea = GetGenerationArea();

            for (int i = 0; i < _coinsCount; i++)
            {
                GameObject coin = GenerateCoinInRandomPoint();
            }

            //todo: use ray cast to determine whether this point is valid on terrain
            //GameObject.Instantiate(coinPrefab, new Vector3(3.57f, 0.3f, 4.42f), new Quaternion());
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private static GameObject GenerateCoinInRandomPoint()
    {
        var point = GenerateSpawnPoint(_generationArea);
        return GameObject.Instantiate(_coinPrefab, point, new Quaternion()) as GameObject;
    }

    private static Rect GetGenerationArea()
    {
        var leftBottomBorderPoint = GameObject.FindGameObjectWithTag("LeftBottom");
        var start = new Vector2(leftBottomBorderPoint.transform.position.x, leftBottomBorderPoint.transform.position.z);
        var xSize = Settings.gameSettings.objectGeneration.gameObjectsGenerationAreaXsize;
        var ySize = Settings.gameSettings.objectGeneration.gameObjectsGenerationAreaYsize;
        return new Rect(start, new Vector2(xSize, ySize));
    }

    private static Vector3 GenerateSpawnPoint(Rect generationArea)
    {
        var x = UnityEngine.Random.Range(generationArea.x, generationArea.xMax);
        var y = UnityEngine.Random.Range(generationArea.y, generationArea.yMax);
        return new Vector3(x, 0.3f, y);
    }

    public override void Execute()
    {
        var currentCoinsCount = GameObject.FindGameObjectsWithTag(TagConstants.Coin).Length;        

        while(currentCoinsCount < _coinsCount)
        {
            GenerateCoinInRandomPoint();
            currentCoinsCount++;
        }
    }
}

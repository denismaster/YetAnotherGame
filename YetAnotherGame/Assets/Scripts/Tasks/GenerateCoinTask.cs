using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCoinTask : Task
{
    public override string taskName { get; set; } = "GenerateCoinTask";

    //todo: move it to game.config
    private static int _coinsCount = 10;

    private static Rect _generationArea;

    static GenerateCoinTask()
    {
        try
        {
            _generationArea = GetGenerationArea();
            UnityEngine.Object coinPrefab = PrefabUtils.Load(PrefabConstants.Coin);

            for (int i = 0; i < _coinsCount; i++)
            {
                var point = GenerateSpawnPoint(_generationArea);
                GameObject coin = GameObject.Instantiate(coinPrefab, point, new Quaternion()) as GameObject;                
            }

            //todo: use ray cast to determine whether this point is valid on terrain
            //GameObject.Instantiate(coinPrefab, new Vector3(3.57f, 0.3f, 4.42f), new Quaternion());
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private static Rect GetGenerationArea()
    {
        var leftBottomBorderPoint = GameObject.FindGameObjectWithTag("LeftBottom");
        var start = new Vector2(leftBottomBorderPoint.transform.position.x, leftBottomBorderPoint.transform.position.z);
        return new Rect(start, new Vector2(60,60));
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
            //add coins to scene after some of them will be collected

            currentCoinsCount++;
        }
    }
}

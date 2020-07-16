using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyBoxSpawner : Spawner
{
    private Vector2 nextSpawnLocation;

    protected override float GetTimeBetweenSpawn(float timeElapsed)
    {
        if (timeElapsed <= 240)
        {
            return 7 - ((1 / 60f) * timeElapsed);
        }
        else
        {
            return 3;
        }
    }


    protected override void SpawnObject()
    {
        Transform spawnTransform = GetRandomSpawnTransform();

        //if a supply box already exists at the spawn location, do nothing
        if (spawnTransform.childCount == 0)
        {
            Transform obj = Instantiate(objectToSpawn);

            obj.gameObject.SetActive(true);
            obj.position = nextSpawnLocation;
            nextSpawnLocation = spawnTransform.position;
            obj.parent = spawnTransform;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : Spawner
{
    protected override float GetTimeBetweenSpawn(float timeElapsed)
    {
        if (timeElapsed <= 720)
        {
            return 10 - ( (1 / 60f) * timeElapsed);
        }
        else
        {
            return 4;
        }
    }

}

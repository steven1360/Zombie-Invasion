using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : Spawner
{
    protected override float GetTimeBetweenSpawn(float timeElapsed)
    {
        if (timeElapsed <= 360)
        {
            return 7 - ( (1 / 60f) * timeElapsed);
        }
        else
        {
            return 1;
        }
    }

}

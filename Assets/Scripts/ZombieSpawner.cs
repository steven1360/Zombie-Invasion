using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private Transform objectToSpawn;
    private Clock clock;
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        clock = new Clock(GetTimeBetweenSpawn(0), 0);
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        clock.DesiredWaitTime = GetTimeBetweenSpawn(timeElapsed);

        if (clock.ReachedDesiredWaitTime())
        {
            SpawnObject();
            clock.ResetClock();
        }

        clock.Tick(Time.deltaTime);
        timeElapsed += Time.deltaTime;
    }

    float GetTimeBetweenSpawn(float x)
    {
        if (x <= 60)
        {
            return 10 - ( (1 / 60f) * x);
        }
        else
        {
            return 4;
        }
    }

    void SpawnObject()
    {
        Transform obj = Instantiate(objectToSpawn);
        obj.gameObject.SetActive(true);
        obj.position = GetRandomSpawnpoint();
        Debug.Log("spawned");
    }

    Vector2 GetRandomSpawnpoint()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        return transform.GetChild(Random.Range(0, transform.childCount)).position;
    }
}

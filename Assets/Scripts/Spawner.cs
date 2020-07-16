using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected Transform objectToSpawn;
    [SerializeField] private Tilemap tilemap;
    private Clock clock;
    private float timeElapsed;
    private Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        clock = new Clock(GetTimeBetweenSpawn(0), 0);
        timeElapsed = 0;
        grid = new Grid(tilemap);
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


    protected abstract float GetTimeBetweenSpawn(float timeElapsed);

    virtual protected void SpawnObject()
    {
        Transform spawnTransform = GetRandomSpawnTransform();
        Transform obj = Instantiate(objectToSpawn);
        obj.gameObject.SetActive(true);
        obj.position = spawnTransform.position;
    }

    protected Transform GetRandomSpawnTransform()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        return transform.GetChild(Random.Range(1, transform.childCount));
    }
}

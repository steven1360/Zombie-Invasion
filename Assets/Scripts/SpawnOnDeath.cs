using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDeath : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private SupplyBoxSpawner spawner;
    private IKillable killable;
    private bool spawned;

    // Start is called before the first frame update
    void Start()
    {
        killable = obj.GetComponent<IKillable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawned && killable != null && killable.IsDead())
        {
            spawner.SpawnSupplyBox(obj.transform);
            spawned = true;
        }
    }
}

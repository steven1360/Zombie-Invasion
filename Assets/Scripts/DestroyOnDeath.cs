using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    [SerializeField] private PlayerStatManager statManager;

    // Update is called once per frame
    void Update()
    {
        if (statManager.Stats.Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

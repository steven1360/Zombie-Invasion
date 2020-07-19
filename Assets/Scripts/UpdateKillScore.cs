using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateKillScore : MonoBehaviour
{
    [SerializeField] private PlayerStatManager playerStatMan;
    [SerializeField] private ZombieStatManager zomStatMan;

    private bool scoreUpdated;

    // Update is called once per frame
    void Update()
    {
        if (!scoreUpdated && zomStatMan.IsDead())
        {
            playerStatMan.Stats.AddKills(1);
            scoreUpdated = true;
        }
    }
}

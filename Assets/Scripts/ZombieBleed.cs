using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBleed : MonoBehaviour
{
    [SerializeField] private Damageable damageable;
    [SerializeField] private Transform blood;
    private bool playingBleedAnim;

    void Start()
    {
        damageable.OnDamageSourceTouched += () => {
            if (!playingBleedAnim)
            {
                StartCoroutine(SetActiveForSeconds(0.5f));
            }
        };
    }

    IEnumerator SetActiveForSeconds(float seconds)
    {
        blood.gameObject.SetActive(true);
        playingBleedAnim = true;
        yield return new WaitForSeconds(seconds);
        blood.gameObject.SetActive(false);
        playingBleedAnim = false;
    }
}

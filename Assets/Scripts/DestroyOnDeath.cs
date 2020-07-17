using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private float delayInSeconds;
    private IKillable killable;

    void Start()
    {
        killable = obj.GetComponent<IKillable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (killable != null && killable.IsDead())
        {
            StartCoroutine(DestroyAfterSeconds(delayInSeconds));
            killable = null; //ensures couroutine gets called only once
        }
    }

    IEnumerator DestroyAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}

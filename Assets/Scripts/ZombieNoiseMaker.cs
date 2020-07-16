using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieNoiseMaker : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private Transform player;
    private AudioSource audSource;

    void Start()
    {
        audSource = GetComponent<AudioSource>();
        StartCoroutine(MakeNoise());
    }

    IEnumerator MakeNoise()
    {
        while (true)
        {
            yield return new WaitForSeconds(7f);
            Random.InitState(System.DateTime.Now.Millisecond);
            int randomIndex = Random.Range(0, audioClips.Length);
            audSource.clip = audioClips[randomIndex];
            audSource.Play();
        }
    }

}

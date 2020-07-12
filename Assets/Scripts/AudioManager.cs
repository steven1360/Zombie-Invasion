using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audioClips;

    [SerializeField]
    private Camera mainCam;

    private Dictionary<string, AudioClip> dict;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        dict = new Dictionary<string, AudioClip>();
        audioSource = GetComponent<AudioSource>();
        foreach (AudioClip clip in audioClips)
        {
            dict.Add(clip.name, clip);
        }

    }

    void Update()
    {
        transform.position = mainCam.transform.position;

        foreach (Transform child in GetComponentInChildren<Transform>())
        {
            if (!child.GetComponent<AudioSource>().isPlaying)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void PlayClip(string clipName)
    {

        if (!dict.TryGetValue(clipName, out AudioClip clip))
        {
            Debug.Log($"{clipName} does not exist in the dictionary.");
        }

        if (audioSource.isPlaying)
        {
            GameObject obj = new GameObject();
            obj.AddComponent<AudioSource>();
            obj.transform.parent = transform;

            AudioSource childAudioSrc = obj.GetComponent<AudioSource>();
            childAudioSrc.clip = clip;
            childAudioSrc.Play();
        }
        else
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
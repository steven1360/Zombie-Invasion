using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private Camera mainCamera;

    private Dictionary<string, AudioClip> audioClips_dict;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        audioClips_dict = new Dictionary<string, AudioClip>();
        audioSource = GetComponent<AudioSource>();
        foreach (AudioClip clip in audioClips)
        {
            audioClips_dict.Add(clip.name, clip);
        }

    }

    void Update()
    {
        transform.position = mainCamera.transform.position;

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

        if (!audioClips_dict.TryGetValue(clipName, out AudioClip clip))
        {
            Debug.Log($"{clipName} does not exist in the dictionary.");
        }

        //Need to create a new child audiosource if one is already playing on this gameobject
        //so that the one that is already playing doesn't get interrupted
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
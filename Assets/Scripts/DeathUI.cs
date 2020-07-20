using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeathUI : MonoBehaviour
{
    [SerializeField] private Text survivalTime;

    [SerializeField] private TimeDisplay time;

    // Update is called once per frame
    void Update()
    {
        survivalTime.text = time.FormatTime();
    }
}

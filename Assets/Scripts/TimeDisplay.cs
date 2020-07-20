using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{
    [SerializeField] PlayerStatManager statManager;
    private float timeElapsed;

    void Update()
    {
        if (!statManager.IsDead())
        {
            timeElapsed += Time.deltaTime;
        }
    }

    public string FormatTime()
    {
        float timeElapsedCopy = timeElapsed;
        int hours = Mathf.FloorToInt(timeElapsedCopy / 3600);
        timeElapsedCopy = timeElapsedCopy - (hours * 3600);
        int minutes = Mathf.FloorToInt(timeElapsedCopy / 60);
        timeElapsedCopy = timeElapsedCopy - (minutes * 60);
        int seconds = Mathf.FloorToInt(timeElapsedCopy);

        //msd = "most significant digit" i.e., leftmost digit
        string msd_hours = (hours <= 9) ? "0" : "";
        string msd_minutes = (minutes <= 9) ? "0" : ""; 
        string msd_seconds = (seconds <= 9) ? "0" : ""; 

        return $" {msd_hours}{hours}:{msd_minutes}{minutes}:{msd_seconds}{seconds}";
    }
}

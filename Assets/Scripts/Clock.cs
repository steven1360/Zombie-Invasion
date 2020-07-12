using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock
{
    public float DesiredWaitTime { get; private set; }
    public float TimeElapsed { get; private set; }

    public Clock(float DesiredWaitTime, float TimeElapsed)
    {
        this.DesiredWaitTime = DesiredWaitTime;
        this.TimeElapsed = TimeElapsed;
    }

    public void Tick(float dt)
    {
        TimeElapsed += dt;
    }

    public bool ReachedDesiredWaitTime()
    {
        if (TimeElapsed >= DesiredWaitTime)
        {
            return true;
        }
        return false;
    }

    public void ResetClock()
    {
        TimeElapsed = 0;
    }

    public void SetRandomWaitTime(float min, float max)
    {
        DesiredWaitTime = Random.Range(min, max);
    }

}
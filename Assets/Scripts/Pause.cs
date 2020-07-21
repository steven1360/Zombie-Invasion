using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private PlayerStatManager statManager;
    [SerializeField] private GameObject[] objsToDisableOnPause;

    public bool Paused { get; private set; }


    void Update()
    {
        if (statManager.IsDead()) return;

        if ( !Paused && (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)))
        {
            Time.timeScale = 0;
            Paused = true;
            SetActiveAll(objsToDisableOnPause, false);
        }
        else if (Paused && (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)))
        {
            Time.timeScale = 1;
            Paused = false;
            SetActiveAll(objsToDisableOnPause, true);
        }
    }

    private void SetActiveAll(GameObject[] objects, bool active)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(active);
        }
    }
}

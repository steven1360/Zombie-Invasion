using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private PlayerStatManager statManager;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject deathUI;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject darknessEffect;
    [SerializeField] private Pause pause;

    // Update is called once per frame
    void Update()
    {

        if (statManager.Stats.Health <= 0)
        {
            mainUI.SetActive(false);
            deathUI.SetActive(true);
            darknessEffect.SetActive(true);
            return;
        }

        if (pause.Paused)
        {
            mainUI.SetActive(false);
            pauseScreen.SetActive(true);
            darknessEffect.SetActive(true);
        }
        else
        {
            mainUI.SetActive(true);
            pauseScreen.SetActive(false);
            darknessEffect.SetActive(false);
        }

    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("Game");    
    }
}

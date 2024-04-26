using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool stop = false;
    [SerializeField] private GameObject PausePanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            stop = !stop;
            if (stop)
            {
               Resume();
            }
            else
            {
                Pause();
                
            }
            

        }
    }

    void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;

    }
    void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class TimeCountDown : MonoBehaviour
{
    [SerializeField] private float time = 60;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private GameObject WinScreen;
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            textMeshProUGUI.text = "" + time.ToString("f1");
        }
        else
        {
            WinScreen.SetActive(true);
            Time.timeScale = 0f;
        }

           
    }

    public float getTime()
    {
        return time;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private int _NumSceneLoad;
    public void NewGame()
    {
        SceneManager.LoadScene(_NumSceneLoad);
    }
    public void Exit()
    {
        Application.Quit();
    }
    
}

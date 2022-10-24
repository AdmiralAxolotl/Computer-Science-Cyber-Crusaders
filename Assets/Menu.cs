using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    private bool gamePause;

    void Start()
    {
        menu = GameObject.FindWithTag("PauseMenu");
        menu.SetActive(true);
        gamePause = true;
        Time.timeScale = 0f;
    }
    
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle();
        }
        if (Input.GetButtonDown("Quit"))
        {
            Application.Quit();
        }
    }

    private void PauseToggle()
    {
        Time.timeScale = (!gamePause) ? 0f : 1f;
        gamePause = !gamePause;
        menu.SetActive(gamePause);
        
        
    }
}

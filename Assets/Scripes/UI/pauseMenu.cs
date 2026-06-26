
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;




public class pauseMenu : MonoBehaviour
{
    public Camera cameraFollowPlayer;
    public GameObject pauseMenuUI;
    public GameObject controlMenuUI;
    public GameObject intructionsMenuUI;
    public GameObject optionsMenuUI;
    public bool isPaused = false;
    public float previousRotationSpeed; // Store the previous rotation speed to restore it when unpausing
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (cameraFollowPlayer == null)
        {
            cameraFollowPlayer = Camera.main;
            if (cameraFollowPlayer == null)
            {
                Debug.LogError("Main Camera not found! Ensure the camera has the 'MainCamera' tag.");
            }
        }
    }
    void Update()
    {
        pause();  
    
        
    }

    // Update is called once per frame
    public void pause()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (Time.timeScale == 1)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
                isPaused = true;
                if (pauseMenuUI != null)
                {
                    pauseMenuUI.SetActive(true); // Show the pause menu 
                }
            }
            else
            {
                Time.timeScale = 1;
                isPaused = false;
                if (pauseMenuUI != null)
                {
                    pauseMenuUI.SetActive(false); // Hide the pause menu
                    controlMenuUI.SetActive(false); 
                    intructionsMenuUI.SetActive(false);
                    optionsMenuUI.SetActive(false); 
                }
            }
        }
    }
    public void Quit()
    {

       SceneManager.LoadScene("title");
       Time.timeScale = 1;

    }
    public void Play()
    {
        Time.timeScale = 1;
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false); // Hide the pause menu
            isPaused = false;
                    }
    }
}

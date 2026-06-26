using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleScreen : MonoBehaviour
{
   public int wizzard = 0;
   public int Warrior = 0;
   public int play = 0;

      void Awake()
      {
         DontDestroyOnLoad(gameObject);
         SceneManager.sceneLoaded += OnSceneLoaded;
      }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "title" && play ==1 )
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    } public void Play(){
      play++;
   SceneManager.LoadScene("Loading");
    Time.timeScale = 1f;
 }
 public void Quit(){
    Application.Quit();

 }
 public void Tutorial(){
    SceneManager.LoadScene("Loading tutorial");
 }
 public void wizardChosen(){
    wizzard = 1;
 }
 public void WarriorChosen(){
      Warrior = 1;
   }
   public void ResetChoices(){
    wizzard = 0;
      Warrior = 0;
   }
 
 
 
 
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
}
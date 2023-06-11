using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arden
{     
    public class MainMenu : MonoBehaviour
    {
        public LevelLoader levelloader;
        public void PlayGame()
        {
            levelloader.LoadNextLevel();
            
        }

        public void QuitGame()
        {
            Debug.Log("QUIT!");
            Application.Quit();
        }
        private void Start()
        {
      
        }
    }
}  

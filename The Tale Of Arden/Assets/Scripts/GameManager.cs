using System.Collections;
using System.Collections.Generic;
using Arden.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Arden
{
    public class GameManager : Singleton<GameManager>
    {

        [Header("Player")] 
        [SerializeField] private PlayerParent player;
        
        
        [Space]
        [SerializeField] private Vector2 spawnPoint;

        [Header("UI Methods")]
        [SerializeField] private Animator fadeInOutUI;


        #region Game Over Methods

        public void SetSpawnPos(Vector2 _pos)
        {
            spawnPoint = _pos;
        }

        public void RestartGame()
        {
            StartCoroutine(StartGame());
        }
        void SpawnPlayer()
        {
            fadeInOutUI.SetTrigger("Fade Out");
            player.transform.position = spawnPoint;
            PlayerParent.PlayerStatManager.HealFull();
        }
        void GameOver()
        {
            fadeInOutUI.SetTrigger("Fade In");
        }

        IEnumerator StartGame()
        {
            GameOver();
            yield return new WaitForSeconds(1);
            SpawnPlayer();
        }

        #endregion
        

    }
}

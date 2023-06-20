using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Arden.Player
{
    public class PlayerStatManager : MonoBehaviour
    {
        [SerializeField] private int health;
        [SerializeField] private TextMeshProUGUI healthBar;
        [SerializeField] private GameObject healthDebugIndicator;
        private int maxHealth;

        private PlayerSoundManager playerSoundManager;
        
        void Start()
        {
            healthBar.text = health.ToString();
            maxHealth = health;
            playerSoundManager = GetComponent<PlayerSoundManager>();
        }

        public void TakeDamage()
        {
            if (health - 1 <= 0)
            {
                PlayerParent.PlayerAnimationManager.PlayDeathAnimation();
                GameManager.Instance.RestartGame();
                
            }
            
            health--;
            healthBar.text = health.ToString();
            StartCoroutine(StartIndicator());
            playerSoundManager.PlayHitSound();
        }

        public void HealFull()
        {
            health = maxHealth;
            healthBar.text = health.ToString();
        }
        IEnumerator StartIndicator()
        {
            healthDebugIndicator.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            healthDebugIndicator.SetActive(false);
        }

    }
}

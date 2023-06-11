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
        
        void Start()
        {
            healthBar.text = health.ToString();
        }

        public void TakeDamage()
        {
            health--;
            healthBar.text = health.ToString();
            StartCoroutine(StartIndicator());
        }

        IEnumerator StartIndicator()
        {
            healthDebugIndicator.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            healthDebugIndicator.SetActive(false);
        }

    }
}

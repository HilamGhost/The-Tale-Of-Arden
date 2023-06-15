using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Arden.Event
{
    public class CorruptedWord : MonoBehaviour
    {
        public bool IsCorrupted;
        public string FullWord;
        private TMP_Text wordText;
        private BoxCollider2D wordCollider;
        private Rigidbody2D wordRB;
        private string currentWord;

        private bool corruptApplied;

        private CorruptedTextTrigger _corruptedTextTrigger;
        

        public string CurrentWord
        { 
            get
            {
                return currentWord;
            }
            set
            {
                currentWord = value;
                SetTextChange();
            }
        }

        private void Start()
        {
            wordText = GetComponentInChildren<TMP_Text>();
            wordCollider = GetComponent<BoxCollider2D>();
            wordRB = GetComponent<Rigidbody2D>();
            _corruptedTextTrigger = GetComponentInParent<CorruptedTextTrigger>();
        }

        void SetTextChange()
        {
            wordText.text = CurrentWord;
        }

        public void SetWord(string _word)
        {
            FullWord = _word;
            wordCollider.size = TextLenghtData.WantedWord(_word.Length);
            wordText.rectTransform.sizeDelta = TextLenghtData.WantedWord(_word.Length);
        }

        public void SetCorrupted()
        {
            wordText.faceColor = Color.red;
            IsCorrupted = true;
            
        }

        void ApplyCorrupt()
        {
            if(corruptApplied) return;
            corruptApplied = true;

            wordCollider.isTrigger = true;
            _corruptedTextTrigger.TransformObject();

            Debug.Log("Corrupt Applied");
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.CompareTag("Player"))
            {
                if(!IsCorrupted) return;
                ApplyCorrupt();
            }
        }
    }
}

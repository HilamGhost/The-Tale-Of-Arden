using System.Collections;
using System.Collections.Generic;
using Arden.Player.State;
using UnityEngine;

namespace Arden.Player
{
    public class PlayerHoldManager
    {
        private HoldProperties holdPropterty;
        
        private PlayerController playerController;
        private Transform player;
        
        HoldableObject holdableObject;
        private Rigidbody2D holdableRB;
        private Rigidbody2D playerRB;
        public PlayerHoldManager(PlayerController _playerController, HoldProperties _holdProperties)
        {
            playerController = _playerController;
            holdPropterty = _holdProperties;
            player = playerController.transform;
            playerRB = playerController.GetComponent<Rigidbody2D>();


        }

        public HoldableObject FindHoldableObject(Vector2 _playerPos,Vector2 _direction)
        {
            RaycastHit2D _holdObjectHit = Physics2D.Raycast(_playerPos,_direction,holdPropterty.holdDedectionRange,holdPropterty.holdableLayer);
            HoldableObject holdableObject = null;
            if (_holdObjectHit)
            {
                if (_holdObjectHit.transform.TryGetComponent(out HoldableObject _holdableObject))
                {
                    holdableObject = _holdableObject;
                }
            }

            return holdableObject;
        }

        public void ConnectHoldObject(HoldableObject _holdableObject)
        {
            player.transform.parent = _holdableObject.transform;
            holdableObject = _holdableObject;
            holdableRB = holdableObject.GetComponent<Rigidbody2D>();
        }

        public void HoldObject(float _input)
        {
            float _moveValue = _input * holdPropterty.holdSpeed; 
            Vector2 _movementVector = Vector2.right * _moveValue + Vector2.up * holdableRB.velocity.y;
            holdableRB.velocity = _movementVector;
            playerRB.velocity = _movementVector;
        }
        public void RemoveHoldObject()
        {
            player.transform.parent = null;
            holdableObject = null;
            holdableRB = null;
        }
    }
        
    [System.Serializable]
    public class HoldProperties
    {
        public float holdSpeed;
        public LayerMask holdableLayer;
        public float holdDedectionRange;
        public Vector3 holdOffset;
    }
       
}


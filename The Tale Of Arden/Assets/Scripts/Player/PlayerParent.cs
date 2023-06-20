using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arden.Player
{
    public class PlayerParent : Singleton<PlayerParent>
    {
        static PlayerController playerController;
        static PlayerInputController playerInput;
        static PlayerStateManager playerStateManager;
        private static PlayerSoundManager playerSoundManager;
        private static PlayerAnimationManager _playerAnimationManager;
        private static PlayerAttackManager playerAttackManager;
        private static PlayerStatManager playerStatManager;
        
        private Animator playerAnimator;
        
        public static PlayerController PlayerController => playerController;
        public static PlayerInputController PlayerInput => playerInput;
        public static PlayerStateManager PlayerStateManager => playerStateManager;
        public static PlayerSoundManager PlayerSoundManager => playerSoundManager;
        public static PlayerAnimationManager PlayerAnimationManager => _playerAnimationManager;
        public static PlayerAttackManager PlayerAttackManager => playerAttackManager;

        public static PlayerStatManager PlayerStatManager => playerStatManager;

        protected override void Awake()
        {
            base.Awake();
            MakeAssignments();
        }

        private void Update()
        {
            playerStateManager.OnStateUpdate();
        }

        private void FixedUpdate()
        {
            playerStateManager.OnStateFixedUpdate();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            PlayerStateManager.OnStateCollideEnter(col);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            PlayerStateManager.OnStateCollideExit(other);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerStateManager.OnStateTriggerEnter(collision);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            PlayerStateManager.OnStateTriggerExit(collision);
        }
        void MakeAssignments()
        {
            playerAnimator = GetComponent<Animator>();
            
            playerController = GetComponent<PlayerController>();
            playerInput = PlayerInputController.Instance;
            playerSoundManager = GetComponent<PlayerSoundManager>();
            playerAttackManager = GetComponent<PlayerAttackManager>();
            _playerAnimationManager = new PlayerAnimationManager(playerAnimator);
            playerStatManager = GetComponent<PlayerStatManager>();
            
            playerStateManager = new PlayerStateManager();

            playerInput.AssignInputManager(playerStateManager);
        }
    }
}

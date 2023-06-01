using UnityEngine;
using UnityEngine.InputSystem;

namespace Arden.Player.State
{
    public abstract class PlayerState 
    {
      
        #region State Methods

        public virtual void OnStateStart() {}
        public virtual void OnStateUpdate() {}

        public virtual void OnStateFixedUpdate(){}

        public virtual void OnStateExit(){}
   

        public virtual void OnStateCollideEnter(Collision2D collision){}
        
        public virtual void OnStateCollideExit(Collision2D collision){}
     

        public virtual void OnStateTriggerEnter(Collider2D collision){}
      

        public virtual void OnStateTriggerExit(Collider2D collision){}
  
        #endregion

        #region Input Methods
        public virtual void OnMove(InputAction.CallbackContext _context){}


        public virtual void OnLook(InputAction.CallbackContext _context){}
   

        public virtual void OnJump(InputAction.CallbackContext _context){}
        public virtual void OnDash(InputAction.CallbackContext context){}

        #endregion

       
    }
}

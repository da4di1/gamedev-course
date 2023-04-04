using Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InputReader
{
    public class ExternalDeviceInputReader : IEntityInputSource
    {
        public float HorizontalDirection => Input.GetAxisRaw("Horizontal");
        public bool IsJumping { get; private set; }
        public bool IsAttacking { get; private set; }

        
        public void OnUpdate()
        {
            if (Input.GetButtonDown("Jump"))
            {
                IsJumping = true;
            }

            if (!IsPointerOverUI() && Input.GetButtonDown("Fire1"))
            {
                IsAttacking = true;
            }
        }
        
        public void ResetOneTimeActions()
        {
            IsJumping = false;
            IsAttacking = false;
        }

        private bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();
    }
}

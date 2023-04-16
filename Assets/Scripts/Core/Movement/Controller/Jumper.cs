using Core.Movement.Data;
using UnityEngine;

namespace Core.Movement.Controller
{
    public class Jumper
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly JumpData _jumpData;
        
        public bool IsJumping { get; private set; }
        public bool IsLanding { get; private set; }


        public Jumper(Rigidbody2D rigidbody, JumpData jumpData)
        {
            _rigidbody = rigidbody;
            _jumpData = jumpData;
        }
        
        public bool StartJump()
        {
            if (IsJumping || IsLanding)
                return false;

            IsJumping = true;
            
            _rigidbody.AddForce(Vector2.up * _jumpData.JumpingForce);
            return true;
        }

        public bool GetOnGround(Collision2D ground)
        {
            if (ground.transform.CompareTag("Ground"))
            {
                IsJumping = false;
                IsLanding = false;
                return true;
            }
            return false;
        }

        public bool GetOffGround(Collision2D ground)
        {
            if (ground.transform.CompareTag("Ground"))
            {
                return true;
            }
            return false;
        }

        public void StartLanding() => IsLanding = true;
    }
}
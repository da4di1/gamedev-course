using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player{

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerHandler : MonoBehaviour
    {
        [Header("HorizontalMovement")]
        [SerializeField] private float _movingSpeed = 190f;

        [Header("Jumping")]
        [SerializeField] private float _jumpingForce = 100f;

        private Rigidbody2D _rigidbody;

        private bool _faceRight = true;
        private bool _isJumping = false;


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            
        }

        private void OnCollisionEnter2D(Collision2D other) 
        {
            if (other.transform.tag == "Ground")
            {
                _isJumping = false;
            }
        }

        private void OnCollisionExit2D(Collision2D other) 
        {
            if (other.transform.tag == "Ground")
            {
                _isJumping = true;
            }
        }


        public void MoveHorizontally(float direction)
        {
            SetDirection(direction);
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = direction * _movingSpeed * Time.deltaTime;
            _rigidbody.velocity = velocity;
        }

        public void Jump()
        {
            if (_isJumping) { return; }

            _rigidbody.AddForce(Vector2.up * _jumpingForce);
        }


        private void SetDirection(float direction)
        {
            if ((_faceRight && direction < 0) || (!_faceRight && direction > 0))
            {
                FlipSide();
            }
        }

        private void FlipSide()
        {
            transform.Rotate(0, 180, 0);
            _faceRight = !_faceRight;
        }
    }

}

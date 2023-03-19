using System;
using System.Collections;
using System.Collections.Generic;
using Camera;
using Core.Enums;
using Core.Tools;
using UnityEngine;

namespace Player{

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerHandler : MonoBehaviour
    {
        [Header("HorizontalMovement")]
        [SerializeField] private float _movingSpeed = 190f;

        [Header("Jumping")]
        [SerializeField] private float _jumpingForce = 100f;

        [SerializeField] private Cameras _cameras;

        private Rigidbody2D _rigidbody;
        
        private bool _isJumping;
        private Direction _faceDirection = Direction.Right;


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D other) 
        {
            if (other.transform.CompareTag("Ground"))
            {
                _isJumping = false;
            }
        }

        private void OnCollisionExit2D(Collision2D other) 
        {
            if (other.transform.CompareTag("Ground"))
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
            if ((_faceDirection == Direction.Right && direction < 0) || 
                (_faceDirection == Direction.Left && direction > 0))
            {
                FlipSide();
                FlipCameras();
            }
        }

        private void FlipSide()
        {
            transform.Rotate(0, 180, 0);
            _faceDirection = _faceDirection == Direction.Right ? Direction.Left : Direction.Right;
        }

        
        public void FlipCameras()
        {
            if (_cameras.StartCamera.enabled || _cameras.FinalCamera.enabled) return;

            foreach (var cameraPair in _cameras.DirectionalCameras)
                cameraPair.Value.enabled = _faceDirection == cameraPair.Key;
        }
    }

}

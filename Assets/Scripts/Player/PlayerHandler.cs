using System;
using Core.Enums;
using Core.Tools;
using Player.PlayerAnimation;
using UnityEngine;

namespace Player{

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerHandler : MonoBehaviour
    {
        [Header("HorizontalMovement")] 
        public float MovingSpeed = 230f;

        [Header("Jumping")] 
        [SerializeField] private float _jumpingForce = 270f;
        
        [SerializeField] private AnimatorController _animator;
        [SerializeField] private Cameras _cameras;

        private Rigidbody2D _rigidbody;

        private bool _isJumping;
        private bool _isLanding;
        private float _direction;
        private Direction _faceDirection = Direction.Right;


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            UpdateAnimations();
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.transform.CompareTag("Ground"))
            {
                _isJumping = false;
                _isLanding = false;
                CancelInvoke(nameof(StartLanding));
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.transform.CompareTag("Ground"))
            {
                Invoke(nameof(StartLanding), 0.3f);
            }
        }

        public void MoveHorizontally(float direction)
        {
            _direction = direction;
            SetDirection(direction);
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = direction * MovingSpeed * Time.deltaTime;
            _rigidbody.velocity = velocity;
        }

        public void Jump()
        {
            if (_isJumping)
                return;

            _isJumping = true;

            if (!_animator.PlayAnimation(AnimationType.Jump, _isJumping))
                return;
            
            _animator.ActionEnded += EndJump;
            _rigidbody.AddForce(Vector2.up * _jumpingForce);
        }

        public void StartAttack()
        {
            if (!_animator.PlayAnimation(AnimationType.Attack, true))
                return;

            _animator.ActionRequested += Attack;
            _animator.ActionEnded += EndAttack;
        }

        public void FlipCameras()
        {
            if (_cameras.StartCamera.enabled || _cameras.FinalCamera.enabled) return;

            foreach (var cameraPair in _cameras.DirectionalCameras)
                cameraPair.Value.enabled = _faceDirection == cameraPair.Key;
        }

        private void UpdateAnimations()
        {
            _animator.PlayAnimation(AnimationType.Land, _isLanding);
            _animator.PlayAnimation(AnimationType.Run, _direction != 0);
            _animator.PlayAnimation(AnimationType.Idle, true);
        }

        private void StartLanding() => _isLanding = true;

        private void EndJump()
        {
            _animator.ActionEnded -= EndJump;
            StartLanding();
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

        private void Attack()
        {
            Debug.Log("Attack has been committed");
        }

        private void EndAttack()
        {
            _animator.ActionRequested -= Attack;
            _animator.ActionEnded -= EndAttack;
            _animator.PlayAnimation(AnimationType.Attack, false);
        }
    }

}

using Core.Animations;
using Core.Enums;
using Core.Movement.Controller;
using Core.Movement.Data;
using Core.Tools;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player{

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntityHandler : MonoBehaviour
    {
        public HorizontalMovementData MovementData;
        
        [SerializeField] private JumpData _jumpData;
        [SerializeField] private AnimatorController _animator;
        [SerializeField] private Cameras _cameras;

        private Rigidbody2D _rigidbody;

        private Mover _mover;
        private Jumper _jumper;


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _mover = new Mover(_rigidbody, MovementData);
            _jumper = new Jumper(_rigidbody, _jumpData);
        }

        private void Update()
        {
            UpdateAnimations();
            UpdateCameras();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_jumper.StayOnGround(other))
                CancelInvoke(nameof(StartLanding));
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (_jumper.GetOffGround(other))
                Invoke(nameof(StartLanding), 0.4f);
        }

        public void MoveHorizontally(float direction) => _mover.MoveHorizontally(direction);

        public void Jump()
        {
            if (!_jumper.StartJump())
                return;
            
            if (!_animator.PlayAnimation(AnimationType.Jump, _jumper.IsJumping))
                return;
            
            _animator.ActionEnded += EndJump;
        }

        public void StartAttack()
        {
            if (!_animator.PlayAnimation(AnimationType.Attack, true))
                return;

            _animator.ActionRequested += Attack;
            _animator.ActionEnded += EndAttack;
        }

        public void UpdateCameras()
        {
            if (_cameras.StartCamera.enabled || _cameras.FinalCamera.enabled) return;

            foreach (var cameraPair in _cameras.DirectionalCameras)
                cameraPair.Value.enabled = _mover.FaceDirection == cameraPair.Key;
        }

        private void UpdateAnimations()
        {
            _animator.PlayAnimation(AnimationType.Land, _jumper.IsLanding);
            _animator.PlayAnimation(AnimationType.Run, _mover.IsMoving);
            _animator.PlayAnimation(AnimationType.Idle, true);
        }

        private void EndJump()
        {
            _animator.ActionEnded -= EndJump;
            StartLanding();
        }
        
        private void StartLanding() => _jumper.StartLanding();
        
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

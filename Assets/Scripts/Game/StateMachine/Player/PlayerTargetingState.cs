using UnityEngine;

namespace Game.StateMachine.Player
{
    public class PlayerTargetingState : PlayerBaseState
    {
        private static readonly int _targetingBlendTree = Animator.StringToHash("Targeting");
        private static readonly int _targetingForward = Animator.StringToHash("TargetingForward");
        private static readonly int _targetingRight = Animator.StringToHash("TargetingRight");
        public PlayerTargetingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            PlayerStateMachine.Animator.Play(_targetingBlendTree);
            SubscribeInputEvents();
        }

        public override void Tick()
        {
            if (PlayerStateMachine.Targeter.CurrentTarget is null)
            {
                PlayerStateMachine.SwitchState(new PlayerMovementState(PlayerStateMachine));
                return;
            }

            var direction = CalculateMovement();
            Move(direction * PlayerStateMachine.MovementConfig.MovementSpeed);
            PreformAnimation();
            FaceTarget();
        }

        public override void Exit()
        {
            UnsubscribeInputEvents();
            PlayerStateMachine.Targeter.ResetCurrentTarget();
        }

        private void SubscribeInputEvents()
        {
            PlayerStateMachine.InputService.OnLockUnlockTargetEvent += OnLockUnlockTargetHandler;
            PlayerStateMachine.InputService.OnAttackEvent += OnAttackEventHandler;
        }

        private void UnsubscribeInputEvents()
        {
            PlayerStateMachine.InputService.OnLockUnlockTargetEvent -= OnLockUnlockTargetHandler;
            PlayerStateMachine.InputService.OnAttackEvent -= OnAttackEventHandler;
        }

        private void OnAttackEventHandler()
        {
            PlayerStateMachine.SwitchState(new PlayerAttackingState(PlayerStateMachine, 0));
        }

        private void OnLockUnlockTargetHandler()
        {
            PlayerStateMachine.SwitchState(new PlayerMovementState(PlayerStateMachine));
        }
        
        private void PreformAnimation()
        {
            var movementValue = PlayerStateMachine.InputService.MovementValue;
            
            if (movementValue.y == 0)
            {
                PlayerStateMachine.Animator.SetFloat(_targetingForward, 0, 0.1f, Time.deltaTime);
            }
            else
            {
                PlayerStateMachine.Animator.SetFloat(_targetingForward, movementValue.y > 0 ? 1f : -1f, 0.1f, Time.deltaTime);
            }
            
            if (movementValue.x == 0)
            {
                PlayerStateMachine.Animator.SetFloat(_targetingRight, 0, 0.1f, Time.deltaTime);
            }
            else
            {
                PlayerStateMachine.Animator.SetFloat(_targetingRight, movementValue.x > 0 ? 1f : -1f, 0.1f, Time.deltaTime);
            }
        }

        private Vector3 CalculateMovement()
        {
            var movement = new Vector3();
            var transform = PlayerStateMachine.transform;
            movement += transform.right * PlayerStateMachine.InputService.MovementValue.x;
            movement += transform.forward * PlayerStateMachine.InputService.MovementValue.y;
            return movement;
        }
    }
}
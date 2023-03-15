using Game.CombatSystem.Data;
using UnityEngine;

namespace Game.StateMachine.Player
{
    public class PlayerAttackingState : PlayerBaseState
    {
        private const string ATTACK_ANIMATION_TAG = "Attack";

        private readonly int _attackIndex;
        private readonly AttackData _attackData;
        private bool _attackForceApplied;

        public PlayerAttackingState(PlayerStateMachine playerStateMachine, int attackIndex) : base(playerStateMachine)
        {
            if (_attackIndex < 0 || _attackIndex > PlayerStateMachine.AttackDatas.Length - 1) return;
            _attackIndex = attackIndex;
            _attackData = PlayerStateMachine.AttackDatas[_attackIndex];
        }

        public override void Enter()
        {
            SubscribeEvents();
            StartAttackAnimation();
        }

        public override void Tick()
        {
            float normalizedAttackTime = GetNormalizedTime();
            Move();
            FaceTarget();
            if (normalizedAttackTime > _attackData.ForceApplyTime) TryApplyForce();
            if (normalizedAttackTime < 1) return;

            if (PlayerStateMachine.Targeter.CurrentTarget is not null)
            {
                PlayerStateMachine.SwitchState(new PlayerTargetingState(PlayerStateMachine));
            }
            else
            {
                PlayerStateMachine.SwitchState(new PlayerMovementState(PlayerStateMachine));
            }
        }

        public override void Exit() => UnsubscribeEvents();

        private void SubscribeEvents() => PlayerStateMachine.InputService.OnAttackEvent += OnAttackEventHandler;

        private void UnsubscribeEvents() => PlayerStateMachine.InputService.OnAttackEvent -= OnAttackEventHandler;

        private void TryApplyForce()
        {
            if (_attackForceApplied) return;
            PlayerStateMachine.ForcesReceiver.AddForce(PlayerStateMachine.transform.forward * _attackData.Force);
            _attackForceApplied = true;
        }

        private void StartAttackAnimation()
        {
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_attackData.AnimationName,
                _attackData.TransitionDuration); //todo from config!
        }

        private void OnAttackEventHandler()
        {
            var normalizedAttackTime = GetNormalizedTime();
            TryComboAttack(normalizedAttackTime);
        }

        private void TryComboAttack(float normalizedAttackTime)
        {
            if (_attackData.ComboStateIndex == -1) return;
            if (normalizedAttackTime < _attackData.ComboAttackTime) return;
            PlayerStateMachine.SwitchState(new PlayerAttackingState(PlayerStateMachine, _attackData.ComboStateIndex));
        }

        private float GetNormalizedTime()
        {
            AnimatorStateInfo currentInfo = PlayerStateMachine.Animator.GetCurrentAnimatorStateInfo(0);
            AnimatorStateInfo nextInfo = PlayerStateMachine.Animator.GetNextAnimatorStateInfo(0);
            if (PlayerStateMachine.Animator.IsInTransition(0) && nextInfo.IsTag(ATTACK_ANIMATION_TAG))
            {
                return nextInfo.normalizedTime;
            }

            if (!currentInfo.IsName(_attackData.AnimationName))
            {
                return nextInfo.normalizedTime;
            }

            if (!PlayerStateMachine.Animator.IsInTransition(0) && currentInfo.IsTag(ATTACK_ANIMATION_TAG))
            {
                return currentInfo.normalizedTime;
            }

            return 0f;
        }
    }
}
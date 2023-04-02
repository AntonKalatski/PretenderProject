using Game.CombatSystem.Data;

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
            SetAttackSettings();
            StartAttackAnimation();
        }

        public override void Tick()
        {
            var animator = PlayerStateMachine.Animator;
            float normalizedAttackTime = GetNormalizedTime(animator, ATTACK_ANIMATION_TAG, _attackData.AnimationName);
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

        private void SetAttackSettings()
        {
            PlayerStateMachine.WeaponHandler.SetWeaponDamage(_attackData.Damage, _attackData.KnockBack);
        }

        private void StartAttackAnimation()
        {
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_attackData.AnimationName,
                _attackData.TransitionDuration); //todo from config!
        }

        private void OnAttackEventHandler()
        {
            var animator = PlayerStateMachine.Animator;
            var normalizedAttackTime = GetNormalizedTime(animator, ATTACK_ANIMATION_TAG, _attackData.AnimationName);
            TryComboAttack(normalizedAttackTime);
        }

        private void TryComboAttack(float normalizedAttackTime)
        {
            if (_attackData.ComboStateIndex == -1) return;
            if (normalizedAttackTime < _attackData.ComboAttackTime) return;
            PlayerStateMachine.SwitchState(new PlayerAttackingState(PlayerStateMachine, _attackData.ComboStateIndex));
        }
    }
}
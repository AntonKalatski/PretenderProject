using UnityEngine;

namespace Game.StateMachine.Player
{
    public class PlayerImpactDamageState : PlayerBaseState
    {
        private readonly int _damageImpact = Animator.StringToHash("DamageImpact");
        private float _duration = 1f;

        public PlayerImpactDamageState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_damageImpact,
                0.1f); //TODO this duration have to be in config
        }

        public override void Tick()
        {
            Move();
            _duration -= Time.deltaTime;
            if (_duration <= 0) ReturnToState();
        }

        public override void Exit()
        {
        }
    }
}
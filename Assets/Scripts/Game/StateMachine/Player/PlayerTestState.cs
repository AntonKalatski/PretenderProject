using UnityEngine;

namespace Game.StateMachine.Player
{
    public class PlayerTestState : PlayerBaseState
    {
        private float timer = 5f;

        public PlayerTestState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            Debug.Log(nameof(Enter));
        }

        public override void Tick()
        {
            timer -= Time.deltaTime;
            Debug.Log(timer);
            if (timer <= 0) PlayerStateMachine.SwitchState(new PlayerTestState(PlayerStateMachine));
        }

        public override void Exit()
        {
            Debug.Log(nameof(Exit));
        }
    }
}
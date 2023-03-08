using UnityEngine;
using Modules.StateMachine;

namespace Game.StateMachine.Player
{
    public abstract class PlayerBaseState : BaseState
    {
        protected readonly PlayerStateMachine PlayerStateMachine;
        protected PlayerBaseState(PlayerStateMachine playerStateMachine) => PlayerStateMachine = playerStateMachine;

        protected void Move(Vector3 direction) => PlayerStateMachine.CharacterController.Move(direction + PlayerStateMachine.ForcesReceiver.Movement);
    }
}
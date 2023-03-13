using UnityEngine;
using Modules.StateMachine;

namespace Game.StateMachine.Player
{
    public abstract class PlayerBaseState : BaseState
    {
        protected readonly PlayerStateMachine PlayerStateMachine;
        protected PlayerBaseState(PlayerStateMachine playerStateMachine) => PlayerStateMachine = playerStateMachine;

        protected void Move()
        {
            Move(Vector3.zero);
        }

        protected void Move(Vector3 direction)
        {
            PlayerStateMachine.CharacterController.Move((direction + PlayerStateMachine.ForcesReceiver.Movement) * Time.deltaTime);
        }

        protected void FaceTarget()
        {
            if (PlayerStateMachine.Targeter.CurrentTarget is null) return;
            var lookDirection = PlayerStateMachine.Targeter.CurrentTarget.transform.position -
                                PlayerStateMachine.transform.position;
            if (lookDirection == Vector3.zero) return;
            lookDirection.y = 0;
            PlayerStateMachine.transform.rotation = Quaternion.Lerp(PlayerStateMachine.transform.rotation,
                Quaternion.LookRotation(lookDirection),
                PlayerStateMachine.MovementConfig.RotationSpeed * Time.deltaTime);
        }
    }
}
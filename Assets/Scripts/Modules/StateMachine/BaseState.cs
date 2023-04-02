using UnityEngine;

namespace Modules.StateMachine
{
    public abstract class BaseState
    {
        public abstract void Enter();
        public abstract void Tick();
        public abstract void Exit();

        protected float GetNormalizedTime(Animator animator, string animationTag, string animationName)
        {
            AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
            AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

            if (!currentInfo.IsName(animationName)) return nextInfo.normalizedTime;
            if (animator.IsInTransition(0) && nextInfo.IsTag(animationTag)) return nextInfo.normalizedTime;
            if (!animator.IsInTransition(0) && currentInfo.IsTag(animationTag)) return currentInfo.normalizedTime;
            return 0f;
        }
    }
}
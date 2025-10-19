using UnityEngine;

public static class AnimatorExtensions
{
    private const string Idle_State_Name = "Idle";
    private static readonly int IdleStateHash = Animator.StringToHash(Idle_State_Name);

    public static void TrySetTrigger(this Animator animator, int triggerHash)
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.shortNameHash == IdleStateHash)
        {
            animator.SetTrigger(triggerHash);
        }
    }
}
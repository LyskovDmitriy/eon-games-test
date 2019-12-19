using UnityEngine;

public class StrongAttackBehavior : StateMachineBehaviour
{
    private const string StrongAttackName = "StrongAttack";

    private bool isAttacking;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetMouseButtonDown(1) && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger(StrongAttackName);
        }
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isAttacking = false;

        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}

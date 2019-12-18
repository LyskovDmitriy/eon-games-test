using UnityEngine;

public class LightAttackBehavior : StateMachineBehaviour
{
    private const string LightAttackIndexName = "LightAttackIndex";
    private const string LightAttackTriggerName = "LightAttack";

    [SerializeField] private int animationsCount = default;

    private bool isAttacking;


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger(LightAttackTriggerName);
            animator.SetInteger(LightAttackIndexName.Length, Random.Range(0, animationsCount));
        }

        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isAttacking = false;

        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}

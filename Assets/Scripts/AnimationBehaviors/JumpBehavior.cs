using UnityEngine;

public class JumpBehavior : StateMachineBehaviour
{
    private const string JumpTriggerName = "Jump";

    [SerializeField] private KeyCode jumpKey = default;


    private bool isJumping;


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKeyDown(jumpKey) && !isJumping)
        {
            animator.SetTrigger(JumpTriggerName);
            isJumping = true;
        }

        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isJumping = false;

        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}

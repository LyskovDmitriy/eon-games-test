using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehavior : StateMachineBehaviour
{
    private const string JumpTriggerName = "Jump";

    [SerializeField] private KeyCode jumpKey = default;


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKeyDown(jumpKey))
        {
            animator.SetTrigger(JumpTriggerName);
        }

        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }
}

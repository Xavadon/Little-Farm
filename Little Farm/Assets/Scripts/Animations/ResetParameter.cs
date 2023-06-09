using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetParameter : StateMachineBehaviour
{
    [SerializeField] private string _parameterName;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(_parameterName, false);
    }
}

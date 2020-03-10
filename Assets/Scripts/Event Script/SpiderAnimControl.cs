using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimControl : MonoBehaviour
{
    public Animator animator;

    public void StartIdle()
    {
        animator.SetTrigger("idle");
    }

    public void StartWalk()
    {
        animator.SetTrigger("walk");
    }

    public void StartAttack()
    {
        animator.SetTrigger("attack");
    }
}

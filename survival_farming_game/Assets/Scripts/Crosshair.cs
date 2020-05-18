using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private float gunAccuracy;


    [SerializeField]
    private GameObject go_CrosshairHUD;

    public void WalkingAnimation(bool _flag)
    {
        animator.SetBool("Walking", _flag);
    }


    public void RunningAnimation(bool _flag)
    {
        animator.SetBool("Running", _flag);
    }

    public void CrouchingAnimation(bool _flag)
    {
        animator.SetBool("Crouching", _flag);
    }

    public void FireAnimation()
    {
        if (animator.GetBool("Walking"))  
            animator.SetTrigger("Walk_Fire");
        else if (animator.GetBool("Crouching"))
            animator.SetTrigger("Crouch_Fire");
        else animator.SetTrigger("Idle_Fire");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

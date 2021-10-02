using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing : MonoBehaviour
{
    private Animator animator;
    private FixedJoint2D joint;
    private EdgeCollider2D collider;
    private float breakForce;
    private bool broken = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        collider = GetComponent<EdgeCollider2D>();
        joint = GetComponent<FixedJoint2D>();
        breakForce = joint.breakForce;
    }

    private void OnJointBreak2D(Joint2D j)
    {
        animator.Play("Extended");
        broken = true;
        transform.parent = null;
        animator.enabled = false;

        MainBody.wings.Remove(this);
        MainBody.joints.Remove(joint);
        MainBody.animators.Remove(animator);
        MainBody.breakForces.Remove(breakForce);
    }
}

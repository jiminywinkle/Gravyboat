using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Direction = WorldInfo.Direction;
using Color = WorldInfo.Color;

public class Wing : MonoBehaviour
{
    public Color color;
    public Animator animator;
    public FixedJoint2D joint;
    public EdgeCollider2D collider;
    public bool broken = false;
    public Direction direction;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        collider = GetComponent<EdgeCollider2D>();
        joint = GetComponent<FixedJoint2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MainBody.instance.Retract();
        MainBody.instance.stunTimer = 0;
        //Break();
    }

    /*
    public void Break()
    {
        Destroy(joint);
        animator.Play("Extended");
        broken = true;
        transform.parent = null;
        animator.enabled = false;
        if (MainBody.wings.Count == 0)
            MainBody.collider.radius = 3.36f;
        MainBody.wings.Remove(this);
        MainBody.joints.Remove(joint);
        MainBody.animators.Remove(animator);
    }
    */
}

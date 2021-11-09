using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Direction = WorldInfo.Direction;
using Color = WorldInfo.Color;

public class Wing : MonoBehaviour
{
    public Color color;
    public Animator animator;
    public FixedJoint2D joint;
    public PolygonCollider2D collider;
    public bool broken = false;
    public Direction direction;
    private Light2D light;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponentInChildren<Light2D>();
        light.intensity = 0;
        animator = GetComponentInChildren<Animator>();
        collider = GetComponent<PolygonCollider2D>();
        joint = GetComponent<FixedJoint2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MainBody.instance.Retract();
        MainBody.instance.stunTimer = 0;
        //Break();
    }

    public IEnumerator Alight()
    {
        light.intensity = 0;
        while (light.intensity < 15)
        {
            light.intensity += 64 * Time.deltaTime;
            yield return null;
        }
        light.intensity = 15;
        while (light.intensity > 0)
        {
            light.intensity -= 16 * Time.deltaTime;
            yield return null;
        }
        light.intensity = 0;
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

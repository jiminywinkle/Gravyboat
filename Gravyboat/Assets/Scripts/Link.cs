using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    private HingeJoint2D hinge;
    private Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        hinge = GetComponent<HingeJoint2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null)
        {
            if (collision.collider.tag == "Laser")
            {
                rigid.AddForce((transform.position - collision.transform.position) * 75, ForceMode2D.Impulse);
                Destroy(hinge);
            }
        }
    }
}

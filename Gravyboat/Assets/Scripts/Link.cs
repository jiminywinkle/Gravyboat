using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    public bool dead = false;
    public List<Link> links = new List<Link>();
    private HingeJoint2D hinge;
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private float alpha = 2;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        hinge = GetComponent<HingeJoint2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (dead)
        {
            alpha -= 1 * Time.deltaTime;
            if (alpha < 0)
                Destroy(gameObject);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null)
        {
            if (collision.collider.tag == "Laser")
            {
                rigid.AddForce((transform.position - collision.transform.position) * 75, ForceMode2D.Impulse);
                if (!dead)
                {
                    Destroy(hinge);
                    dead = true;
                    foreach (Link link in links)
                        link.dead = true;
                }
            }
        }
    }
}

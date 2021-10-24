using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LaserBeam : MonoBehaviour
{
    public float timer;
    private float initTimer;
    private bool enabled = true;
    private SpriteRenderer sprite;
    private PolygonCollider2D collider;
    private Light2D light;

    // Start is called before the first frame update
    void Start()
    {
        initTimer = timer;
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<PolygonCollider2D>();
        light = GetComponent<Light2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wing")
        {
            GameObject wing = collision.gameObject;
            wing.AddComponent<Rigidbody2D>();
            wing.transform.parent = null;
            MainBody.dead = true;
            collider.enabled = false;
        }
        else if (collision.tag == "Body")
        {
            MainBody.dead = true;
            collider.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            if (timer > 0)
            {
                timer -= 1 * Time.deltaTime;
            }
            else
            {
                timer = 0;
                enabled = false;
                StartCoroutine(Activate(false));
                collider.enabled = false;
            }
        }
        else
        {
            if (timer < initTimer)
            {
                timer += 1 * Time.deltaTime;
            }
            else
            {
                timer = initTimer;
                enabled = true;
                StartCoroutine(Activate(true));
                collider.enabled = true;
            }
        }
    }

    IEnumerator Activate(bool positive)
    {
        float alpha;
        if (positive)
        {
            alpha = 0;
            while (alpha < 1)
            {
                alpha += 2 * Time.deltaTime;
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
                light.intensity = alpha;
                yield return null;
            }
        }
        else
        {
            alpha = 1;
            while (alpha > 0)
            {
                alpha -= 2 * Time.deltaTime;
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
                light.intensity = alpha;
                yield return null;
            }
        }
    }
}

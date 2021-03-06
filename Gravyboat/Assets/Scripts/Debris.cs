using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    private float alpha = 2;
    private SpriteRenderer sprite;
    private AudioSource audioSrc;

    // Start is called before the first frame update

    private void Awake()
    {
        if (GetComponent<AudioSource>() != null)
        {
            audioSrc = GetComponent<AudioSource>();
        }
    }
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(-100, 100));
    }

    private void OnEnable()
    {
        if (audioSrc != null)
            audioSrc.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (alpha > 0 && gameObject.activeSelf)
        {
            alpha -= 1 * Time.deltaTime;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
        }
        else
            Destroy(gameObject);
    }
}

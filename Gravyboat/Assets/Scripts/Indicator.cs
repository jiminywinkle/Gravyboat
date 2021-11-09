using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;
using Direction = WorldInfo.Direction;

public class Indicator : MonoBehaviour
{
    public float countdown;
    public bool clickable;
    public Direction direction;
    public Vector3 startPos = Vector3.zero;
    public Vector3 startRot = Vector3.zero;
    public GameObject dummy;
    public GameObject icon;
    public Transform moveSpot;
    public List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    private AudioSource audioSrc;
    private Light2D light;

    private bool forward = true;
    private float moveTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponentInChildren<Light2D>();
        audioSrc = GetComponent<AudioSource>();

        transform.position = startPos;
        transform.eulerAngles = startRot;
        switch (direction)
        {
            case (Direction)0:
                dummy.transform.eulerAngles = Vector3.zero;
                break;
            case (Direction)1:
                dummy.transform.eulerAngles = new Vector3(0, 0, -180);
                break;
            case (Direction)2:
                dummy.transform.eulerAngles = new Vector3(0, 0, 90);
                break;
            case (Direction)3:
                dummy.transform.eulerAngles = new Vector3(0, 0, -90);
                break;
        }

        StartCoroutine(Move());
        if (countdown != 0)
            StartCoroutine(Begin());
    }

    IEnumerator Move()
    {
        while (true)
        {
            if (forward)
            {
                if (moveTimer < 1)
                    moveTimer += 1 * Time.deltaTime;
                else
                {
                    moveTimer = 1;
                    forward = false;
                }
            }
            else if (!forward)
            {
                if (moveTimer > 0)
                    moveTimer -= 1 * Time.deltaTime;
                else
                {
                    moveTimer = 0;
                    forward = true;
                }
            }
            icon.transform.position = new Vector3(Mathf.SmoothStep(startPos.x, moveSpot.position.x, moveTimer), Mathf.SmoothStep(startPos.y, moveSpot.position.y, moveTimer), 0);
            yield return null;
        }
    }

    IEnumerator Begin()
    {
        StartCoroutine(Flash());
        while (countdown > 0)
        {
            countdown -= 1 * Time.deltaTime;
            yield return null;
        }
        StartCoroutine(Die());
    }

    IEnumerator Flash()
    {
        while (true)
        {
            if (light.enabled)
                light.enabled = false;
            else
                light.enabled = true;
            yield return new WaitForSeconds(countdown / 10);
        }
    }

    IEnumerator Die()
    {
        audioSrc.Play();
        float alpha = 1;
        while (alpha > 0)
        {
            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
            }
            alpha -= 1 * Time.deltaTime;
            yield return null; 
        }
        Destroy(gameObject);
        MainBody.instance.controllable = true;
    }

    private void OnMouseDown()
    {
        if (clickable)
        {
            StartCoroutine(Die());
        }
    }
}

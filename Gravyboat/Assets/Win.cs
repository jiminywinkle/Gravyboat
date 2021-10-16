using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public Blackness black;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Body")
            black.Win();
    }
}

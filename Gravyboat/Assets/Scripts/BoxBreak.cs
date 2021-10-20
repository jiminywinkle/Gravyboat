using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBreak : MonoBehaviour
{
    private Rigidbody2D rigid;
    private GameObject[] children;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        children = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i).gameObject;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null)
        {
            if (collision.collider.tag == "Laser")
            {
                rigid.AddForce((transform.position - collision.transform.position) * 75, ForceMode2D.Impulse);
                for (int i = 0; i < children.Length; i++)
                {
                    children[i].transform.parent = null;
                    children[i].gameObject.SetActive(true);
                    Rigidbody2D childrig = children[i].GetComponent<Rigidbody2D>();
                    childrig.AddForce((transform.position - collision.transform.position) * 5, ForceMode2D.Impulse);
                    childrig.AddTorque(Random.Range(-5, 5));
                }
                gameObject.SetActive(false);
            }
        }
    }
}

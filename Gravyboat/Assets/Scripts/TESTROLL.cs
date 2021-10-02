using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTROLL : MonoBehaviour
{
    private Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponentInChildren<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("RollLeft"))
        {
            rigid.AddTorque(12f);
        }
        else if (Input.GetButton("RollRight"))
        {
            rigid.AddTorque(-12f);
        }
    }
}

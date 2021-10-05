using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Direction = WorldInfo.Direction;

public class Indicator : MonoBehaviour
{
    public Direction direction;
    public Vector3 startPos = Vector3.zero;
    public Vector3 startRot = Vector3.zero;
    public GameObject dummy;
    public GameObject icon;
    public Transform moveSpot;
    private SpriteRenderer sprite;

    private bool forward = true;
    private float moveTimer = 0;


    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
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
}

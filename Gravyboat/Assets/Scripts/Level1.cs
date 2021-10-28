using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level1 : MonoBehaviour
{
    public TextMeshPro text1;
    public TextMeshPro text2;
    public TextMeshPro text3;
    public TextMeshPro text4;
    private Vector3 gravyPos;

    // Start is called before the first frame update
    void Start()
    {
        text2.enabled = false;
        text3.enabled = false;
        text4.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        gravyPos = MainBody.instance.gameObject.transform.position;
        if (gravyPos.x < 150 && text1)
        {
            Destroy(text1);
        }
        if (text2)
        {
            if (gravyPos.x < 50)
            {
                text2.enabled = true;
            }
            if (gravyPos.y < 20)
            {
                text3.enabled = true;
                text4.enabled = true;
                Destroy(text2);
            }
        }
    }
}

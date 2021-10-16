using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    public GameObject hook;
    public GameObject link;
    public int linkCount;
    private GameObject startLink;
    private GameObject prevLink;
    private Vector3 startRot;

    // Start is called before the first frame update
    void Start()
    {
        // This needs to be done to spawn the links correctly, the desired rotation will be re-added after the generation
        startRot = transform.eulerAngles;
        transform.eulerAngles = Vector3.zero;

        startLink = transform.GetChild(0).gameObject;
        for (int i = 0; i < linkCount; i++)
        {
            if (i == 0)
                prevLink = startLink;
            GameObject newLink = Instantiate(link, new Vector3(prevLink.transform.position.x, prevLink.transform.position.y - 3.75f, 0), Quaternion.identity, transform);
            HingeJoint2D hinge = newLink.GetComponent<HingeJoint2D>();
            hinge.connectedBody = prevLink.GetComponent<Rigidbody2D>();
            hinge.anchor = new Vector2(0, 2);
            prevLink = newLink;
        }
        GameObject newHook = Instantiate(hook, new Vector3(prevLink.transform.position.x, prevLink.transform.position.y - 6, 0), Quaternion.identity, transform);
        HingeJoint2D hookHinge = newHook.GetComponent<HingeJoint2D>();
        hookHinge.connectedBody = prevLink.GetComponent<Rigidbody2D>();
        hookHinge.anchor = new Vector2(0, 4);

        transform.eulerAngles = startRot;
    }
}

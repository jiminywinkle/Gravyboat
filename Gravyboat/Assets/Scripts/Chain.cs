using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    public GameObject hook;
    public GameObject link;
    public GameObject carryObject;
    public Vector3 carryPlace;
    public int linkCount;
    public bool carrying = false;
    public bool hookEnd = true;
    private List<Link> links = new List<Link>();
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
        int i = 0;
        for (; i < linkCount; i++)
        {
            if (i == 0)
                prevLink = startLink;
            GameObject newLink = Instantiate(link, new Vector3(prevLink.transform.position.x, prevLink.transform.position.y - 3.75f, 0), Quaternion.identity, transform);
            Link linkLink = newLink.GetComponent<Link>();
            linkLink.index = i;
            linkLink.chain = this;
            links.Add(linkLink);
            HingeJoint2D hinge = newLink.GetComponent<HingeJoint2D>();
            hinge.connectedBody = prevLink.GetComponent<Rigidbody2D>();
            hinge.anchor = new Vector2(0, 2);
            prevLink = newLink;
        }

        for (int j = 0; j < links.Count - 1; j++)
        {
            for (int k = j + 1; k < links.Count; k++)
               links[j].links.Add(links[k]);
        }

        if (hookEnd)
        {
            GameObject newHook = Instantiate(hook, new Vector3(prevLink.transform.position.x, prevLink.transform.position.y - 6, 0), Quaternion.identity, transform);
            HingeJoint2D hookHinge = newHook.GetComponent<HingeJoint2D>();
            hookHinge.connectedBody = prevLink.GetComponent<Rigidbody2D>();
            hookHinge.anchor = new Vector2(0, 4);
            foreach (Link link in links)
                link.links.Add(newHook.GetComponent<Link>());

            if (carrying)
            {
                carryObject.transform.parent = newHook.transform;
                carryObject.transform.localPosition = carryPlace;
                carryObject.GetComponent<Rigidbody2D>().isKinematic = true;
                carryObject.layer = LayerMask.NameToLayer("HookCarry");
            }
        }

        transform.eulerAngles = startRot;
    }

    /// <summary>
    /// Remove all subsequent link references from every remaining link once a break has been made in the chain
    /// </summary>
    /// <param name="index"></param>
    public void Purge(int index)
    {
        for (int i = 0; i < index; i++)
        {
            Link linkLink = links[i].GetComponent<Link>();
            List<Link> newList = new List<Link>();
            for (int j = 0; j < index; j++)
            {
                newList.Add(links[j]);
            }
            linkLink.links = newList;
        }
    }
}

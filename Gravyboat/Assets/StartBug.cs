using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Color = WorldInfo.Color;

public class StartBug : MonoBehaviour
{
    public GameObject bug;
    public Color color;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null)
        {
            if (collision.collider.tag == "Obstacle")
            {
                Bug startBug = Instantiate(bug, new Vector3(transform.position.x, transform.position.y, -.1f), Quaternion.identity).GetComponent<Bug>();
                startBug.location = collision.gameObject;
                StartCoroutine(DelayChange(startBug));
            }
        }
    }

    /// <summary>
    /// Allow the bug to initialize before changing the color
    /// </summary>
    /// <param name="sB"></param>
    /// <returns></returns>
    IEnumerator DelayChange(Bug sB)
    {
        yield return null;
        sB.ForceColor(color);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Look : MonoBehaviour
{

    public float radius;
    public bool enabled;

    Vector3 mousePos = Vector3.zero;
    Vector3 direction = Vector3.zero;

    private float centeringProgress;

    // Update is called once per frame
    void Update()
    {
        if (enabled && !MainBody.dead)
        {
            mousePos = Input.mousePosition;
            mousePos.z = 0;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            direction = Vector3.ClampMagnitude(mousePos - transform.parent.position, radius);
            transform.position = transform.parent.position + direction;
        }
    }

    public void Center()
    {
        if (!enabled)
        {
            centeringProgress = 0;
            Vector3 currentPos = transform.localPosition;
            StartCoroutine(Centering(currentPos));
        }
    }

    IEnumerator Centering(Vector3 currentPos)
    {
        if (currentPos != transform.parent.position)
        {
            transform.localPosition = Vector3.Lerp(currentPos, transform.parent.position, centeringProgress);
            centeringProgress += Time.deltaTime;
            yield return null;
        }
    }
}

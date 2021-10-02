using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed;
    private float timer = 2;
    private Vector3 mousePos;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;
        direction = mousePos - MainBody.instance.pupil.transform.position;
        mousePos.x = mousePos.x - transform.position.x;
        mousePos.y = mousePos.y - transform.position.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void Update()
    {
        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }

        timer -= 1 * Time.deltaTime;

        Vector3 movement = new Vector3(direction.x, direction.y, 0).normalized;
        transform.position += movement * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}

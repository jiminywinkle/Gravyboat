using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        transform.localScale = Vector3.one;
        float width = render.sprite.bounds.size.x;
        float height = render.sprite.bounds.size.y;
        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        transform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height, 1);
    }
}

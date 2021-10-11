using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Blackness : MonoBehaviour
{
    private Image image;
    private float opacity = 1;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        StartCoroutine(Blackify(true));
    }

    public void Win()
    {
        image.enabled = true;
        opacity = 0;
        StartCoroutine(Blackify(false));
    }

    IEnumerator Blackify(bool intro)
    {
        if (intro)
        {
            while (opacity > 0)
            {
                opacity -= 1 * Time.deltaTime;
                image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
                yield return null;
            }
            image.enabled = false;
        }
        else
        {
            while (opacity < 1)
            {
                opacity += 1 * Time.deltaTime;
                image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
                yield return null;
            }
            MainBody.instance.Clean();
            SceneManager.LoadScene("Title");
        }
    }
}

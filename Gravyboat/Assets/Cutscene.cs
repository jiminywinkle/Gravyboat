using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    public SpriteRenderer image;
    public Sprite[] images;
    public Image black;
    private float blackAmount = 1;
    private float showTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        black.color = new Color(black.color.r, black.color.g, black.color.b, 1);
        StartCoroutine(Anim());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Title");
        }
    }

    IEnumerator Anim()
    {
        for (int i = 0; i < images.Length; i++)
        {
            image.sprite = images[i];
            while (blackAmount > 0)
            {
                blackAmount -= 1 * Time.deltaTime;
                black.color = new Color(black.color.r, black.color.g, black.color.b, blackAmount);
                yield return null;
            }
            showTime = 3;
            while (showTime > 0)
            {
                showTime -= 1 * Time.deltaTime;
                yield return null;
            }
            while (blackAmount < 1)
            {
                blackAmount += 1 * Time.deltaTime;
                black.color = new Color(black.color.r, black.color.g, black.color.b, blackAmount);
                yield return null;
            }
        }
        SceneManager.LoadScene("Title");
    }
}

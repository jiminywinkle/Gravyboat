using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image black;
    private float opacity = 1f;

    private void Start()
    {
        black.color = new Color(black.color.r, black.color.g, black.color.b, 1);
        StartCoroutine(Blackness());
    }

    IEnumerator Blackness()
    {
        while (opacity > 0)
        {
            opacity -= 1 * Time.deltaTime;
            black.color = new Color(black.color.r, black.color.g, black.color.b, opacity);
            yield return null;
        }
        Destroy(black.gameObject);
    }

    public void Begin()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Levels()
    {
        SceneManager.LoadScene("Levels");
    }

    public void Quit()
    {
        Application.Quit();
    }
}

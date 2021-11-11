using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image black;
    public AudioClip click;
    public GameObject levels;
    public static MainMenu instance;
    private float opacity = 1f;
    private AudioSource audioSrc;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        levels.SetActive(false);

        audioSrc = GetComponent<AudioSource>();
        audioSrc.loop = true;
        audioSrc.Play();
        black.color = new Color(black.color.r, black.color.g, black.color.b, 1);
        StartCoroutine(Blackness());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (levels.activeSelf)
                CloseLevels();
            else
                Quit();
        }
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
        if (!levels.activeSelf)
            StartCoroutine(Load("Level1"));
    }

    public void Levels()
    {
        if(!levels.activeSelf)
        {
            audioSrc.PlayOneShot(click);
            levels.SetActive(true);
        }
    }

    public void CloseLevels()
    {
        audioSrc.PlayOneShot(click);
        levels.SetActive(false);
    }

    public void Reset()
    {
        if (PlayerPrefs.HasKey("MaxLevel"))
        {
            PlayerPrefs.SetInt("MaxLevel", 0);
            Debug.Log("Progress Reset");
        }
        else
            Debug.Log("No Data to Reset");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        if(!levels.activeSelf)
            Application.Quit();
    }

    public IEnumerator Load(string level)
    {
        audioSrc.PlayOneShot(click);
        yield return new WaitForSeconds(.45f);
        SceneManager.LoadScene(level);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    public SpriteRenderer image;
    public Sprite[] images;
    public List<int> times = new List<int>();
    public List<AudioClip> clips = new List<AudioClip>();
    public Image black;
    public string location;
    private AudioSource audioSrc;
    private float blackAmount = 1;
    private float audioAmount = 0;
    private float showTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("MaxLevel"))
            PlayerPrefs.SetInt("MaxLevel", 0);
        else if (PlayerPrefs.GetInt("MaxLevel") < SceneManager.GetActiveScene().buildIndex)
            PlayerPrefs.SetInt("MaxLevel", SceneManager.GetActiveScene().buildIndex);

        black.color = new Color(black.color.r, black.color.g, black.color.b, 1);
        audioSrc = GetComponent<AudioSource>();
        StartCoroutine(Anim());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(location);
        }
    }

    IEnumerator Anim()
    {
        for (int i = 0; i < images.Length; i++)
        {
            audioSrc.volume = 0;
            audioSrc.PlayOneShot(clips[i]);
            image.sprite = images[i];
            while (blackAmount > 0)
            {
                blackAmount -= 1 * Time.deltaTime;
                black.color = new Color(black.color.r, black.color.g, black.color.b, blackAmount);
                audioAmount += .5f * Time.deltaTime;
                audioSrc.volume = audioAmount;
                yield return null;
            }
            showTime = times[i];
            while (showTime > 0)
            {
                showTime -= 1 * Time.deltaTime;
                yield return null;
            }
            while (blackAmount < 1)
            {
                blackAmount += 1 * Time.deltaTime;
                black.color = new Color(black.color.r, black.color.g, black.color.b, blackAmount);
                audioAmount -= .5f * Time.deltaTime;
                audioSrc.volume = audioAmount;
                yield return null;
            }
            audioSrc.Stop();
        }
        SceneManager.LoadScene(location);
    }
}

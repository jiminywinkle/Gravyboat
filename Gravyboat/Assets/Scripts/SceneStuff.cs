using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Color = WorldInfo.Color;
using Direciton = WorldInfo.Direction;

public class SceneStuff : MonoBehaviour
{
    public static List<int> levelsSeen = new List<int>();
    public static SceneStuff instance;
    public Texture2D[] cursors = new Texture2D[5];
    public GameObject indicator;
    public GameObject bugs;
    public GameObject TEST;
    public Vector3 startPos;
    public Vector3 startRot;
    public Direciton indicatorDirection;
    public bool laser;
    public bool bugsPlacable = true;
    public bool clickableIndicator = true;
    public bool canQuit = true;
    public float countdown;
    public int redNum = 1;
    public int blueNum = 1;
    public int yellowNum = 1;
    public int greenNum = 1;
    public static Color selectedColor = Color.Red;
    [HideInInspector]
    public int[] colorNums = new int[4];

    public AudioClip click;
    private AudioSource audioSrc;

    private void Awake()
    {
        colorNums[0] = redNum;
        colorNums[1] = blueNum;
        colorNums[2] = yellowNum;
        colorNums[3] = greenNum;
        instance = this;
    }

    public void Start()
    {
        if (!levelsSeen.Contains(SceneManager.GetActiveScene().buildIndex))
            levelsSeen.Add(SceneManager.GetActiveScene().buildIndex);

        if (PlayerPrefs.GetInt("MaxLevel") < SceneManager.GetActiveScene().buildIndex)
            PlayerPrefs.SetInt("MaxLevel", SceneManager.GetActiveScene().buildIndex);

        audioSrc = GetComponent<AudioSource>();
        audioSrc.loop = true;
        audioSrc.Play();

        Indicator indicate = Instantiate(indicator).GetComponent<Indicator>();
        indicate.direction = indicatorDirection;
        indicate.startPos = startPos;
        indicate.startRot = startRot;
        indicate.countdown = countdown;
        indicate.clickable = clickableIndicator;

        StartCoroutine(FirstCheck());
    }

    /// <summary>
    /// This is needed to make sure that the first BugCheck is performed after UIBug is fully initialized. Simply using Awake still ignores dependencies
    /// </summary>
    /// <returns></returns>
    IEnumerator FirstCheck()
    {
        yield return new WaitForEndOfFrame();
        BugChecker();
    }

    public void BugChecker()
    {
        if (colorNums[(int)selectedColor] < 1)
        {
            bool found = false;
            int i = 0;
            for (; i < colorNums.Length; i++)
            {
                if (colorNums[i] > 0)
                {
                    found = true;
                    break;
                }
            }
            if (found)
            {
                selectedColor = (Color)i;
                Cursor.SetCursor(cursors[i + 1], Vector2.zero, CursorMode.Auto);
                UIBug.instance.ColorStuff(true);
            }
            else
                Cursor.SetCursor(cursors[0], Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(cursors[(int)selectedColor + 1], Vector2.zero, CursorMode.Auto);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Red") && colorNums[0] > 0 && selectedColor != Color.Red)
        {
            Cursor.SetCursor(cursors[1], Vector2.zero, CursorMode.Auto);
            audioSrc.PlayOneShot(click);
            selectedColor = Color.Red;
            UIBug.instance.ColorStuff(true);
        }
        else if (Input.GetButtonDown("Blue") && colorNums[1] > 0 && selectedColor != Color.Blue)
        {
            Cursor.SetCursor(cursors[2], Vector2.zero, CursorMode.Auto);
            audioSrc.PlayOneShot(click);
            selectedColor = Color.Blue;
            UIBug.instance.ColorStuff(true);
        }
        else if (Input.GetButtonDown("Yellow") && colorNums[2] > 0 && selectedColor != Color.Yellow)
        {
            Cursor.SetCursor(cursors[3], Vector2.zero, CursorMode.Auto);
            audioSrc.PlayOneShot(click);
            selectedColor = Color.Yellow;
            UIBug.instance.ColorStuff(true);
        }
        else if (Input.GetButtonDown("Green") && colorNums[3] > 0 && selectedColor != Color.Green)
        {
            Cursor.SetCursor(cursors[4], Vector2.zero, CursorMode.Auto);
            audioSrc.PlayOneShot(click);
            selectedColor = Color.Green;
            UIBug.instance.ColorStuff(true);
        }
        else if (Input.GetButtonDown("Quit") && canQuit)
        {
            MainBody.instance.Clean();
            SceneManager.LoadScene("Title");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Color = WorldInfo.Color;

public class SceneStuff : MonoBehaviour
{
    public static List<int> levelsSeen = new List<int>();
    public static SceneStuff instance;
    public GameObject indicator;
    public GameObject bugs;
    public GameObject TEST;
    public Vector3 startPos;
    public Vector3 startRot;
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

        Indicator indicate = Instantiate(indicator).GetComponent<Indicator>();
        indicate.startPos = startPos;
        indicate.startRot = startRot;
        indicate.countdown = countdown;
        indicate.clickable = clickableIndicator;

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
                UIBug.instance.ColorStuff(true);
            }
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Red") && colorNums[0] > 0)
        {
            selectedColor = Color.Red;
            UIBug.instance.ColorStuff(true);
        }
        else if (Input.GetButtonDown("Blue") && colorNums[1] > 0)
        {
            selectedColor = Color.Blue;
            UIBug.instance.ColorStuff(true);
        }
        else if (Input.GetButtonDown("Yellow") && colorNums[2] > 0)
        {
            selectedColor = Color.Yellow;
            UIBug.instance.ColorStuff(true);
        }
        else if (Input.GetButtonDown("Green") && colorNums[3] > 0)
        {
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
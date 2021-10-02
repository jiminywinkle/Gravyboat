using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStuff : MonoBehaviour
{
    public static SceneStuff instance;
    public GameObject bugs;
    public GameObject TEST;
    public static int redNum = 1;
    public static int blueNum = 1;
    public static int yellowNum = 1;
    public static int greenNum = 1;
    public static Bug.bugColor selectedColor = Bug.bugColor.Red;
    public static int[] colorNums = { redNum, blueNum, yellowNum, greenNum };

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        BugChecker();
    }

    public void BugChecker()
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
            selectedColor = (Bug.bugColor)i;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Red") && colorNums[0] > 0)
        {
            selectedColor = Bug.bugColor.Red;
            print(selectedColor);
        }
        else if (Input.GetButtonDown("Blue") && colorNums[1] > 0)
        {
            selectedColor = Bug.bugColor.Blue;
            print(selectedColor);
        }
        else if (Input.GetButtonDown("Yellow") && colorNums[2] > 0)
        {
            selectedColor = Bug.bugColor.Yellow;
            print(selectedColor);
        }
        else if (Input.GetButtonDown("Green") && colorNums[3] > 0)
        {
            selectedColor = Bug.bugColor.Green;
            print(selectedColor);
        }
    }
}
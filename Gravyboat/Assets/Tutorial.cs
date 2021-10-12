using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public GameObject explanation;
    public GameObject arrow;
    private TextMeshPro text;
    private int progress = 0;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = "Welcome to the Tutorial \n Click to Continue";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (progress)
            {
                case 0:
                    Click1();
                    break;
                case 1:
                    Click2();
                    break;
                case 2:
                    Click3();
                    break;
                case 3:
                    Click4();
                    break;
                case 4:
                    Click5();
                    break;
                case 5:
                    Click6();
                    break;
                case 6:
                    Click7();
                    break;
                case 7:
                    Click8();
                    break;
                case 8:
                    Click9();
                    break;
                case 9:
                    Click10();
                    break;
            }
        }
    }

    private void Click1()
    {
        text.text = "This is Gravyboat \n They can fly (somewhat) and roll";
        progress++;
    }

    private void Click2()
    {
        text.text = "Press 'A' to extend your wings, 'S' to retract";
        progress++;
    }

    private void Click3()
    {
        text.text = "Gravyboat can fly in four directions \n They also have four wings";
        progress++;
    }

    private void Click4()
    {
        text.text = "These bugs can help you fly by changing the direction of Gravyboat \n Each wing has its own matching bug color";
        progress++;
    }

    private void Click5()
    {
        text.text = "A bug pointing towards a straight-facing wing will propel Gravyboat \n Ones on top and bottom instead will rotate accordingly";
        progress++;
    }

    private void Click6()
    {
        text.text = "Press 1-4 to select bug color \n Left click to place a bug \n Right click to remove";
        progress++;
    }

    private void Click7()
    {
        text.text = "The direction the bugs are pointing is dependant on where they were placed on the obstacle";
        progress++;
    }

    private void Click8()
    {
        text.text = "This indicator tells you where Gravyboat will enter the scene \n The game will start when either the counter ticks down or you click the indicator";
        progress++;
    }

    private void Click9()
    {
        text.text = "Reach the exit to win";
        progress++;
    }

    private void Click10()
    {
        MainBody.instance.Clean();
        SceneManager.LoadScene("Level1");
    }
}

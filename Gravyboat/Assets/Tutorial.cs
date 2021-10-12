using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public GameObject explanation;
    public GameObject arrow;
    public GameObject dummyboat;
    public GameObject rolledboat;
    public GameObject bugs;
    public GameObject indicator;
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
        dummyboat.transform.position = new Vector3(13.1f, 12.2f, 0);
        text.text = "This is Gravyboat \n They can fly (somewhat) and roll";
        progress++;
    }

    private void Click2()
    {
        dummyboat.transform.position = new Vector3(-150, 0, 0);
        rolledboat.transform.position = new Vector3(13.1f, 12.2f, 0);
        text.text = "Press 'A' to extend your wings, 'S' to retract";
        progress++;
    }

    private void Click3()
    {
        rolledboat.transform.position = new Vector3(-150, 0, 0);
        dummyboat.transform.position = new Vector3(13.1f, 12.2f, 0);
        text.text = "Gravyboat can fly in four directions \n They also have four wings";
        progress++;
    }

    private void Click4()
    {
        dummyboat.transform.position = new Vector3(-150, 0, 0);
        bugs.transform.position = new Vector3(13.1f, 0, 0);
        text.text = "These bugs can help you fly by changing the direction of Gravyboat \n Each wing has its own matching colored bug";
        progress++;
    }

    private void Click5()
    {
        bugs.transform.position = new Vector3(57.6f, -2.6f, 0);
        Light2D bugLight = bugs.GetComponent<Light2D>();
        bugLight.color = new Color(255, 255, 0);
        bugLight.intensity = .02f;
        dummyboat.transform.position = new Vector3(9, -2.6f, 0);
        arrow.transform.position = new Vector3(-41, -2.6f, 0);
        arrow.transform.eulerAngles = new Vector3(0, 0, -180);
        text.text = "A bug pointing the same way as a matching wing will propel Gravyboat \n If a wing is perpendicular, it will instead rotate Gravyboat";
        progress++;
    }

    private void Click6()
    {
        bugs.transform.position = new Vector3(-150, 0, 0);
        dummyboat.transform.position = new Vector3(-150, 0, 0);
        arrow.transform.position = new Vector3(-47.9f, -.6f, 0);
        arrow.transform.eulerAngles = new Vector3(0, 0, -142.707f);
        text.text = "Press 1-4 to select bug color \n Left click to place a bug / Right to remove \n The numbers indicate how many bugs you can use";
        progress++;
    }

    private void Click7()
    {
        arrow.transform.position = new Vector3(-150, 0, 0);
        explanation.transform.position = new Vector3(9.7f, 3.3f, 0);
        text.text = "The direction the bugs are pointing is dependant on where they were placed on the obstacle";
        progress++;
    }

    private void Click8()
    {
        explanation.transform.position = new Vector3(-150, 0, 0);
        indicator.transform.position = new Vector3(13.1f, 0, 0);
        text.text = "This indicator tells you where Gravyboat will enter the scene and their rotation \n The game will start when either the counter ticks down or you click the indicator";
        progress++;
    }

    private void Click9()
    {
        indicator.transform.position = new Vector3(-150, 0, 0);
        arrow.transform.position = new Vector3(63.5f, 7.2f, 0);
        arrow.transform.eulerAngles = new Vector3(0, 0, -59.6f);
        text.text = "Reach the exit to win";
        progress++;
    }

    private void Click10()
    {
        MainBody.instance.Clean();
        SceneManager.LoadScene("Level1");
    }
}

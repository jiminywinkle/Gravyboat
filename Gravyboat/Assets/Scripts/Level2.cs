using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Level2 : MonoBehaviour
{
    public TextMeshPro text1;
    public TextMeshPro text2;
    public TextMeshPro text3;
    public TextMeshPro text4;
    public BugPlacer obstacle;

    // Start is called before the first frame update
    void Start()
    {
        text2.enabled = false;
        text3.enabled = false;
        text4.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MainBody.instance.Clean();
            SceneManager.LoadScene("Level3");
        }

        if (text1)
        {
            foreach (bool position in obstacle.positions)
            {
                if (position)
                {
                    text2.enabled = true;
                    Destroy(text1);
                }
            }
        } 
        else if (text2)
        {
            bool allHere = true;
            foreach (bool position in obstacle.positions)
            {
                if (!position)
                    allHere = false;
            }
            if (allHere)
            {
                text3.enabled = true;
                text4.enabled = true;
                Destroy(text2);
            }
        }
    }
}

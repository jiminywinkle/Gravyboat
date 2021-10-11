using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoundingBox : MonoBehaviour
{
    // Up Down Left Right
    public float[] bounds = new float[4];
    private bool[] boundsEnabled = new bool[4];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            if (bounds[i] == 0)
                boundsEnabled[i] = false;
            else
                boundsEnabled[i] = true;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (boundsEnabled[i])
            {
                switch (i)
                {
                    case 0:
                        if (MainBody.instance.gameObject.transform.position.y > bounds[i])
                        {
                            MainBody.instance.Clean();
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        }
                        break;
                    case 1:
                        if (MainBody.instance.gameObject.transform.position.y < bounds[i])
                        {
                            MainBody.instance.Clean();
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        }
                        break;
                    case 2:
                        if (MainBody.instance.gameObject.transform.position.x < bounds[i])
                        {
                            MainBody.instance.Clean();
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        }
                        break;
                    case 3:
                        if (MainBody.instance.gameObject.transform.position.x > bounds[i])
                        {
                            MainBody.instance.Clean();
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        }
                        break;
                }
            }
        }
    }
}

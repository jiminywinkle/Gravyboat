using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void Reload()
    {
        MainBody.instance.Clean();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

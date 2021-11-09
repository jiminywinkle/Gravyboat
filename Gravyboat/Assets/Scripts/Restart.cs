using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private AudioSource audioSrc;

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }
    public void Reload()
    {
        StartCoroutine(Initiate());
    }

    IEnumerator Initiate()
    {
        audioSrc.Play();
        yield return new WaitForSeconds(.35f);
        MainBody.instance.Clean();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

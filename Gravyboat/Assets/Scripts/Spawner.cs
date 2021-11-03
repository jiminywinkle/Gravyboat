using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnable;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            GameObject[] spawned = GameObject.FindGameObjectsWithTag("Spawned");
            if (spawned.Length < 3)
            {
                GameObject spawning = Instantiate(spawnable, transform.position, Quaternion.identity);
                spawning.tag = "Spawned";
                yield return new WaitForSeconds(3);
            }
            else if (spawned.Length > 0)
            {
                for (int i = spawned.Length - 1; i >= 0; i--)
                {
                    if (spawned[i].transform.position.y < -70)
                        Destroy(spawned[i]);
                }
            }
            yield return null;
        }
    }
}

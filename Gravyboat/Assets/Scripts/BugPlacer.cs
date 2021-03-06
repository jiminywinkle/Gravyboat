using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class BugPlacer : MonoBehaviour
{
    // Up, Down, Left, Right
    public bool[] positions = { false, false, false, false };
    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        // Just so that you don't have to change the mask or light sprite in the inspecter
        SpriteMask mask = GetComponent<SpriteMask>();
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        mask.sprite = renderer.sprite;

        // Add the color cycling effect so that you don't have to :D
        gameObject.AddComponent<ColorMat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (SceneStuff.instance.colorNums[(int)SceneStuff.selectedColor] > 0 && SceneStuff.instance.bugsPlacable)
        {
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            // THIS IS VERY IMPORTANT. It allows the mouse to always hit the bug's collider primarily instead of the obstacle
            mousePos.z = -.1f;
            GameObject bugs = Instantiate(SceneStuff.instance.bugs, mousePos, Quaternion.identity);
            Bug bugScript = bugs.GetComponent<Bug>();
            bugScript.location = gameObject;
            SceneStuff.instance.colorNums[(int)SceneStuff.selectedColor]--;
            UIBug.instance.ColorStuff(false, -1, SceneStuff.selectedColor);
            CircleCollider2D collider = bugs.GetComponent<CircleCollider2D>();
            ContactFilter2D filter = new ContactFilter2D().NoFilter();
            List<Collider2D> results = new List<Collider2D>();
            collider.OverlapCollider(filter, results);
            foreach (Collider2D collision in results)
            {
                if (collision.tag == "Bug")
                {
                    Destroy(bugs.gameObject);
                    SceneStuff.instance.colorNums[(int)SceneStuff.selectedColor]++;
                    UIBug.instance.ColorStuff(false, 1, SceneStuff.selectedColor);
                    break;
                }
            }
        }
    }
}

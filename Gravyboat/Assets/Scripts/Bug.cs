using JetBrains.Annotations;
using System.Numerics;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Direction = WorldInfo.Direction;

public class Bug : MonoBehaviour
{
    public GameObject location;
    public Direction direction;
    public bool active = true;
    private WorldInfo.Color color;
    private Light2D light;
    private Vector3 raycastDir;
    private float deactivateCount = 1f;

    private float midPoint;
    private float topSection;
    private float bottomSection;

    [SerializeField] private LayerMask mask;

    private GameObject TEST;

    private void Start()
    {
        color = SceneStuff.selectedColor;
        light = GetComponentInChildren<Light2D>();

        switch (color)
        {
            case WorldInfo.Color.Red:
                light.color = new Color(255, 0, 0);
                break;
            case WorldInfo.Color.Blue:
                light.color = new Color(0, 0, 255);
                break;
            case WorldInfo.Color.Yellow:
                light.color = new Color(255, 255, 0);
                break;
            case WorldInfo.Color.Green:
                light.color = new Color(0, 255, 0);
                break;
        }

        SceneStuff.instance.BugChecker();

        SpriteRenderer locationSprite = location.GetComponent<SpriteRenderer>();
        midPoint = location.transform.position.x;
        topSection = location.transform.position.y + (locationSprite.bounds.extents.y / 2);
        bottomSection = location.transform.position.y - (locationSprite.bounds.extents.y / 2);

        //print(locationSprite.bounds.extents.y);
        //print(-locationSprite.bounds.extents.y);

        if (transform.position.y > topSection)
        {
            direction = Direction.Up;
            raycastDir = transform.up;
            TEST = Instantiate(SceneStuff.instance.TEST, new Vector3(transform.position.x, transform.position.y + 5, 0), Quaternion.Euler(0, 0, 90));
        }
        else if (transform.position.y < bottomSection)
        {
            direction = Direction.Down;
            raycastDir = -transform.up;
            TEST = Instantiate(SceneStuff.instance.TEST, new Vector3(transform.position.x, transform.position.y - 5, 0), Quaternion.Euler(0, 0, -90));
        }
        else if (transform.position.x > midPoint)
        {
            direction = Direction.Right;
            raycastDir = transform.right;
            TEST = Instantiate(SceneStuff.instance.TEST, new Vector3(transform.position.x + 5, transform.position.y, 0), Quaternion.Euler(0, 0, 0));
        }
        else
        {
            direction = Direction.Left;
            raycastDir = -transform.right;
            TEST = Instantiate(SceneStuff.instance.TEST, new Vector3(transform.position.x - 5, transform.position.y, 0), Quaternion.Euler(0, 0, 180));
        }

        //print("Top: " + location.transform.position.x.ToString() + " " + topSection.ToString() + " " + 0);
        //print("Bottom: " + location.transform.position.x.ToString() + " " + bottomSection.ToString() + " " + 0);
        //print("Mid: " + midPoint.ToString() + " " + location.transform.position.y.ToString() + " " + 0);

        //Instantiate(SceneStuff.instance.TEST, new Vector3(location.transform.position.x, topSection, 0), Quaternion.identity);
        //Instantiate(SceneStuff.instance.TEST, new Vector3(location.transform.position.x, bottomSection, 0), Quaternion.identity);
        //Instantiate(SceneStuff.instance.TEST, new Vector3(midPoint, location.transform.position.y, 0), Quaternion.identity);

        if (location.GetComponent<BugPlacer>().positions[(int)direction])
        {
            Destroy(gameObject);
            Destroy(TEST);
            SceneStuff.colorNums[(int)color]++;
        }
        location.GetComponent<BugPlacer>().positions[(int)direction] = true;
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360f));
    }

    // Update is called once per frame
    void Update()
    {
        if (active && MainBody.flying)
        {
            //RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, 2f, raycastDir, Mathf.Infinity, mask);
            Debug.DrawRay(transform.position, raycastDir * 10);
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, raycastDir, Mathf.Infinity, mask);
            for (int j = 0; j < hit.Length; j++)
                print(hit[j].collider.name);
            for (int i = 1; i < hit.Length; i++)
            {
                if (hit[i].collider != null)
                {
                    if (hit[i].collider.tag == "Obstacle")
                        return;
                    else if (hit[i].collider.tag == "Wing" || hit[i].collider.tag == "Body")
                    {
                        foreach (Wing wing in MainBody.wings)
                        {
                            if (wing.color == color)
                            {
                                // Remember: Up, Down, Left, Right
                                switch (direction)
                                {
                                    case (Direction)0:
                                        switch (wing.direction)
                                        {
                                            // Up and Up pushes body upward
                                            case (Direction)0:
                                                MainBody.direction = Direction.Up;
                                                break;
                                            // Up and Left rotates body right
                                            case (Direction)2:
                                                MainBody.instance.Rotate(true);
                                                break;
                                            // Up and Right rotates body left
                                            case (Direction)3:
                                                MainBody.instance.Rotate(false);
                                                break;
                                        }
                                        break;
                                    case (Direction)1:
                                        switch (wing.direction)
                                        {
                                            // Down and Down pushes body downward
                                            case (Direction)1:
                                                MainBody.direction = Direction.Down;
                                                break;
                                            // Down and Left rotates body left
                                            case (Direction)2:
                                                MainBody.instance.Rotate(false);
                                                break;
                                            // Down and Right rotates body right
                                            case (Direction)3:
                                                MainBody.instance.Rotate(true);
                                                break;
                                        }
                                        break;
                                    case (Direction)2:
                                        switch (wing.direction)
                                        {
                                            // Left and Up rotates body left
                                            case (Direction)0:
                                                MainBody.instance.Rotate(false);
                                                break;
                                            // Left and Down rotates body right
                                            case (Direction)1:
                                                MainBody.instance.Rotate(true);
                                                break;
                                            // Left and Left pushes body left
                                            case (Direction)2:
                                                MainBody.direction = Direction.Left;
                                                break;
                                        }
                                        break;
                                    case (Direction)3:
                                        switch (wing.direction)
                                        {
                                            // Right and Up rotates body right
                                            case (Direction)0:
                                                MainBody.instance.Rotate(true);
                                                break;
                                            // Right and Down rotates body left
                                            case (Direction)1:
                                                MainBody.instance.Rotate(false);
                                                break;
                                            // Right and Right pushes body right
                                            case (Direction)3:
                                                MainBody.direction = Direction.Right;
                                                break;
                                        }
                                        break;
                                }
                                StartCoroutine(Deactivate());
                                break;
                            }
                        }
                        return;
                    }
                }
            }
        }
    }

    
    IEnumerator Deactivate()
    {
        print("DEACTIVATING");
        active = false;
        while (deactivateCount > 0)
        {
            deactivateCount -= (1 * Time.deltaTime) / 2;
            light.intensity = deactivateCount;
            yield return null;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(1))
        {
            Destroy(this.gameObject);
            Destroy(TEST);
            location.GetComponent<BugPlacer>().positions[(int)direction] = false;
            SceneStuff.colorNums[(int)color]++;
        }
    }
}

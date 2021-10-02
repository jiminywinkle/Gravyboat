using JetBrains.Annotations;
using System.Numerics;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Bug : MonoBehaviour
{
    public enum bugColor { Red, Blue, Yellow, Green}
    public enum bugDirection { Up, Down, Left, Right}
    public GameObject location;
    public bugDirection direction;
    public bool active = true;
    private bugColor type;
    private Light2D light;
    private Vector3 raycastDir;
    private float deactivateCount = 1f;

    private float midPoint;
    private float topSection;
    private float bottomSection;

    private GameObject TEST;

    private void Start()
    {
        type = SceneStuff.selectedColor;
        light = GetComponentInChildren<Light2D>();

        switch (type)
        {
            case bugColor.Red:
                light.color = new Color(255, 0, 0);
                break;
            case bugColor.Blue:
                light.color = new Color(0, 0, 255);
                break;
            case bugColor.Yellow:
                light.color = new Color(255, 255, 0);
                break;
            case bugColor.Green:
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
            direction = bugDirection.Up;
            raycastDir = transform.up;
            TEST = Instantiate(SceneStuff.instance.TEST, new Vector3(transform.position.x, transform.position.y + 5, 0), Quaternion.Euler(0, 0, 90));
        }
        else if (transform.position.y < bottomSection)
        {
            direction = bugDirection.Down;
            raycastDir = -transform.up;
            TEST = Instantiate(SceneStuff.instance.TEST, new Vector3(transform.position.x, transform.position.y - 5, 0), Quaternion.Euler(0, 0, -90));
        }
        else if (transform.position.x > midPoint)
        {
            direction = bugDirection.Right;
            raycastDir = transform.right;
            TEST = Instantiate(SceneStuff.instance.TEST, new Vector3(transform.position.x + 5, transform.position.y, 0), Quaternion.Euler(0, 0, 0));
        }
        else
        {
            direction = bugDirection.Left;
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
            SceneStuff.colorNums[(int)type]++;
        }
        location.GetComponent<BugPlacer>().positions[(int)direction] = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDir);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Wing")
                {

                }
            }
        }
    }

    
    IEnumerator Deactivate()
    {
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
            SceneStuff.colorNums[(int)type]++;
        }
    }
}

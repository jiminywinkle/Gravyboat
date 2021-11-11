using JetBrains.Annotations;
using System.Numerics;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Direction = WorldInfo.Direction;
using Vector2 = UnityEngine.Vector2;

public class Bug : MonoBehaviour
{
    public GameObject location;
    public Direction direction;
    public bool active = true;
    private WorldInfo.Color color;
    private Light2D light;
    private SpriteRenderer sprite;
    private Collider2D collider;
    private Vector3 raycastDir;
    private float deactivateCount = 1f;

    private float midPoint;
    private float topSection;
    private float bottomSection;

    public AudioClip place;
    public AudioClip remove;
    public AudioClip effect;
    private AudioSource audioSrc;

    [SerializeField] private LayerMask mask;

    private GameObject TEST;

    private void Start()
    {
        color = SceneStuff.selectedColor;
        collider = GetComponent<Collider2D>();
        light = GetComponentInChildren<Light2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        audioSrc = GetComponent<AudioSource>();

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
            TEST = Instantiate(SceneStuff.instance.TEST, new Vector3(transform.position.x, transform.position.y + 10, 0), Quaternion.Euler(0, 0, 90));
        }
        else if (transform.position.y < bottomSection)
        {
            direction = Direction.Down;
            raycastDir = -transform.up;
            TEST = Instantiate(SceneStuff.instance.TEST, new Vector3(transform.position.x, transform.position.y - 10, 0), Quaternion.Euler(0, 0, -90));
        }
        else if (transform.position.x > midPoint)
        {
            direction = Direction.Right;
            raycastDir = transform.right;
            TEST = Instantiate(SceneStuff.instance.TEST, new Vector3(transform.position.x + 10, transform.position.y, 0), Quaternion.Euler(0, 0, 0));
        }
        else
        {
            direction = Direction.Left;
            raycastDir = -transform.right;
            TEST = Instantiate(SceneStuff.instance.TEST, new Vector3(transform.position.x - 10, transform.position.y, 0), Quaternion.Euler(0, 0, 180));
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
            SceneStuff.instance.colorNums[(int)color]++;
            UIBug.instance.ColorStuff(false, 1, color);
        }

        SceneStuff.instance.BugChecker();

        audioSrc.PlayOneShot(place);

        location.GetComponent<BugPlacer>().positions[(int)direction] = true;
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360f));
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            // Currently, the bug will not function while Gravyboat is rotating because rotating while already rotating is bugged and I don't feel like fixing it
            if (MainBody.flying && MainBody.instance.controllable && !MainBody.instance.rotating)
            {
                //RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, 2f, raycastDir, Mathf.Infinity, mask);
                Debug.DrawRay(transform.position, raycastDir * 10);
                RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, raycastDir, 80, mask);
                //for (int j = 0; j < hit.Length; j++)
                //    print(hit[j].collider.name);
                for (int i = 1; i < hit.Length; i++)
                {
                    if (hit[i].collider != null)
                    {
                        if (hit[i].collider.tag == "Obstacle")
                            return;
                        else if ((hit[i].collider.tag == "Wing" && Vector2.Distance(hit[i].point, MainBody.instance.gameObject.transform.position) < 7.5f) || hit[i].collider.tag == "Body")
                        {
                            foreach (Wing wing in MainBody.wings)
                            {
                                if (wing.color == color)
                                {
                                    bool used = false;

                                    // Remember: Up, Down, Left, Right
                                    switch (direction)
                                    {
                                        case (Direction)0:
                                            switch (wing.direction)
                                            {
                                                // Up and Up pushes body upward
                                                case (Direction)0:
                                                    MainBody.direction = Direction.Up;
                                                    used = true;
                                                    break;
                                                // Up and Left rotates body right
                                                case (Direction)2:
                                                    MainBody.instance.Rotate(true);
                                                    used = true;
                                                    break;
                                                // Up and Right rotates body left
                                                case (Direction)3:
                                                    MainBody.instance.Rotate(false);
                                                    used = true;
                                                    break;
                                            }
                                            break;
                                        case (Direction)1:
                                            switch (wing.direction)
                                            {
                                                // Down and Down pushes body downward
                                                case (Direction)1:
                                                    MainBody.direction = Direction.Down;
                                                    used = true;
                                                    break;
                                                // Down and Left rotates body left
                                                case (Direction)2:
                                                    MainBody.instance.Rotate(false);
                                                    used = true;
                                                    break;
                                                // Down and Right rotates body right
                                                case (Direction)3:
                                                    MainBody.instance.Rotate(true);
                                                    used = true;
                                                    break;
                                            }
                                            break;
                                        case (Direction)2:
                                            switch (wing.direction)
                                            {
                                                // Left and Up rotates body left
                                                case (Direction)0:
                                                    MainBody.instance.Rotate(false);
                                                    used = true;
                                                    break;
                                                // Left and Down rotates body right
                                                case (Direction)1:
                                                    MainBody.instance.Rotate(true);
                                                    used = true;
                                                    break;
                                                // Left and Left pushes body left
                                                case (Direction)2:
                                                    MainBody.direction = Direction.Left;
                                                    used = true;
                                                    break;
                                            }
                                            break;
                                        case (Direction)3:
                                            switch (wing.direction)
                                            {
                                                // Right and Up rotates body right
                                                case (Direction)0:
                                                    MainBody.instance.Rotate(true);
                                                    used = true;
                                                    break;
                                                // Right and Down rotates body left
                                                case (Direction)1:
                                                    MainBody.instance.Rotate(false);
                                                    used = true;
                                                    break;
                                                // Right and Right pushes body right
                                                case (Direction)3:
                                                    MainBody.direction = Direction.Right;
                                                    used = true;
                                                    break;
                                            }
                                            break;
                                    }
                                    if (used)
                                    {
                                        StartCoroutine(Deactivate());
                                        wing.StartCoroutine(wing.Alight());
                                        audioSrc.PlayOneShot(effect);
                                    }
                                    break;
                                }
                            }
                            return;
                        }
                    }
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
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, deactivateCount);
            light.intensity = deactivateCount;
            yield return null;
        }
        location.GetComponent<BugPlacer>().positions[(int)direction] = false;
        Destroy(TEST);
        Destroy(this.gameObject);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(1) && active)
        {
            collider.enabled = false;
            audioSrc.PlayOneShot(remove);
            StartCoroutine(Remove());
            Destroy(TEST);
            location.GetComponent<BugPlacer>().positions[(int)direction] = false;
            SceneStuff.instance.colorNums[(int)color]++;
            UIBug.instance.ColorStuff(false, 1, color);
        }
    }

    IEnumerator Remove()
    {
        active = false;
        float alpha = 1;
        float intensity = light.intensity;
        while (alpha > 0)
        {
            alpha -= 1 * Time.deltaTime;
            light.intensity = alpha * intensity;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
            yield return null;
        }
        Destroy(gameObject);
    }

    public void ForceColor(WorldInfo.Color chosen)
    {
        color = chosen;
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
    }
}

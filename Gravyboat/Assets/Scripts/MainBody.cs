using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Light2D = UnityEngine.Experimental.Rendering.Universal.Light2D;
using Direction = WorldInfo.Direction;

public class MainBody : MonoBehaviour
{
    [HideInInspector]
    public static Direction direction;
    public Direction flyCommand = Direction.Right;
    public bool controllable = false;
    public static bool dead = false;
    public static bool flying = true;
    public static MainBody instance;
    public static List<Wing> wings = new List<Wing>();
    public static List<Animator> animators = new List<Animator>();
    //public static List<FixedJoint2D> joints = new List<FixedJoint2D>();
    public static List<PolygonCollider2D> colliders = new List<PolygonCollider2D>();
    public static CircleCollider2D collider;
    public GameObject body;
    public GameObject eyeWhite;
    public GameObject pupil;
    public GameObject laser;
    public Rigidbody2D rigid;
    private Light2D eyeLight;
    public float stunTimer = 1;
    public float moveSpeed = 10;
    public float gravityScale;
    private float laserTimer = 0;
    private float rotateTimer = 0;
    [HideInInspector]
    public bool rotating = false;

    public AudioClip extend;
    public AudioClip retract;
    public AudioClip shoot;
    private AudioSource audioSrc;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] tempWings = GameObject.FindGameObjectsWithTag("Wing");
        foreach (GameObject wing in tempWings)
        {
            wings.Add(wing.GetComponent<Wing>());
        }
        for (int i = 0; i < wings.Count; i++)
        {
            animators.Add(wings[i].GetComponentInChildren<Animator>());
            //joints.Add(wings[i].GetComponent<FixedJoint2D>());
            colliders.Add(wings[i].GetComponent<PolygonCollider2D>());
        }
        audioSrc = GetComponent<AudioSource>();
        collider = GetComponent<CircleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        gravityScale = rigid.gravityScale;
        eyeLight = pupil.GetComponent<Light2D>();
 
        if(flying)
        {
            rigid.angularVelocity = 0;
            //rigid.freezeRotation = true;
            rigid.gravityScale = 0;
        }
        direction = flyCommand;
    }

    public void Extend()
    {
        audioSrc.PlayOneShot(extend);
        for (int i = 0; i < wings.Count; i++)
        {
            animators[i].SetTrigger("Extending");
            colliders[i].enabled = true;
            wings[i].gameObject.GetComponentInChildren<Light2D>().enabled = true;
        }
        flying = true;
        rigid.angularVelocity = 0;
        rigid.freezeRotation = true;
        rigid.gravityScale = 0;
        WingCheck();
        direction = DirectionCheck();
        transform.eulerAngles = new Vector3(0, 0, SnapRotation(transform.eulerAngles.z));
        collider.radius = 3.36f;
    }

    public void Retract()
    {
        audioSrc.PlayOneShot(retract);
        for (int i = 0; i < wings.Count; i++)
        {
            animators[i].SetTrigger("Retracting");
            colliders[i].enabled = false;
            wings[i].gameObject.GetComponentInChildren<Light2D>().enabled = false;
        }
        flying = false;
        rigid.freezeRotation = false;
        rigid.gravityScale = gravityScale;
        if (wings.Count > 0)
            collider.radius = 5.5f;
    }

    void FixedUpdate()
    {
        if(controllable)
        {
            if (!dead)
            {
                if (!flying)
                {
                    if (stunTimer >= 1f)
                    {
                        if (Input.GetButton("RollLeft"))
                        {
                            rigid.AddTorque(100f);
                        }
                        else if (Input.GetButton("RollRight"))
                        {
                            rigid.AddTorque(-100f);
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (controllable)
        {
            if (!dead)
            {
                if (laserTimer > 0)
                {
                    eyeLight.intensity -= 5 * Time.deltaTime;
                    laserTimer -= 1 * Time.deltaTime;
                }

                if (stunTimer < 1f)
                    stunTimer += 1 * Time.deltaTime;
                else
                {
                    // Grabs an arbitrary animator from the wings to check the animation state
                    if (Input.GetButton("Extend") && animators[0].GetCurrentAnimatorStateInfo(0).IsName("Retracted"))
                    {
                        Extend();

                    }
                    else if (Input.GetButton("Retract") && animators[0].GetCurrentAnimatorStateInfo(0).IsName("Extended"))
                    {
                        Retract();
                    }

                    if (SceneStuff.instance.laser)
                    {
                        if (Input.GetButtonDown("Laser") && laserTimer <= 0)
                        {
                            audioSrc.PlayOneShot(shoot);
                            eyeLight.intensity = 5;
                            laserTimer = 1;
                            GameObject shot = Instantiate(laser, pupil.transform.position, Quaternion.identity);
                        }
                    }
                }

                if (flying)
                {
                    //if (Input.GetKeyDown(KeyCode.M))
                    //    Rotate(true);
                    //else if (Input.GetKeyDown(KeyCode.N))
                    //    Rotate(false);

                    switch (direction)
                    {
                        case (Direction)0:
                            rigid.velocity = new Vector2(0, moveSpeed);
                            break;
                        case (Direction)1:
                            rigid.velocity = new Vector2(0, -moveSpeed);
                            break;
                        case (Direction)2:
                            rigid.velocity = new Vector2(-moveSpeed, 0);
                            break;
                        case (Direction)3:
                            rigid.velocity = new Vector2(moveSpeed, 0);
                            break;
                    }
                }
            }
            else if (animators[0].GetCurrentAnimatorStateInfo(0).IsName("Extended"))
            {
                Retract();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (flying)
        {
            StartCoroutine(Die());
            /* Can't just use the Break() function because that would alter the list as it's iterating. The player is already dead so removing the list elements doesn't matter anyway
            foreach (Wing wing in wings)
            {
                wing.animator.Play("Extended");
                wing.broken = true;
                wing.transform.parent = null;
                wing.animator.enabled = false;
            }
            */
        }
    }

    public IEnumerator Die()
    {
        dead = true;
        rigid.freezeRotation = false;
        rigid.gravityScale = 5;
        while (eyeWhite.transform.localScale.y > 0)
        {
            eyeWhite.transform.localScale = new Vector3(eyeWhite.transform.localScale.x, eyeWhite.transform.localScale.y - 1*Time.deltaTime, eyeWhite.transform.localScale.z);
            yield return null;
        }
    }

    /// <summary>
    /// Rounds the given float to the closest interval of 90
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns></returns>
    float SnapRotation(float rotation)
    {
        return Mathf.Round(rotation / 90) * 90;
    }

    /// <summary>
    /// Returns the direction the rigidbody is currently moving based in the four cardinal directions
    /// </summary>
    /// <returns></returns>
    Direction DirectionCheck()
    {
        Direction result;
        if (Mathf.Abs(rigid.velocity.x) < 10)
        {
            if (rigid.velocity.y > 0)
                result = Direction.Up;
            else
                result = Direction.Down;
        }
        else
        {
            if (rigid.velocity.x > 0)
                result = Direction.Right;
            else
                result = Direction.Left;
        }
        return result;
    }

    private void WingCheck()
    {
        foreach (Wing wing in wings)
        {
            Transform wingSprite = wing.gameObject.transform.GetChild(0);
            float snapWing = SnapRotation(wingSprite.eulerAngles.z);
            if (transform.GetChild(0).eulerAngles.z > SnapRotation(transform.GetChild(0).eulerAngles.z))
                snapWing -= 90;
            else
                snapWing += 90;
            switch (snapWing)
            {
                case (0):
                case (360):
                    wing.direction = Direction.Up;
                    break;
                case (90):
                case (-270):
                case (450):
                    wing.direction = Direction.Left;
                    break;
                case (180):
                case (-180):
                    wing.direction = Direction.Down;
                    break;
                case (-90):
                case (270):
                case (-450):
                    wing.direction = Direction.Right;
                    break;
            }
            //print(wing.color.ToString() + " " + snapWing.ToString() + " " + wing.direction);
        }
    }

    public void Rotate(bool right)
    {
        if (!rotating)
        {
            rigid.freezeRotation = false;
            // Quickly finish the last rotation before the new one begins
            if (rotateTimer < 1)
            {
                //StopAllCoroutines();
                //transform.eulerAngles = new Vector3(0, 0, SnapRotation(transform.eulerAngles.z));
            }
            rotateTimer = 0;
            if (right)
            {
                foreach (Wing wing in wings)
                {
                    if (wing.direction == Direction.Down)
                        wing.direction = Direction.Left;
                    else if (wing.direction == Direction.Left)
                        wing.direction = Direction.Up;
                    else if (wing.direction == Direction.Right)
                        wing.direction = Direction.Down;
                    else
                        wing.direction = Direction.Right;
                }
            }
            else
            {
                foreach (Wing wing in wings)
                {
                    if (wing.direction == Direction.Down)
                        wing.direction = Direction.Right;
                    else if (wing.direction == Direction.Left)
                        wing.direction = Direction.Down;
                    else if (wing.direction == Direction.Right)
                        wing.direction = Direction.Up;
                    else
                        wing.direction = Direction.Left;
                }
            }
            StartCoroutine(Rotating(right));
        }
    }

    IEnumerator Rotating(bool right)
    {
        rotating = true;
        Quaternion destination = transform.rotation;
        if (right)
            destination.eulerAngles = new Vector3(0, 0, destination.eulerAngles.z - 90);
        else
            destination.eulerAngles = new Vector3(0, 0, destination.eulerAngles.z + 90);
        while (rotateTimer < 1)
        {
            rotateTimer += 2 * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, destination, rotateTimer);
            yield return null;
        }
        rigid.freezeRotation = true;
        rotating = false;
    }

    /// <summary>
    /// Clear all of the static info on a scene reload. Honestly not sure why I need to do this, but it prevents errors
    /// </summary>
    public void Clean()
    {
        controllable = false;
        dead = false;
        flying = true;
        wings.Clear();
        animators.Clear();
        colliders.Clear();
    }
}

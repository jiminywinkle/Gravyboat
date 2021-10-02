using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Light2D = UnityEngine.Experimental.Rendering.Universal.Light2D;

public class MainBody : MonoBehaviour
{
    public static bool flying = false;
    public static MainBody instance;
    public static List<Wing> wings = new List<Wing>();
    public static List<Animator> animators = new List<Animator>();
    public static List<FixedJoint2D> joints = new List<FixedJoint2D>();
    public static List<EdgeCollider2D> colliders = new List<EdgeCollider2D>();
    public static List<float> breakForces = new List<float>();
    public GameObject eyeWhite;
    public GameObject pupil;
    public GameObject laser;
    private Light2D eyeLight;
    private CircleCollider2D collider;
    private Rigidbody2D rigid;
    private float laserTimer = 0;


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
            joints.Add(wings[i].GetComponent<FixedJoint2D>());
            colliders.Add(wings[i].GetComponent<EdgeCollider2D>());
            breakForces.Add(joints[i].breakForce);
        }
        collider = GetComponent<CircleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        eyeLight = pupil.GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (laserTimer > 0)
        {
            eyeLight.intensity -= 5 * Time.deltaTime;
            laserTimer -= 1 * Time.deltaTime;
        }

        // Grabs an arbitrary animator from the wings to check the animation state
        if (Input.GetButton("Extend") && animators[0].GetCurrentAnimatorStateInfo(0).IsName("Retracted"))
        {
            for (int i = 0; i < wings.Count; i++)
            {
                animators[i].SetTrigger("Extending");
                joints[i].breakForce = breakForces[i];
                colliders[i].enabled = true;
            }
            rigid.angularVelocity = 0;
            rigid.freezeRotation = true;
            transform.eulerAngles = new Vector3(0, 0, SnapRotation(transform.eulerAngles.z));
            collider.radius = 3.36f;

        }
        else if (Input.GetButton("Retract") && animators[0].GetCurrentAnimatorStateInfo(0).IsName("Extended"))
        {
            for(int i = 0; i < wings.Count; i++)
            {
                animators[i].SetTrigger("Retracting");
                joints[i].breakForce = Mathf.Infinity;
                colliders[i].enabled = false;
            }
            rigid.freezeRotation = false;
            collider.radius = 5.5f;
        }

        if (WorldInfo.laserUnlocked)
        {
            if (Input.GetButtonDown("Laser") && laserTimer <= 0)
            {
                eyeLight.intensity = 5;
                laserTimer = 1;
                GameObject shot = Instantiate(laser, pupil.transform.position, Quaternion.identity);
            }
        }
    }

    float SnapRotation(float rotation)
    {
        return Mathf.Round(rotation / 90) * 90;
    }
}

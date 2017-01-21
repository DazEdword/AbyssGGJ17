using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotor : MonoBehaviour {

    [HideInInspector]
    public Rigidbody Rigidbody;
    [HideInInspector]
    public Collider Collider;
    [HideInInspector]
    public Creature Creature;

    Vector3 targetPosition = Vector3.zero;
    public Vector3 TargetPosition
    {
        get
        {
            return targetPosition;
        }
        set
        {
            targetPosition = value;
        }
    }

    [HideInInspector]
    public float MinimumDistanceToTargetPosition = 1f;
    public float JetSpeed = 20f;
    public float jetFreq = 1;

    public Vector3 jetDirection;

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
        Creature = GetComponent<Creature>();
        TargetPosition = transform.position;
    }

    void Start()
    {
        //AutoGround();
        //TargetPosition = transform.position;
        //Stop();

        //MinimumDistanceToTargetPosition = Collider.radius * +0.5f;
        InvokeRepeating("Jet", 0, jetFreq);

    }

    // Update is called once per frame
    void Update () {
    }

    public void Stop()
    {
        Rigidbody.velocity = Vector3.zero;
        TargetPosition = transform.position;
    }


    public void Jet()
    {
        float xDirectionSign = Mathf.Sign(Random.Range(-1f, 1f));

        jetDirection.x *= xDirectionSign;

        //Rigidbody.velocity = jetDirection.normalized * JetSpeed;
        Rigidbody.AddForce(jetDirection.normalized * JetSpeed);

        //Debug.Log(Rigidbody.velocity.magnitude);
        //while(Rigidbody.velocity.magnitude)
    }


    /*
    public void MoveTowardsPoint(Vector3 target)
    {
        Vector3 targetDir = new Vector3(target.x, transform.position.y, 0);
        targetDir -= transform.position;

        Rigidbody.velocity = targetDir.normalized * zSpeed;
    } */
}

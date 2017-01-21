using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotor : MonoBehaviour {

    public float jetTimer;
    public bool jet;


    //private
    private Vector3 jetDirection;
    private float jetSpeed;
    public int pushPtr = 0;
    [HideInInspector]
    public Rigidbody Rigidbody;
    [HideInInspector]
    public Collider Collider;
    [HideInInspector]
    public Creature Creature;

    public enum MovementTypes { JET = 0, SWIM = 1 };

    //Vector3 targetPosition = Vector3.zero;
    //public Vector3 TargetPosition
    //{
    //    get
    //    {
    //        return targetPosition;
    //    }
    //    set
    //    {
    //        targetPosition = value;
    //    }
    //}

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        jet = false;  
    }

    // Update is called once per frame
    void Update () {
        if (jetTimer >= 0)
        {
            jetTimer -= Time.deltaTime;
        }
        if ((jet) && (jetTimer <= 0))
        {
            thrust();
        }

    }

    public void Stop()
    {
        Rigidbody.velocity = Vector3.zero;
    }

    
    public void Jet(Vector3 jetDirection, float jetSpeed)
    {
        jet = true;
        pushPtr = 0;
        float xDirectionSign = Mathf.Sign(Random.Range(-1f, 1f));
        this.jetDirection = jetDirection;
        this.jetDirection.x = jetDirection.x*xDirectionSign;
        this.jetSpeed = jetSpeed;
    }
    private void thrust()
    {  
        if (pushPtr < 5)
        {
            if (jetTimer <= 0)
            {

                Rigidbody.AddForce(jetDirection.normalized * jetSpeed);

                pushPtr++;
                jetTimer = 0.01f;
            }


        }
        else if ((pushPtr >= 5) && (pushPtr < 10))
        {
                Rigidbody.AddForce(jetDirection.normalized * jetSpeed);

                pushPtr++;
                jetTimer = 0.1f;
        }
        else
        {
            jet = false;
        }
    }

    public void Swim(Vector3 swimDirection, float swimSpeed) {
        Rigidbody.velocity = swimDirection * swimSpeed;
    }
}

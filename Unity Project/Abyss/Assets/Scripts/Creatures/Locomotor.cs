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
       
    }

    // Update is called once per frame
    void Update () {
    }

    public void Stop()
    {
        Rigidbody.velocity = Vector3.zero;
        //TargetPosition = transform.position;
    }


    
    public void Jet(Vector3 jetDirection, float jetSpeed)
    {
        float xDirectionSign = Mathf.Sign(Random.Range(-1f, 1f));

        jetDirection.x *= xDirectionSign;

        Rigidbody.AddForce(jetDirection.normalized * jetSpeed);
    }

    public void Swim() {

    }
}

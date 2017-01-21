using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody rigidbody;
    public SphereCollider collider;

    public float SurfaceDrag = 3;
    public float UnderwaterDrag = 5f;


    public float AttractionToFinger = 1;


    public float OriginalYDepth = 0;

    void Awake()
    {
        rigidbody.useGravity = false;
    }


    public void Push(Vector3 Direction)
    {
        rigidbody.velocity = Direction;
    }



    void OnTriggerStay(Collider col)
    {
        if (col.tag == "TouchSphere")
        {
            FingerTouching(col as SphereCollider);
        }
    }

    void FingerTouching(SphereCollider finger)
    {
        Vector3 motion = (finger.transform.position - transform.position).normalized;

        Push(motion * AttractionToFinger);
    }
}

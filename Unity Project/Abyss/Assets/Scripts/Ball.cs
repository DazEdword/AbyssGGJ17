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


    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Rubbish")
        {
            TouchRubbish(col.collider.gameObject);
        }
    }

    void TouchRubbish(GameObject RubbishObject)
    {
        RubbishObject.transform.GetChild(0).SetParent(transform);
        Destroy(RubbishObject);
    }


    void Update()
    {
        Cheats();
    }


    void Cheats()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.Instance.Reset();
            GameManager.Instance.ConsoleWrite("Reset");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            rigidbody.useGravity = !rigidbody.useGravity;
            GameManager.Instance.ConsoleWrite("Gravity: " + (rigidbody.useGravity ? "ON" : "OFF"));
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InputManager : MonoBehaviour
{

    public enum InputModes
    {
        FingerFollow,
        DrawCurrent
    }

    public static InputModes InputMode = InputModes.DrawCurrent;


    public SphereCollider TouchSphere;
    public MeshRenderer TouchSphereRenderer;
    public GameObject GestureBall;

    public bool ShowGestures = true;


    public float DrawCurrentMovementSpeed = 5;
    public float DrawCurrentPointDistance = 0.1f;



    Vector3 MouseWorldPosition = Vector3.zero;


    void Update()
    {
        if (!GameManager.Instance.CanReadInput)
            return;

        switch (InputMode)
        {
            case InputModes.FingerFollow:
                InputMode_FollowFinger();
                break;

            case InputModes.DrawCurrent:
                InputMode_DrawCurrent();
                break;
        }


    }


    Vector3 GetWorldTouchedPosition(Vector3 FromPosition, ref bool Contact)
    {
        Vector3 pos = Vector3.zero;
        Plane plane = new Plane(Vector3.forward, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            pos = ray.GetPoint(distance);
            Contact = true;
        }
        else
            Contact = false;

        return pos;
    }


    void InputMode_FollowFinger()
    {
        bool contact = false;

        //Detect touch
        if (Input.GetMouseButton(0))
        {
            MouseWorldPosition = GetWorldTouchedPosition(Input.mousePosition, ref contact);
        }

        if (contact)
        {
            //Put the touch ball
            TouchSphere.transform.position = MouseWorldPosition;
            TouchSphere.enabled = true;
            TouchSphereRenderer.enabled = true;
        }
        else
        {
            TouchSphere.enabled = false;
            TouchSphereRenderer.enabled = false;
        }
    }


    List<Vector3> TouchedPoints = new List<Vector3>();
    List<GameObject> GestureBalls = new List<GameObject>();

    float RegisterMinimumTemp = 0.025f;
    float RegisterClock = 0;
    int MaxLengthOfGesture = 100;

    void InputMode_DrawCurrent()
    {
        bool contact = false;
        Vector3 pos = Vector3.zero;


        if (Input.GetMouseButton(0))
        {
            pos = GetWorldTouchedPosition(Input.mousePosition, ref contact);
        }

        if (RegisterClock > 0)
        {
            RegisterClock -= Time.deltaTime;
        }

        if (contact)
        {
            if (RegisterClock <= 0 && TouchedPoints.Count < MaxLengthOfGesture)
            {
                float distance = float.MaxValue;

                if (TouchedPoints.Count > 2)
                {
                    distance = Vector3.Distance(TouchedPoints.Last(), pos);
                }

                if (distance > DrawCurrentPointDistance)
                {
                    TouchedPoints.Add(pos);
                    SpawnGestureBall(pos);
                    RegisterClock = RegisterMinimumTemp;
                    GameManager.Instance.ConsoleWrite("Registering", 0);
                }
            }
        }
        else
        {
            MoveToPointAndRemoveFromList();
            GameManager.Instance.ConsoleWrite("Moving", 0);
            RegisterClock = 0;
        }
    }

    void MoveToPointAndRemoveFromList()
    {
        if (TouchedPoints.Count < 2)
            return;

        //Move the ball
        Vector3 from = TouchedPoints[0];
        Vector3 to = TouchedPoints[1];

        Vector3 movement = (to - from).normalized * DrawCurrentMovementSpeed;

        GameManager.Instance.Ball.Push(movement);


        //Delete the points

        if (GestureBalls.Count < 1)
            return;

        TouchedPoints.RemoveAt(0);

        Destroy(GestureBalls[0]);
        GestureBalls.RemoveAt(0);

        //Bugfix
        if (TouchedPoints.Count == 1)
        {
            TouchedPoints.RemoveAt(0);
            Destroy(GestureBalls[0]);
            GestureBalls.RemoveAt(0);
        }


    }

    void SpawnGestureBall(Vector3 Position)
    {
        if (!ShowGestures)
        {
            return;
        }

        GameObject obj = GameObject.Instantiate(GestureBall, Position, Quaternion.identity) as GameObject;
        GestureBalls.Add(obj);

        if (GestureBalls.Count > 1)
        {
            obj.transform.LookAt(GestureBalls[GestureBalls.Count - 2].transform);
        }
        else
        {
            obj.transform.rotation = Quaternion.identity;
        }

    }


}

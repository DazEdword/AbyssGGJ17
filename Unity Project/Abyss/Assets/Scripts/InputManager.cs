using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InputManager : MonoBehaviour
{

    public enum InputModes
    {
        FingerFollow,
        DrawCurrent,
        FingerCenterFollow
    }

    public static InputModes InputMode = InputModes.FingerFollow;


    public SphereCollider TouchSphere;
    public MeshRenderer TouchSphereRenderer;
    public GameObject GestureBall;
    public ParticleSystem DragParticles;

    public bool ShowGestures = true;


    public float DrawCurrentMovementSpeed = 5;
    public float DrawCurrentPointDistance = 0.1f;
    public float DrawCurrentBubblesDuration = 1;
    public float DrawCurrentDistanceFromBottle = 3;



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

            case InputModes.FingerCenterFollow:
                InputMode_FingerCenterFollow();
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


    public float FCF_Force = 2;
    public float FCF_DistanceToFinger = 3;


    void InputMode_FingerCenterFollow()
    {
        bool contact = false;
        Vector3 pos = Vector3.zero;
        bool down = false;
        bool up = false;

        //Detect touch
        if (Input.GetMouseButton(0))
        {
            pos = GetWorldTouchedPosition(Input.mousePosition, ref contact);
        }

        down = Input.GetMouseButtonDown(0);
        up = Input.GetMouseButtonUp(0);


        if (contact)
        {
            //User is touching

            Vector3 MotionTowardsFinger = (pos - GameManager.Instance.Ball.transform.position);
            if (MotionTowardsFinger.sqrMagnitude < FCF_DistanceToFinger)
            {
                Debug.Log("MotionTowardsFinger.sqrMagnitude:" + MotionTowardsFinger.sqrMagnitude);
                //GameManager.Instance.Ball.rigidbody.AddForce(MotionTowardsFinger * FCF_Force);

                float diminish = 1 / MotionTowardsFinger.sqrMagnitude;//(MotionTowardsFinger.sqrMagnitude * 1.71848013f - 1.00171848f);

                GameManager.Instance.Ball.rigidbody.velocity = (MotionTowardsFinger * FCF_Force * diminish);

            }
            else
                Debug.Log("Far");


        }
        else
        {
            //User is not touching

        }

    }

    void InputMode_FollowFinger()
    {
        bool contact = false;
        Vector3 pos = Vector3.zero;

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
            TouchSphereRenderer.enabled = ShowGestures;


            //DragParticles.SetActive(true);
            DragParticles.transform.SetParent(GameManager.Instance.Ball.transform);
            DragParticles.transform.localPosition = Vector3.zero;
            DragParticles.transform.position -= Vector3.forward * 0.5f;
            DragParticles.Play();
        }
        else
        {
            TouchSphere.enabled = false;
            TouchSphereRenderer.enabled = false;

            //DragParticles.SetActive(false);
            DragParticles.Stop();

        }
    }


    List<Vector3> TouchedPoints = new List<Vector3>();
    List<GameObject> GestureBalls = new List<GameObject>();
    List<KeyValuePair<GameObject, float>> Bubbles = new List<KeyValuePair<GameObject, float>>();

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
            //DragParticles.SetActive(true);
            DragParticles.transform.position = pos;

        }
        else
        {
            //DragParticles.SetActive(false);
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
                float distanceFromBottle = Vector3.Distance(GameManager.Instance.Ball.transform.position, pos);

                if (TouchedPoints.Count > 2)
                {
                    distance = Vector3.Distance(TouchedPoints.Last(), pos);
                }

                if (distance > DrawCurrentPointDistance && distanceFromBottle < DrawCurrentDistanceFromBottle)
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

        ExtinguishBubbles();
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

        Transform BubbleParticle = obj.transform.GetChild(0);
        BubbleParticle.SetParent(null);
        Bubbles.Add(new KeyValuePair<GameObject, float>(BubbleParticle.gameObject, DrawCurrentBubblesDuration));

        if (GestureBalls.Count > 1)
        {
            obj.transform.LookAt(GestureBalls[GestureBalls.Count - 2].transform);
        }
        else
        {
            obj.transform.rotation = Quaternion.identity;
        }

    }

    void ExtinguishBubbles()
    {
        if (Bubbles.Count < 1)
            return;

        List<GameObject> ToDestroy = new List<GameObject>();

        for (int i = 0; i < Bubbles.Count; i++)
        {
            float newValue = Bubbles[i].Value - Time.deltaTime;
            Bubbles[i] = new KeyValuePair<GameObject, float>(Bubbles[i].Key, newValue);
            if (Bubbles[i].Value <= 0)
            {
                ToDestroy.Add(Bubbles[i].Key);
            }
        }

        while (ToDestroy.Count > 0)
        {
            int index = Bubbles.FindIndex(x => x.Key == ToDestroy[0]);

            if (index >= 0)
            {
                Bubbles.RemoveAt(index);
            }

            Destroy(ToDestroy[0]);
            ToDestroy.RemoveAt(0);
        }

    }


}

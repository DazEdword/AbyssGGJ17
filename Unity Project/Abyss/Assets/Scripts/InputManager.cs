using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public enum InputModes
    {
        FingerFollow,
        DrawCurrent
    }

    public static InputModes InputMode = InputModes.FingerFollow;


    public SphereCollider TouchSphere;
    public MeshRenderer TouchSphereRenderer;

    Vector3 MouseWorldPosition = Vector3.zero;


    void Update()
    {
        if (!GameManager.Instance.CanReadInput)
            return;

        bool contact = false;

        //Detect touch
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.forward, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                MouseWorldPosition = ray.GetPoint(distance);
                contact = true;
            }
        }

        switch (InputMode)
        {
            case InputModes.FingerFollow:
                InputMode_FollowFinger(contact);
                break;

            case InputModes.DrawCurrent:

                break;
        }


    }


    void InputMode_FollowFinger(bool contact)
    {
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



}

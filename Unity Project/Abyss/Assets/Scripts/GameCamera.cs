using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public Camera Camera;
    public Color InitialColor;
    public Color Skycolor;
    public GameObject ObjectToFollow;
    public Light CameraLight;
    [Range(0, 2)]
    public float SpeedX = 1f;
    [Range(0, 2)]
    public float SpeedY = 1f;

    public float ZFactor = 0.01f;

    [HideInInspector]
    public bool CanLookAt = false;

    void FixedUpdate()
    {
        if (!GameManager.Instance.GameStarted)
            return;

        float myY = gameObject.transform.position.y;
        float followY = ObjectToFollow.transform.position.y;

        float myX = gameObject.transform.position.x;
        float followX = ObjectToFollow.transform.position.x;

        float myZ = gameObject.transform.position.z;
        float targetZ = -(5 + (-ZFactor * GameManager.Instance.Ball.transform.position.y));

        float lerpedX = Mathf.Lerp(myX, followX, Mathf.Clamp01(Time.fixedDeltaTime * SpeedX));
        float lerpedY = Mathf.Lerp(myY, followY, Mathf.Clamp01(Time.fixedDeltaTime * SpeedY));
        float lerpedZ = Mathf.Lerp(myZ, targetZ, Time.fixedDeltaTime);


        //this is a super simple linear tween.
        transform.position = new Vector3(lerpedX, lerpedY, lerpedZ);

        if (CanLookAt)
        {
            Quaternion currentR = transform.rotation;
            transform.LookAt(ObjectToFollow.transform);
            Quaternion desiredR = transform.rotation;

            transform.rotation = Quaternion.Lerp(currentR, desiredR, Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }
}

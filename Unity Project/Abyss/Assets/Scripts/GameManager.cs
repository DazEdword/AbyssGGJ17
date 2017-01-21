﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
        InitialCameraPosition = GameCamera.transform.position;
        InitialCameraRotation = GameCamera.transform.rotation;
        InitialBallPosition = Ball.transform.position;
        InitialBallDrag = Ball.rigidbody.drag;
    }


    //Flags
    public bool GameStarted = false;
    public bool CanReadInput = false;

    //Initial positions
    Vector3 InitialCameraPosition;
    Quaternion InitialCameraRotation;
    Vector3 InitialBallPosition;
    float InitialBallDrag;

    //References
    public Light SurfaceLight, UnderwaterLight;
    public Text TapToStartText;
    public GameObject TapToStartArea;
    public GameCamera GameCamera;
    public Ball Ball;
    [SerializeField]
    Text ConsoleText;
    public float TimeToEnterWater = 2;
    public float TimeToReadInput = 2;

    void Start()
    {
        AudioManager.Instance.PlayMusic("overwater_ambient");
    }


    public void StartGame()
    {
        float timeEffect = 1.5f;
        iTween.MoveTo(Camera.main.gameObject, iTween.Hash("z", Camera.main.transform.position.z - 3f, "time", timeEffect, "easetype", iTween.EaseType.easeOutQuad));
        TapToStartArea.SetActive(false);
        Invoke("DropKey", timeEffect);
    }


    void DropKey()
    {
        Ball.rigidbody.useGravity = true;
        Invoke("EnterWater", TimeToEnterWater);
        float r = 2f;
        Ball.rigidbody.drag = Ball.SurfaceDrag;
        Ball.rigidbody.angularVelocity = new Vector3(UnityEngine.Random.Range(-r, r), UnityEngine.Random.Range(-r, r), UnityEngine.Random.Range(-r, r));
        GameStarted = true;
        Ball.rigidbody.drag = InitialBallDrag * 0.1f;
        Invoke("DropSound", 0.825f);
    }

    public void Reset()
    {
        SurfaceLight.enabled = true;
        UnderwaterLight.enabled = false;
        GameCamera.transform.position = InitialCameraPosition;
        GameCamera.transform.rotation = InitialCameraRotation;
        Ball.rigidbody.velocity = Vector3.zero;
        Ball.rigidbody.useGravity = false;
        Ball.transform.position = InitialBallPosition;
        AudioManager.Instance.StopAllSound();
        GameCamera.Camera.clearFlags = CameraClearFlags.Skybox;
        GameCamera.Camera.backgroundColor = GameCamera.InitialColor;
        GameStarted = false;
        CanReadInput = false;
        TapToStartArea.SetActive(true);


        CancelInvoke();
    }


    void EnterWater()
    {
        UnderwaterLight.enabled = true;
        SurfaceLight.enabled = false;
        Ball.rigidbody.drag = Ball.UnderwaterDrag;
        //GameCamera.Light.enabled = true;
        Ball.OriginalYDepth = transform.position.y;
        Ball.rigidbody.drag = InitialBallDrag;
        GameCamera.Camera.clearFlags = CameraClearFlags.SolidColor;
        GameCamera.Camera.backgroundColor = GameCamera.Skycolor;

        Invoke("AllowInput", TimeToReadInput);
    }

    void DropSound()
    {
        AudioManager.Instance.PlaySound("colision-2");
    }

    void AllowInput()
    {
        CanReadInput = true;
    }

    public void ConsoleClear()
    {
        ConsoleText.text = string.Empty;
    }

    public void ConsoleWrite(string Text, float Duration = 2)
    {
        ConsoleText.text = Text;

        if (Duration > 0)
            Invoke("ConsoleClear", Duration);
    }





}

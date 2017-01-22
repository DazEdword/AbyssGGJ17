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

    public ParticleSystem Bubbles;

    void Awake()
    {
        rigidbody.useGravity = false;
        Bubbles.Stop();
    }


    public void Push(Vector3 Direction)
    {
        //rigidbody.velocity = Direction;
        rigidbody.AddForce(Direction);
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
        else if (col.collider.tag == "Rock")
        {
            if (GameManager.Instance.CanReadInput)
                TouchRocks();
        }
        else if(col.collider.tag == "JellyFish")
        {
            if (GameManager.Instance.CanReadInput)
            {
                int random = Random.Range(0, 6);
                if(random == 0)
                {
                    AudioManager.Instance.PlaySound("jellyfishsound_A");
                }
                else if(random == 1)
                {
                    AudioManager.Instance.PlaySound("jellyfishsound_B");
                }
                else if(random == 2)
                {
                    AudioManager.Instance.PlaySound("jellyfishsound_C");
                }
                else if(random == 3)
                {
                    AudioManager.Instance.PlaySound("jellyfishsound_D");
                }
                else if(random == 4)
                {
                    AudioManager.Instance.PlaySound("jellyfishsound_E");
                }
                else if(random == 5)
                {
                    AudioManager.Instance.PlaySound("jellyfishsound_F");
                }
                else
                {
                    AudioManager.Instance.PlaySound("jellyfishsound_G");
                }
                
            }

        }
        else if(col.collider.tag == "Coral")
        {
            if (GameManager.Instance.CanReadInput)
            {
                //AudioManager.Instance.PlaySound("");
            }
        }
    }

    void TouchRubbish(GameObject RubbishObject)
    {
        RubbishObject.transform.GetChild(0).SetParent(transform);
        Destroy(RubbishObject);
    }

    float TimeSinceLastSound = 0;
    void TouchRocks()
    {
        if (Time.time - TimeSinceLastSound > 1)
        {
            AudioManager.Instance.PlaySound(CollisionRandomSound());
            TimeSinceLastSound = Time.time;
        }
    }

    string CollisionRandomSound()
    {
        List<string> sounds = new List<string>() { "colision-0", "colision-1", "colision-3", "colision-4", "colision-5", "colision-6", "colision-7" };

        int random = Random.Range(0, sounds.Count);

        return sounds[random];
    }


    void Update()
    {
        Cheats();
        BubblesEmission();
    }

    public float BubblesEmissionSpeed = 10;
    void BubblesEmission()
    {
        if (!GameManager.Instance.GameStarted)
            return;

        if (rigidbody.velocity.sqrMagnitude > BubblesEmissionSpeed)
        {
            if (!Bubbles.isEmitting && Random.Range(0, 10) > 7)
            {
                Bubbles.Play();
                Invoke("StopBubbles", Random.Range(0.5f, 2));
            }
        }
    }

    void StopBubbles()
    {
        Bubbles.Stop();
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

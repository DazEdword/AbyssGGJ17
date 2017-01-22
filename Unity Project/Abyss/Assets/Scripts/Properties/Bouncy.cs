using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncy : MonoBehaviour {

    public Vector3 bounceDirection;

    public float speed;

    private GameManager gm;
    private Light light;
    private bool lightUp;
	// Use this for initialization
	void Start () {
        gm = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");
        light = (Light)gameObject.GetComponentInChildren(typeof(Light)) as Light;
        lightUp = false;

    }
	
	// Update is called once per frame
	void Update () {
        if (lightUp)
        {
            light.intensity = Mathf.Lerp(light.intensity, 5, 0.1f);
            if(light.intensity > 4.8f)
            {
                lightUp = false;
            }
        }
        else
        {
            light.intensity = Mathf.Lerp(light.intensity, 0, 0.1f);
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.rigidbody.AddExplosionForce(speed, gameObject.transform.position, 50f);
            if (gm.restrictedInput())
            {
                gm.restrictInput();
                gm.Invoke("AllowInput", 1f);
            }
            if (!lightUp)
            {
                lightUp = true;
            }
            
        }
    }
}

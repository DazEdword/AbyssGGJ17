using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

    public static Creature Player;

    public enum CREATURES {Fish, Jellyfish, Anemone, END};

    [HideInInspector]
    public Rigidbody Rigidbody;
    [HideInInspector]
    public Collider Collider;

    //public CREATURES Type = CREATURES.Fish;

    //[Range(1f, 4.5f)]
    //public float speed = 1;

    //public Locomotor.MovementTypes MovementType = Locomotor.MovementTypes.WALK;
    public CREATURES CreatureType = CREATURES.Jellyfish;
    public Locomotor.MovementTypes MovementType;// = Locomotor.MovementTypes.JET;


    //[HideInInspector]
    //public Graphic Graphic;

    [HideInInspector]
    public Locomotor Locomotor;

    [HideInInspector]
    public Brain Brain;

    [HideInInspector]
    public Animator Animator;

    [HideInInspector]
    public SpriteRenderer Sprite;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
        Locomotor = GetComponent<Locomotor>();
        GetBrain();
    }

    public void GetBrain()
    {
        Brain = GetComponent<Brain>();
        if (Brain == null) Debug.LogError("Brain not found for " + gameObject.name);

        if (GetComponents<Brain>().Length > 1)
            Debug.LogError(gameObject.name + " has too many Brains!");
    }
}

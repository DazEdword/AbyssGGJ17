using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

    public static Creature Player;

    public enum CREATURES {Fish, Jellyfish, Anemone, END};

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
}

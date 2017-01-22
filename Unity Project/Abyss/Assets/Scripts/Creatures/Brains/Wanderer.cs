﻿using UnityEngine;

public class Wanderer : Brain
{
    //public float timerToMove = 0;
    public int chanceToStop = 4;

    public float JetSpeed = 20f;
    public float jetFreq = 4;

    public Vector3 jetDirection;

    public void Start()
    {
        InvokeRepeating("Movement", 0, jetFreq);
    }

    void Movement()
    {
        switch (Creature.CreatureType)
        {
            case Creature.CREATURES.Fish:
                Creature.Locomotor.Swim(jetDirection, swimSpeed);
                break;
            case Creature.CREATURES.Jellyfish:
                Jet();
                break;
            case Creature.CREATURES.Anemone:
                break;
            case Creature.CREATURES.END:
                break;
            default:
                break;
        }
    }

    //Jellyfish jet
    void Jet()
    {

        Creature.Locomotor.Jet(jetDirection, JetSpeed);
    }
}
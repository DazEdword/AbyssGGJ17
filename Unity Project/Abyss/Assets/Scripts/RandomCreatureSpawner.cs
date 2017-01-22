using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomCreatureSpawner : MonoBehaviour
{
    [System.Serializable]
    public class CreatureProbability
    {
        public GameObject Prefab;
        public int Weight;
    }


    public float Frequency = 10;
    public float ChanceOfCreature = 1f;
    public List<CreatureProbability> Probabilities = new List<CreatureProbability>();

    void Start()
    {
        InvokeRepeating("TrySpawnCreature", 0, Frequency);
    }


    List<GameObject> Slots = new List<GameObject>();

    void TrySpawnCreature()
    {
        if (Probabilities.Count < 1)
            return;

        if (Random.Range(0, 1) > ChanceOfCreature)
        {
            return;
        }

        Slots.Clear();

        foreach (var entry in Probabilities)
        {
            for (int i = 0; i < entry.Weight; i++)
            {
                Slots.Add(entry.Prefab);
            }
        }


        int r = Random.Range(0, Slots.Count);

        GameObject chosenCreatureToSpawn = Slots[r];
        SpawnCreatureAround(chosenCreatureToSpawn);
    }

    void SpawnCreatureAround(GameObject Creature)
    {
        Vector3 Position = transform.position;

        Position.x = Mathf.Sign(Random.Range(-1, 1)) * 10;
        Position.y += Random.Range(-5, 5);
        Position.z += Mathf.Sign(Random.Range(-1, 1)) * 5;

        GameObject newCreature = Instantiate(Creature, Position, Quaternion.identity) as GameObject;
        newCreature.transform.LookAt(transform);

    }
}

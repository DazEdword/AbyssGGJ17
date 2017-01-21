using UnityEngine;

public class Wanderer : Brain
{
    
    //TODO add a "pack" mode, with no random side
    
    //public float timerToMove = 0;
    public int chanceToStop = 4;

    public float jetSpeed = 20f;
    public float jetFreq = 1;

    public float swimSpeed = 10f;
    //public float jetFreq = 1;

    public Vector3 jetDirection;

    /*
    public override void GetInput()
    {
        //base.GetInput();

        if (timerToMove <= 0)
            MoveToRandomPosition();
        else
        {
            //timerToMove -= Creature.RefreshFrequency;
            if (timerToMove < 0)
                timerToMove = 0;
        }
    } */

    /*
    private void MoveToRandomPosition()
    {
        if (Random.Range(1, 10) < chanceToStop) {
            Creature.Locomotor.Stop();
        }
        else
        {
            Vector3 nuPos = transform.position;
            nuPos.x += (.5f - Random.value) * 5.0f;
            nuPos.z += (.5f - Random.value) * 3.0f;
            Creature.Locomotor.TargetPosition = nuPos;
        }

        timerToMove = 1;
    } */

    public void Start() {
        WanderFloat();
    }
    //Jellyfish jet
    public void WanderFloat() {
        InvokeRepeating("Jet", 0, jetFreq);
    }

    void Jet()
    {
        Creature.Locomotor.Jet(jetDirection, jetSpeed);
    }
}
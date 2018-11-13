using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class SheepController : MonoBehaviour {

    public StateMachine<SheepController> stateMachine { get; set; }

    private float nextActionTime = 0.0f;
    private float period = 1f;

    [Header("Hunger Variables")] [Space(5)]
    public int hunger;
    public int hungerIncrease = 1;
    public int foodIncrease = 3;
    public int fullBelly = 100;

    public bool isEating = false;
    public bool hungry = false;

    private int hungryPoint = 25;

    [Header("Movement Variables")] [Space(5)]
    public float movementSpeed;


    private void Start()
    {
        stateMachine = new StateMachine<SheepController>(this);
        stateMachine.ChangeState(WanderingState.Instance);

        hunger = fullBelly;
    }

    private void Update()
    {
        HungerControl();
        stateMachine.Update();
    }

    private void HungerControl()
    {
        if (!isEating)
        {
            if (Time.time > nextActionTime)
            {
                nextActionTime += period;
                hunger -= hungerIncrease;
            }
        }
        else
        {
            nextActionTime = Time.time;
        }

        if (!hungry)
        {
            if (hunger <= hungryPoint)
            {
                hungry = true;
                stateMachine.ChangeState(HungerState.Instance);
            }
        }

        if (hunger == fullBelly)
            hungry = false;
    }
}

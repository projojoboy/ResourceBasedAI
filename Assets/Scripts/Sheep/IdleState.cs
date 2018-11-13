using StateStuff;
using UnityEngine;

public class IdleState : State<SheepController>
{
    private static IdleState _instance;

    private IdleState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static IdleState Instance
    {
        get
        {
            if (_instance == null)
            {
                new IdleState();
            }

            return _instance;
        }
    }

    private float timer = 5;
    private float startTimer;

    private bool walkAround = false;

    Animator anim;

    public override void EnterState(SheepController _owner)
    {
        Debug.Log("Entering Idle State");
        startTimer = timer;
        //Start Idle Animation

        anim = _owner.GetComponent<Animator>();
        anim.Play("idle");
    }

    public override void ExitState(SheepController _owner)
    {
        Debug.Log("Exiting Idle State");
    }

    public override void UpdateState(SheepController _owner)
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            timer = startTimer;

            var randBool = Random.Range(0f,1f);
            if (randBool > 0.5)
                walkAround = true;

            Debug.Log("Randbool: " + randBool + ", walkAround: " + walkAround);

            if (walkAround)
            {
                _owner.stateMachine.ChangeState(WanderingState.Instance);
            }
        }
    }
}

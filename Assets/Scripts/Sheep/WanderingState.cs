using System.Collections;
using System.Collections.Generic;
using StateStuff;
using UnityEngine;

public class WanderingState : State<SheepController>
{
    private static WanderingState _instance;

    private WanderingState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static WanderingState Instance
    {
        get
        {
            if (_instance == null)
            {
                new WanderingState();
            }

            return _instance;
        }
    }

    private Vector2 desPos;

    Animator anim;

    public override void EnterState(SheepController _owner)
    {
        Debug.Log("Entering Wandering State");

        desPos = Random.insideUnitCircle * 5;

        float ownerX = _owner.transform.position.x;

        anim = _owner.GetComponent<Animator>();
        if (desPos.x <= ownerX)
            anim.Play("walkLeft");
        else if (desPos.x > ownerX)
            anim.Play("walkRight");
    }

    public override void ExitState(SheepController _owner)
    {
        Debug.Log("Exiting Wandering State");
    }

    public override void UpdateState(SheepController _owner)
    {
        _owner.transform.position = Vector2.MoveTowards(_owner.transform.position, desPos, _owner.movementSpeed * Time.deltaTime);

        if (_owner.transform.position.x == desPos.x && _owner.transform.position.y == desPos.y) {
            _owner.stateMachine.ChangeState(IdleState.Instance);
        }
    }
}

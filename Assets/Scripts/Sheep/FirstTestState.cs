using StateStuff;
using UnityEngine;

public class FirstTestState : State<SheepController>
{
    private static FirstTestState _instance;
    bool switchState = false;

    private FirstTestState()
    {
        if(_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static FirstTestState Instance
    {
        get
        {
            if(_instance == null)
            {
                new FirstTestState();
            }

            return _instance;
        }
    }

    public override void EnterState(SheepController _owner)
    {
        Debug.Log("Entering First State");
    }

    public override void ExitState(SheepController _owner)
    {
        Debug.Log("Exiting First State");
    }

    public override void UpdateState(SheepController _owner)
    {
        if (switchState)
        {
            _owner.stateMachine.ChangeState(SecondTestState.Instance);
        }
    }
}

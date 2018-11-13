using StateStuff;
using UnityEngine;

public class SecondTestState : State<SheepController>
{
    private static SecondTestState _instance;
    bool switchState = false;

    private SecondTestState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static SecondTestState Instance
    {
        get
        {
            if (_instance == null)
            {
                new SecondTestState();
            }

            return _instance;
        }
    }

    public override void EnterState(SheepController _owner)
    {
        Debug.Log("Entering Second State");
    }

    public override void ExitState(SheepController _owner)
    {
        Debug.Log("Exiting Second State");
    }

    public override void UpdateState(SheepController _owner)
    {
        if (switchState)
        {
            _owner.stateMachine.ChangeState(FirstTestState.Instance);
        }
    }
}

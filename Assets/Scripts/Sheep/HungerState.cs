using StateStuff;
using UnityEngine;

public class HungerState : State<SheepController>
{
    private static HungerState _instance;

    public GameObject[] foodSources;

    private int foodIndex = -1;

    private bool atFood;

    private float nextActionTime = 0.0f;
    private float period = 1f;


    private HungerState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }
    public static HungerState Instance
    {
        get
        {
            if (_instance == null)
            {
                new HungerState();
            }

            return _instance;
        }
    }

    Animator anim;
    Vector3 foodSourcePos;

    public override void EnterState(SheepController _owner)
    {
        Debug.Log("Entering Hunger State");

        atFood = false;

        //Find closest food source
        foodSources = GameObject.FindGameObjectsWithTag("FoodSource");
        float smallestDistance = 999;
        for (int i = 0; i < foodSources.Length; i++)
        {
            float currentMagnitude = Mathf.Abs(Vector3.Magnitude(foodSources[i].transform.position));
            if (currentMagnitude <= smallestDistance)
            {
                smallestDistance = currentMagnitude;
                foodIndex = i;
            }
        }

        foodSourcePos = foodSources[foodIndex].transform.position;

        float ownerX = _owner.transform.position.x;

        anim = _owner.GetComponent<Animator>();
        if (foodSourcePos.x <= ownerX)
            anim.Play("walkLeft");
        else if (foodSourcePos.x > ownerX)
            anim.Play("walkRight");
    }

    public override void ExitState(SheepController _owner)
    {
        Debug.Log("Exiting Hunger State");
    }

    public override void UpdateState(SheepController _owner)
    {
        //Walk towards closest food source
        if (!atFood)
        {
            _owner.transform.position = Vector2.MoveTowards(_owner.transform.position, foodSourcePos, _owner.movementSpeed * Time.deltaTime);
            nextActionTime = Time.time;
        }
        //if at food source
        if(_owner.transform.position == foodSourcePos)
        {
            //Go eat
            anim.SetBool("startEating", true);

            int addFoodAmount = _owner.foodIncrease;
            atFood = true;

            if (_owner.hunger + _owner.foodIncrease > _owner.fullBelly)
            {
                addFoodAmount = _owner.fullBelly - _owner.hunger;
            }
            else
                addFoodAmount = _owner.foodIncrease;

            //Is belly full
            if (_owner.hunger != _owner.fullBelly)
            {
                _owner.isEating = true;
                //No? Continue eating
                if (Time.time > nextActionTime)
                {
                    nextActionTime += period;
                    _owner.hunger += addFoodAmount;
                }
            }
            else
            {
                //Yes? Stop eating and change state
                anim.SetBool("stopEating", true);

                anim.SetBool("startEating", false);
                anim.SetBool("stopEating", false);
                _owner.isEating = false;
                _owner.stateMachine.ChangeState(WanderingState.Instance);
            }
        }
    }
}

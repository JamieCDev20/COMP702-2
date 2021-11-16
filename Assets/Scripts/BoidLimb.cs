using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidLimb : MonoBehaviour
{

    [SerializeField] private BoidType type;

    private void Act()
    {
        switch (type)
        {
            case BoidType.LoadBearing:
                LoadBearRules();
                break;
            case BoidType.CounterWeight:
                CounterWightRules();
                break;
            case BoidType.Leader:
                LeadRules();
                break;
            default:
                break;
        }
    }

    private void LoadBearRules()
    {
        // Cant move if carrying too much weight

        // Move to keep the predicted COG between all load bearers
    }

    private void CounterWightRules()
    {
        // Try to stay on the correct side of the body

        //Something else about balancing the body, don't know yet though
    }

    private void LeadRules()
    {
        // Move in the direction of the input with respect to the distance from the COM
    }

}

public enum BoidType
{
    LoadBearing,
    CounterWeight,
    Leader
}

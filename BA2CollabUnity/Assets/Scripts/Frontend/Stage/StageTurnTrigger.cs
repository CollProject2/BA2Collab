using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTurnTrigger : MonoBehaviour
{
    public enum TurnDirection
    {
        clockWise,
        counterClockWise
    }

    public TurnDirection turnDirection;
    private DoorAdjuster doorAdjuster;

    private void Awake()
    {
        doorAdjuster = gameObject.GetComponentInParent<DoorAdjuster>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnDoorInteract();
        }
    }

    void OnDoorInteract()
    {
        switch (turnDirection)
        {
            case TurnDirection.clockWise:
                doorAdjuster.AdjustDoors();
                // player is idle animation
                Player.instance.animator.SetBool("isMoving",false);
                // call stage animation 
                Environment.instance.TurnEnvironmentClockWise();
                break;
            case TurnDirection.counterClockWise:
                doorAdjuster.AdjustDoors();
                // player is idle animation
                Player.instance.animator.SetBool("isMoving",false);
                // call stage animation 
                Environment.instance.TurnEnvironmentCounterClockWise();
                break;
        }
    }
}

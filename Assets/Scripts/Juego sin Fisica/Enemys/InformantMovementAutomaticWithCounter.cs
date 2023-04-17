using System;
using UnityEngine;

/// <summary>
/// La clase informant comunica al ControllerWinLose los contadores de movimiento en X e Y del objeto
/// </summary>

public class InformantMovementAutomaticWithCounter : MonoBehaviour
{
    private MovementAutomaticWithCounter movementAutomaticWithCounter;
    private int actualCounterX;
    private int actualCounterY;

    ControllerAddPointOrDead cwl;

    private InformantMovementAutomaticWithCounter()
    {

    }

    private void Awake()
    {
        cwl = FindObjectOfType<ControllerAddPointOrDead>();
    }
  
    private void Start()
    {
        try
        {
            movementAutomaticWithCounter = GetComponent<MovementAutomaticWithCounter>();
            cwl.AddGameObject(this.gameObject, movementAutomaticWithCounter.CounterX, movementAutomaticWithCounter.CounterY);
        }catch
        {
            Debug.LogError("El objeto necesita contener la clase MovementAutomaticWithCounter para utilizar el informante");
        }
    }

    private void Update()
    {
        if (movementAutomaticWithCounter.CounterX != actualCounterX || movementAutomaticWithCounter.CounterY != actualCounterY)
        {
            actualCounterX = movementAutomaticWithCounter.CounterX;
            actualCounterY = movementAutomaticWithCounter.CounterY;
            cwl.NewPosition(this.gameObject, movementAutomaticWithCounter.CounterX,movementAutomaticWithCounter.CounterY);
        }
    }
}

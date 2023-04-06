using System;
using UnityEngine;

/// <summary>
/// La clase informant comunica al ControllerWinLose los contadores de movimiento en X e Y del objeto
/// </summary>

public class Informant : MonoBehaviour
{
    enum TypeInformant { Movement, MovementAutomaticWithCounter }
    [SerializeField] private TypeInformant type;
    private Movement movement;
    private MovementAutomaticWithCounter movementAutomaticWithCounter;
    private int actualCounterX;
    private int actualCounterY;

    ControllerAddPointOrLose cwl;

    private Informant()
    {

    }

    private void Awake()
    {
        cwl = FindObjectOfType<ControllerAddPointOrLose>();
    }

    private void Start()
    {
        try
        {
            if (type == TypeInformant.Movement)
            {
                movement = GetComponent<Movement>();
                cwl.AddGameObject(this.gameObject, movement.CounterX, movement.CounterY);
            }
            else
            {
                movementAutomaticWithCounter = GetComponent<MovementAutomaticWithCounter>();
                cwl.AddGameObject(this.gameObject, movementAutomaticWithCounter.CounterX);
            }
        }catch
        {
            if (type == TypeInformant.Movement)
                Debug.LogError("El objeto necesita contener la clase Movement para utilizar el informante");
            else
                Debug.LogError("El objeto necesita contener la clase MovementAutomaticWithCounter para utilizar el informante");
        }
    }

    private void Update()
    {
        if (type == TypeInformant.Movement)
        {
            if (movement.CounterX != actualCounterX || movement.CounterY != actualCounterY)
            {
                actualCounterX = movement.CounterX;
                actualCounterY = movement.CounterY;
                cwl.NewPosition(this.gameObject, movement.CounterX, movement.CounterY);
            }
        }
        else
        {
            if (movementAutomaticWithCounter.CounterX != actualCounterX )
            {
                actualCounterX = movementAutomaticWithCounter.CounterX;
                cwl.NewPosition(this.gameObject, movementAutomaticWithCounter.CounterX);
            }
        }
    }
}

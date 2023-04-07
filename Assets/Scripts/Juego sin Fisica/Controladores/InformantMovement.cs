using System;
using UnityEngine;

/// <summary>
/// La clase informantMovement comunica al ControllerWinLose los contadores de movimiento en X e Y del jugador
/// </summary>

public class InformantMovement : MonoBehaviour
{
    private Movement movement;
    [SerializeField] private int actualCounterX;
    [SerializeField] private int actualCounterY;
    private Player player;
    private bool deadNotification=false;

    ControllerAddPointOrDead cwl;
    private InformantMovement()
    {

    }

    private void Awake()
    {
        cwl = FindObjectOfType<ControllerAddPointOrDead>();
        player = FindObjectOfType<Player>();

    }


    private void Start()
    {
        try
        {
            movement = GetComponent<Movement>();
            cwl.AddGameObject(this.gameObject, movement.CounterX, movement.CounterY);
        }catch
        {
            Debug.LogError("El objeto necesita contener la clase Movement para utilizar el informante");
        }
    }

    private void Update()
    {
        if (deadNotification)
            player.Died();

        if(!deadNotification)
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
            deadNotification = false;
            movement.ResetVectorOriginalPosition();
        }


    }

    public void DeadNotification()
    {
        deadNotification = true;
    }
}

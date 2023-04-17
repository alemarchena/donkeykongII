using System;
using UnityEngine;

/// <summary>
/// La clase informantMovement comunica al ControllerWinLose los contadores de movimiento en X e Y del jugador
/// </summary>

public class InformantMovement : MonoBehaviour
{
    [SerializeField] private int actualCounterX;
    [SerializeField] private int actualCounterY;

    private ControllerAddPointOrDead controllerAddPointOrDead;
    private Player player;
    [SerializeField] private ControllerCollisionPlayerKey controllerMovementKey;

    [SerializeField] private IntentMovement intentMovement;
    private bool deadNotification=false;
    private InformantMovement()
    {

    }

    private void Awake()
    {
        controllerAddPointOrDead = FindObjectOfType<ControllerAddPointOrDead>();
        player = FindObjectOfType<Player>();
        controllerMovementKey = FindObjectOfType<ControllerCollisionPlayerKey>();
    }


    private void Start()
    {
        try
        {
            intentMovement = GetComponent<IntentMovement>();
            controllerAddPointOrDead.AddGameObject(this.gameObject, intentMovement.CounterX, intentMovement.CounterY);
        }catch
        {
            if(!intentMovement)
            Debug.LogError("El objeto necesita contener la clase IntentMovement para utilizar el informante");

            if (!controllerAddPointOrDead)
                Debug.LogError("El objeto necesita contener la clase ControllerCollisionPlayerKey para utilizar el informante");
        }
    }

    private void Update()
    {
        if (deadNotification)
            player.Died();

        if(!deadNotification)
        {
            if (intentMovement.CounterX != actualCounterX || intentMovement.CounterY != actualCounterY)
            {
                actualCounterX = intentMovement.CounterX;
                actualCounterY = intentMovement.CounterY;

                controllerAddPointOrDead.NewPosition(this.gameObject, actualCounterX, actualCounterY);
                controllerMovementKey.NewPositionPlayer(this.gameObject, actualCounterX, actualCounterY);
            }
        }
        else
        {
            deadNotification = false;
            intentMovement.ResetVectorOriginalPosition();
        }
    }

    public void DeadNotification()
    {
        deadNotification = true;
    }
}

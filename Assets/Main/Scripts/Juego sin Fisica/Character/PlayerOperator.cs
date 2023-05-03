
using UnityEngine;

/// <summary>
/// La clase informantMovement comunica al ControllerWinLose los contadores de movimiento en X e Y del jugador
/// </summary>

public class PlayerOperator : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    private PlayerMovementIntent playerIntentMovement;
    private ControllerSound controllerSound;
    private ControllerUI controllerUI;

    private int actualCounterX;
    private int actualCounterY;
    private bool deadNotification=false;
    private bool winPointNotification=false;
    private ControllerAddPointOrDead cAddDead;
    private ControllerCollisionPlayerKey controllerMovementKey;
    private PlayerOperator(){}
    private void Awake()
    {
        try
        {
            cAddDead = FindObjectOfType<ControllerAddPointOrDead>();
            controllerUI = FindObjectOfType<ControllerUI>();

            controllerMovementKey = FindObjectOfType<ControllerCollisionPlayerKey>();
            if(!playerData) Debug.LogError("Falta asociar el Player a èste objeto");

            playerIntentMovement = GetComponent<PlayerMovementIntent>();
            cAddDead.AddGameObject(this.gameObject, playerIntentMovement.CounterX, playerIntentMovement.CounterY);
           
            controllerSound = FindObjectOfType<ControllerSound>();

            if (!controllerSound) Debug.LogError("Falta el ControllerSound en el juego");

            ReInit();
        }
        catch
        {
            if (!cAddDead) Debug.LogError("Falta el objeto ControllerAddPointOrDead en el juego");
            if (!controllerMovementKey) Debug.LogError("Falta el objeto ControllerCollisionPlayerKey en el juego");
            if (!playerIntentMovement) Debug.LogError("El objeto necesita contener la clase IntentMovement para utilizar el informante");
        }
    }

    public void ReInit()
    {
        playerIntentMovement.ResetVectorOriginalPosition();
    }

    private void Update()
    {
        if (winPointNotification) { 
            playerData.AddPoint();
            winPointNotification = false;
        }

        if(!deadNotification)
        {
            if (playerIntentMovement.CounterX != actualCounterX || playerIntentMovement.CounterY != actualCounterY)
            {
                actualCounterX = playerIntentMovement.CounterX;
                actualCounterY = playerIntentMovement.CounterY;

                cAddDead.NewPosition(this.gameObject, actualCounterX, actualCounterY);
                controllerMovementKey.NewPositionPlayer(this.gameObject, actualCounterX, actualCounterY);
            }
        }
        else
        {
            deadNotification = false;
            controllerSound.PlayLostLifePlayer();
            playerData.DiscountLife();
            controllerUI.CreateUIlife();
            playerIntentMovement.ResetVectorOriginalPosition();
        }
    }

    public void DeadNotification()
    {
        deadNotification = true;
    }

    public void WinPointNotification()
    {
        winPointNotification = true;
    }
}

using System;
using UnityEngine;

/// <summary>
/// La clase informantMovement comunica al ControllerWinLose los contadores de movimiento en X e Y del jugador
/// </summary>

public class PlayerInformant : MonoBehaviour
{
    [SerializeField] private int actualCounterX;
    [SerializeField] private int actualCounterY;

    private ControllerAddPointOrDead controllerAddPointOrDead;
    [SerializeField] Player player;
    private ControllerCollisionPlayerKey controllerMovementKey;

    [SerializeField] private PlayerMovementIntent playerIntentMovement;
    private bool deadNotification=false;
    private bool winPointNotification=false;
    public bool PlayerItsAlive { get; private set; }
    public int PlayerLife { get; private set; }
    public int PlayerInitialLife { get; private set; }
    public int PlayerPoints { get; private set; }
    public bool Stoped { get; private set; }

    private PlayerInformant()
    {

    }

    private void Awake()
    {

        try
        {
            controllerAddPointOrDead = FindObjectOfType<ControllerAddPointOrDead>();
            //player = FindObjectOfType<Player>();
            controllerMovementKey = FindObjectOfType<ControllerCollisionPlayerKey>();
            if(!player) Debug.LogError("Falta asociar el Player a èste objeto");

            playerIntentMovement = GetComponent<PlayerMovementIntent>();
            controllerAddPointOrDead.AddGameObject(this.gameObject, playerIntentMovement.CounterX, playerIntentMovement.CounterY);

            ReInit();
        }
        catch
        {
            if (!controllerAddPointOrDead) Debug.LogError("Falta el objeto ControllerAddPointOrDead en el juego");
            if (!controllerMovementKey) Debug.LogError("Falta el objeto ControllerCollisionPlayerKey en el juego");
            if (!playerIntentMovement) Debug.LogError("El objeto necesita contener la clase IntentMovement para utilizar el informante");
        }

    }


    public void ReInit()
    {
        Stoped = false;
        player.ReInit();
        PlayerItsAlive = player.ItsAlive;
        PlayerInitialLife = player.InitialLife;
        playerIntentMovement.ResetVectorOriginalPosition();

    }

    private void Update()
    {
        if (!Stoped)
        {

            if (winPointNotification) { 
                player.AddPoint();
                winPointNotification = false;
            }

            if(!deadNotification)
            {
                if (playerIntentMovement.CounterX != actualCounterX || playerIntentMovement.CounterY != actualCounterY)
                {
                    actualCounterX = playerIntentMovement.CounterX;
                    actualCounterY = playerIntentMovement.CounterY;

                    controllerAddPointOrDead.NewPosition(this.gameObject, actualCounterX, actualCounterY);
                    controllerMovementKey.NewPositionPlayer(this.gameObject, actualCounterX, actualCounterY);
                }
            }
            else
            {
                player.DiscountLife();
                deadNotification = false;
                playerIntentMovement.ResetVectorOriginalPosition();
            }

            if (player.Life <= 0)
            {
                PlayerItsAlive = false;
                Stoped = true;
            }

            PlayerPoints = player.Points;
            PlayerLife = player.Life;
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

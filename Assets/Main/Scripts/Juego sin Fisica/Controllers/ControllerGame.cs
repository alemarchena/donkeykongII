using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class ControllerGame : MonoBehaviour
{
    [SerializeField] Configuration configuration;
    [SerializeField] PlayerData playerData;
    ControllerSound controllerSound;
    ControllerUI controllerUI;

    public bool Winner { get; private set; }
    public bool Loser { get; private set; }
    public bool Playing { get; private set; } 
    public bool ReStart { get; private set; }

    private KeyOperator keyOperator;
    private PlayerOperator playerOperator;
    private void Awake()
    {
        try
        {
            keyOperator = FindObjectOfType<KeyOperator>();
            playerOperator = FindObjectOfType<PlayerOperator>();
            controllerUI = FindObjectOfType<ControllerUI>();
            controllerSound = FindObjectOfType<ControllerSound>();

            if (!playerData) Debug.LogError("Falta el componente PlayerData");
            if (!configuration) Debug.LogError("Falta asignar el archivo Configuration");
            if (!controllerSound) Debug.LogError("Falta el ControllerSound en el juego");
            if (!controllerUI) Debug.LogError("Falta el ControllerUI en el juego");

            Playing = false;
            Loser = false;
            Winner = false;
            ReStart = false;

        }
        catch
        {
            if (!keyOperator) Debug.LogError("Falta el componente KeyInformant");
            if (!playerOperator) Debug.LogError("Falta el componente PlayerInformant");
        }
    }
    private void Start()
    {
        StartCoroutine(WaitWinner());
    }

    public void Play()
    {
        ReStart = false;
        configuration.ReInit();
        playerData.ReInit();
        ReInit();
        StartCoroutine(RetardPlaying());
    }

    private void Update()
    {
        if(Playing)
        {
            if (keyOperator.KeyComplete)
            { 
                Winner = true;
                Playing = false;
            }

            if (playerData.Life<=0)
            {
                controllerSound.PlayDeadPlayer();
                Loser = true;
                Playing = false;
            }
        }
    }

    IEnumerator WaitWinner()
    {
        yield return new WaitUntil(HasWinner);
        if (Winner)
        {
            ReInit();
            playerData.ReStart();
            ReStart = true;
            controllerUI.ReInit();
            StartCoroutine(RetardPlaying());
            configuration.IncrementVelocity();
            StartCoroutine(WaitWinner());
        }
    }
    IEnumerator RetardPlaying()
    {
        yield return new WaitForSeconds(0.3f);
        Playing = true;
        controllerUI.ReInit();

    }
    private bool HasWinner()
    {
        return Winner;
    }


    private void ReInit()
    {
        Winner = false;
        Loser = false;
        keyOperator.ReInit();
        playerOperator.ReInit();
    }
}

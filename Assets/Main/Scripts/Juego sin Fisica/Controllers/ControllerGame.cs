using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class ControllerGame : MonoBehaviour
{
    [SerializeField] Configuration configuration;
    [SerializeField] EnemyList enemyList;
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
            enemyList = FindObjectOfType<EnemyList>();

            if (!playerData) Debug.LogError("Falta el componente PlayerData");
            if (!configuration) Debug.LogError("Falta asignar el archivo Configuration");
            if (!controllerSound) Debug.LogError("Falta el ControllerSound en el juego");
            if (!controllerUI) Debug.LogError("Falta el ControllerUI en el juego");
            if (!enemyList) Debug.LogError("Falta el EnemyList en el juego");

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
        
        ReInit();
        StartCoroutine(RetardPlaying());
        controllerSound.PlayGame();

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
            yield return new WaitForSeconds(2f);

            ReInit();
            playerData.ReStart();
            ReStart = true;
            configuration.IncrementVelocity();
            StartCoroutine(RetardPlaying());

            controllerUI.ReInit();

            StartCoroutine(WaitWinner());
        }
    }
    IEnumerator RetardPlaying()
    {
        yield return new WaitForSeconds(0.3f);
        Playing = true;
        controllerUI.ReInit();
        yield return new WaitForSeconds(0.5f);
        controllerSound.PlayGeneral();


    }
    private bool HasWinner()
    {
        return Winner;
    }

    public void Reset()
    {
        configuration.Reset();
    }

    public void ReInit()
    {
        Winner = false;
        Loser = false;
        keyOperator.ReInit();
        playerOperator.ReInit();
        enemyList.ReInit();
        playerData.ReInit();
    }
}

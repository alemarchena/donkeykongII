using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControllerGame : MonoBehaviour
{
    [SerializeField] Configuration configuration;
    [SerializeField] PlayerData playerData;
    ControllerSound controllerSound;

    public bool Winner { get; private set; }
    public bool Loser { get; private set; }
    public bool Playing { get; private set; } 

    private KeyOperator keyOperator;
    private PlayerOperator playerOperator;
    private void Awake()
    {
        try
        {
            keyOperator = FindObjectOfType<KeyOperator>();
            playerOperator = FindObjectOfType<PlayerOperator>();

            if (!playerData)
                Debug.LogError("Falta el componente PlayerData");

            if (!configuration) Debug.LogError("Falta asignar el archivo Configuration");

            controllerSound = FindObjectOfType<ControllerSound>();
            if (!controllerSound) Debug.LogError("Falta el ControllerSound en el juego");

            Playing = false;
        }
        catch
        {
            if (!keyOperator) Debug.LogError("Falta el componente KeyInformant");
            if (!playerOperator) Debug.LogError("Falta el componente PlayerInformant");
        }
    }


    public void Play()
    {
        configuration.ReInit();

        Winner = false;
        Loser = false;
        
        keyOperator.ReInit();
        playerOperator.ReInit();
        playerData.ReInit();
        StartCoroutine(RetardPlaying());
    }


    IEnumerator RetardPlaying()
    {
        yield return new WaitForSeconds(0.3f);
        Playing = true;
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
}

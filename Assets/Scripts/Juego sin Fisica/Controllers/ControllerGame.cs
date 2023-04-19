using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControllerGame : MonoBehaviour
{
    PlayerInformant playerInformant;
    KeyInformant keyInformant;

    public bool Winner { get; private set; }
    public bool Loser { get; private set; }
    public bool Playing { get; private set; } 
    public int PlayerPoints { get; private set; }
    public bool PlayerItsAlive { get; private set; }
    public int PlayerLife { get; private set; }
    public int PlayerInitialLife { get; private set; }


    private void Awake()
    {
        try
        {
            Playing = false;
            keyInformant = FindObjectOfType<KeyInformant>();
            playerInformant = FindObjectOfType<PlayerInformant>();
        }
        catch
        {
            if (!keyInformant) Debug.LogError("Falta el componente KeyInformant");
            if (!playerInformant) Debug.LogError("Falta el componente PlayerInformant");

        }
        GetDataInformantPlayer();
    }


    public void Play()
    {
        Winner = false;
        Loser = false;
        keyInformant.ReInit();
        playerInformant.ReInit();
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
            if (keyInformant.KeyComplete)
            { 
                Winner = true;
                Playing = false;
            }

            if (playerInformant.PlayerItsAlive == false )
            {
                Loser = true;
                Playing = false;
            }

            if (playerInformant.PlayerLife <= 0) { 
                Playing = false;
            }

            GetDataInformantPlayer();
        }
    }

    private void GetDataInformantPlayer()
    {
        PlayerPoints = playerInformant.PlayerPoints;
        PlayerLife = playerInformant.PlayerLife;
        PlayerInitialLife = playerInformant.PlayerInitialLife;
        PlayerItsAlive = playerInformant.PlayerItsAlive;
    }
}

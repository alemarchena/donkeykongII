using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerUI : MonoBehaviour
{
    ControllerGame controllerGame;

    [Tooltip("Es el objeto de tipo Text ubicado en el canvas del juego para mostrar los puntos")]
    [SerializeField] Text textPoints;
    [Tooltip("Es el objeto de tipo Text ubicado en el canvas del juego para mostrar Ganar o Perder")]
    [SerializeField] Text textWinLose;
    [Space]
    [Tooltip("Es el sprite que muestra la cantidad de vidas en el juego")]
    [SerializeField] GameObject prefabSpritelife;
    [SerializeField] SpriteRenderer spritePlayer;
    [SerializeField] SpriteRenderer spriteKey;
    [SerializeField] SpriteRenderer spriteDonKeyKong;
    [SerializeField] SpriteRenderer spriteDonKeyKongfree;
    [SerializeField] SpriteRenderer spriteDonKeyKongwinner;

    [Space]
    [SerializeField] GameObject buttonPlay;
    [SerializeField] PlayerData playerData;

    private bool stoped;


    [Tooltip("Es el contenedor que tendra como hijo al sprite que muestra la cantidad de vidas en el juego")]
    [SerializeField] Transform content;
    private int actualPlayerLife;

    private float stepPositionPrefab = 0.5f;
    private List<GameObject> listGameObjectsPrefab = new();

    private KeyOperator keyOperator;
    private Lock listLock;

    

    private bool readyReInit;


    private void Awake()
    {
        if (!playerData) Debug.LogError("Falta asociar el Player a èste objeto");

        listLock = FindObjectOfType<Lock>();
        if (!listLock) Debug.LogError("Falta asociar la clase Lock a èste objeto");

        keyOperator = FindObjectOfType<KeyOperator>();
        if (!keyOperator) Debug.LogError("Falta el componente KeyInformant");

        if (!prefabSpritelife) Debug.LogError("Falta el componente Sprite para las vidas del player");
        if (!spritePlayer) Debug.LogError("Falta el componente Sprite del player");
        if (!spriteKey) Debug.LogError("Falta el componente Sprite de la llave");
        if (!spriteDonKeyKong) Debug.LogError("Falta el componente Sprite de la DonKeyKong");
        
        stoped = true;
        readyReInit = false;
        ActivationSpritePlayer(false);
        ActivationSpriteDonKeyKongFree(false);
        ActivationSpriteDonKeyKongWinner(false);

    }
    void Start()
    {
        controllerGame= FindObjectOfType<ControllerGame>();
    }

    void Update()
    {
        if (controllerGame.Playing && readyReInit)
        {
            ActivationSpritePlayer(true);
            ActivationSpriteKey(true);
            ActivationSpriteDonKeyKong(true);
            stoped = false;
            readyReInit = false;
            actualPlayerLife = 0;
            textWinLose.text = "";
            listLock.ActivateLocks();
            ActivationSpriteDonKeyKongFree(false);
            ActivationSpriteDonKeyKongWinner(false);
        }


        if (!controllerGame.Playing && stoped && !readyReInit)
            textWinLose.text = "Press Start";

        if (controllerGame.Playing && playerData.Life <=0)
            textWinLose.text = "You Lose";

        if (controllerGame.Playing && !spritePlayer.enabled)
            ActivationSpritePlayer(true);

        if (controllerGame.Playing)
        {
            if (actualPlayerLife != playerData.Life)
            {
                CreateUIlife();
            }
            actualPlayerLife = playerData.Life;
        }

        if (controllerGame.Playing && stoped)
            stoped = false;

        if (controllerGame.Loser)
        {
            textWinLose.text = "You Lose";
            stoped = true;
            readyReInit = true;
            ActivationSpritePlayer(false);
            ActivationSpriteKey(false);
        }

        if (!stoped)
        {
            try
            {
                textPoints.text = playerData.Points.ToString();

                if (playerData.Life > 0)
                    textWinLose.text = "";


                if (controllerGame.Winner)
                {
                    textWinLose.text = "You Win";
                    readyReInit = true;
                    ActivationSpritePlayer(false);
                    ActivationSpriteKey(false);
                    ActivationSpriteDonKeyKong(false);
                    StartCoroutine(UIShowWinner());
                    stoped = true;
                }

                //Logica para desactivar candados en la UI
                foreach (KeyValuePair<int, bool> pair in keyOperator.DictionaryKey)
                {
                    int key = pair.Key;
                    bool value = pair.Value;
                    if(value)
                        listLock.DeactiveLock(key);
                }
            }
            catch
            {
                if(!controllerGame)
                    Debug.LogError("El componente ControllerGame no se encuentra en el juego");

                if (!textPoints)
                    Debug.LogError("El componente de Texto para mostrar el puntaje no se encuentra en el juego");
            }
        }

        if (controllerGame.Playing)
        {
            if(buttonPlay.activeSelf)
                buttonPlay.SetActive(false);
        }
        else
        {
            if (!buttonPlay.activeSelf)
                buttonPlay.SetActive(true);
        }
    }


    private void ActivationSpritePlayer(bool state)
    {
        spritePlayer.enabled = state;
    }

    private void ActivationSpriteKey(bool state)
    {
        spriteKey.enabled = state;
    }

    private void ActivationSpriteDonKeyKong(bool state)
    {
        spriteDonKeyKong.enabled = state;
    }

    private void CreateUIlife()
    {
        foreach (GameObject go in listGameObjectsPrefab)
        {
            DestroyImmediate(go);
        }
        listGameObjectsPrefab.Clear();
        for (int a = 1; a < playerData.Life; a++)
        {
            Vector3 newPosition = new Vector3(content.position.x + a * stepPositionPrefab, content.position.y, content.position.z);
            GameObject gaob = Instantiate(prefabSpritelife, newPosition, Quaternion.identity, content) as GameObject;
            listGameObjectsPrefab.Add(gaob);
        }
    }

    private void ActivationSpriteDonKeyKongFree(bool state)
    {
        spriteDonKeyKongfree.enabled = state;
    }
    private void ActivationSpriteDonKeyKongWinner(bool state)
    {
        spriteDonKeyKongwinner.enabled = state;
    }
    IEnumerator UIShowWinner()
    {
        ActivationSpriteDonKeyKongFree(true);
        yield return new WaitForSeconds(1f);
        ActivationSpriteDonKeyKongFree(false);
        ActivationSpriteDonKeyKongWinner(true);
    }
}

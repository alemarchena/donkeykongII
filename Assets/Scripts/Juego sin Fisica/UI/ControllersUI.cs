using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllersUI : MonoBehaviour
{

    ControllerGame controllerGame;

    [Tooltip("Es el objeto de tipo Text ubicado en el canvas del juego para mostrar los puntos")]
    [SerializeField] Text textPoints;
    [Tooltip("Es el objeto de tipo Text ubicado en el canvas del juego para mostrar Gaanr o Perder")]
    [SerializeField] Text textWinLose;
    [Space]
    [Tooltip("Es el sprite que muestra la cantidad de vidas en el juego")]
    [SerializeField] GameObject prefabSpritelife;
    [SerializeField] SpriteRenderer spritePlayer;
    [SerializeField] GameObject buttonPlay;
    private bool stoped;


    [Tooltip("Es el contenedor que tendra como hijo al sprite que muestra la cantidad de vidas en el juego")]
    [SerializeField] Transform content;
    private int actualPlayerLife;

    private float stepPositionPrefab = 0.5f;
    private List<GameObject> listGameObjectsPrefab = new();
    private bool readyReInit;


    private void Awake()
    {
        stoped = true;
        readyReInit = false;
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
            stoped = false;
            readyReInit = false;
            actualPlayerLife = 0;
            textWinLose.text = "";
        }


        if (!controllerGame.Playing && stoped)
            textWinLose.text = "Press Start";


        if(controllerGame.Playing && !spritePlayer.enabled)
            ActivationSpritePlayer(true);

        if (actualPlayerLife != controllerGame.PlayerLife)
        {
            CreateUIlife();
        }
        actualPlayerLife = controllerGame.PlayerLife;

        if (controllerGame.Playing && stoped)
            stoped = false;


        if (!stoped)
        {
            try
            {
                textPoints.text = controllerGame.PlayerPoints.ToString();


                if (controllerGame.PlayerItsAlive && controllerGame.PlayerLife > 0)
                    textWinLose.text = "";

                if (!controllerGame.PlayerItsAlive && controllerGame.PlayerLife <= 0)
                {
                    textWinLose.text = "You Lose";
                    stoped = true;
                    readyReInit = true;
                    ActivationSpritePlayer(false);
                }

                if (controllerGame.Winner)
                {
                    textWinLose.text = "You Win";
                    readyReInit = true;
                    ActivationSpritePlayer(false);
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


    private void CreateUIlife()
    {
        foreach (GameObject go in listGameObjectsPrefab)
        {
            DestroyImmediate(go);
        }
        listGameObjectsPrefab.Clear();
        for (int a = 1; a < controllerGame.PlayerLife; a++)
        {
            Vector3 newPosition = new Vector3(content.position.x + a * stepPositionPrefab, content.position.y, content.position.z);
            GameObject gaob = Instantiate(prefabSpritelife, newPosition, Quaternion.identity, content) as GameObject;
            listGameObjectsPrefab.Add(gaob);
        }
    }


}

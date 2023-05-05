using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ControllerUI : MonoBehaviour
{
    ControllerGame controllerGame;
    ControllerKeyPoint controllerKeyPoint;

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

    private float stepPositionPrefab = 0.5f;
    private List<GameObject> listGameObjectsPrefab = new();

    private KeyOperator keyOperator;
    private Lock listLock;

    private void Awake()
    {
        if (!playerData) Debug.LogError("Falta asociar el Player a èste objeto");

        controllerKeyPoint = FindObjectOfType<ControllerKeyPoint>();
        if (!controllerKeyPoint) Debug.LogError("Falta la clase ControllerKeyPoint en el juego");


        listLock = FindObjectOfType<Lock>();
        if (!listLock) Debug.LogError("Falta asociar la clase Lock a èste objeto");

        keyOperator = FindObjectOfType<KeyOperator>();
        if (!keyOperator) Debug.LogError("Falta el componente KeyInformant");

        if (!prefabSpritelife) Debug.LogError("Falta el componente Sprite para las vidas del player");
        if (!spritePlayer) Debug.LogError("Falta el componente Sprite del player");
        if (!spriteKey) Debug.LogError("Falta el componente Sprite de la llave");
        if (!spriteDonKeyKong) Debug.LogError("Falta el componente Sprite de la DonKeyKong");
        
        stoped = true;
        ActivationSpritePlayer(false);
        ActivationSpriteDonKeyKongFree(false);
        ActivationSpriteDonKeyKongWinner(false);

    }
    void Start()
    {
        controllerGame= FindObjectOfType<ControllerGame>();
        if (!controllerGame)Debug.LogError("El componente ControllerGame no se encuentra en el juego");
        if (!textPoints)Debug.LogError("El componente de Texto para mostrar el puntaje no se encuentra en el juego");
    }

    public void ReInit()
    {
        stoped = false;
        textWinLose.text = "";
        

        CreateUIlife();
        StartCoroutine(WaitWinner());
        StartCoroutine(WaitLoser());
        StartCoroutine(WaitPlaying());
        StartCoroutine(DelayActivaLocks());
        StartCoroutine(DelayDisplayCharacters());
        StartCoroutine(DelayDeactivateDisplayFreeDonKeyKong());
    }

    IEnumerator DelayActivaLocks()
    {
        yield return new WaitForSeconds(3f);
        listLock.ActivateLocks();
    }

    IEnumerator DelayDisplayCharacters()
    {
        yield return new WaitForSeconds(1.2f);
        ActivationSpritePlayer(true);
        ActivationSpriteKey(true);
        ActivationSpriteDonKeyKong(true);
    }

    IEnumerator DelayDeactivateDisplayFreeDonKeyKong()
    {
        yield return new WaitForSeconds(3.5f);
        ActivationSpriteDonKeyKongFree(false);
        ActivationSpriteDonKeyKongWinner(false);
    }
   
    void Update()
    {
        if (!controllerGame.Playing && stoped && !controllerGame.ReStart)
            textWinLose.text = "Press Start";


        
        if (controllerGame.Playing && stoped)
            stoped = false;

        textPoints.text = playerData.Points.ToString();

    }

    IEnumerator WaitPlaying()
    {

        yield return new WaitUntil(HasPlaying);
        if (playerData.Life > 0) textWinLose.text = "";
        if (!spritePlayer.enabled)ActivationSpritePlayer(true);
        if (buttonPlay.activeSelf) buttonPlay.SetActive(false);
        

    }

    private bool HasPlaying()
    {
        return controllerGame.Playing;
    }

    IEnumerator WaitLoser()
    {
        yield return new WaitUntil(HasLoser);
        buttonPlay.SetActive(true);
        textWinLose.text = "You Lose";
        ActivationSpritePlayer(false);
        ActivationSpriteKey(false);
    }
    private bool HasLoser()
    {
        return controllerGame.Loser;
    }

    IEnumerator WaitWinner()
    {
        yield return new WaitUntil(HasWinner);
        if (controllerGame.Winner)
        {
            textWinLose.text = "You Win";
            ActivationSpritePlayer(false);
            ActivationSpriteKey(false);
            ActivationSpriteDonKeyKong(false);
            StartCoroutine(UIShowWinner(true));
            stoped = true;
            StartCoroutine(UIShowWinner(false));
            StartCoroutine(WaitWinner());
        }
    }
    private bool HasWinner()
    {
        return controllerGame.Winner;
    }

    public void CheckLock()
    {
        //Logica para desactivar candados en la UI
        foreach (KeyValuePair<int, bool> pair in keyOperator.DictionaryKey)
        {
            int key = pair.Key;
            bool value = pair.Value;
            if (value)
                listLock.DeactiveLock(key);
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

    public void CreateUIlife()
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
    IEnumerator UIShowWinner(bool state)
    {
        if(state)
        {
            ActivationSpriteDonKeyKongFree(true);
            yield return new WaitForSeconds(1.5f);
            ActivationSpriteDonKeyKongFree(false);
            ActivationSpriteDonKeyKongWinner(true);
        }
        else
        {
            yield return new WaitForSeconds(3f);
            ActivationSpriteDonKeyKongFree(false);
            ActivationSpriteDonKeyKongWinner(false);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAddPointOrDead : MonoBehaviour 
{
    enum TypeAction { AddPoint,DiscountLife }
    TypeAction typeAction;

    private  List<CharacterObject> listCharacters =new List<CharacterObject>();
    [SerializeField] PlayerMovementMap playerMovementMap;
    bool canReact = true;
    bool canAddPoint = true;
    bool canDiscountLife = true;
    
    /// <summary>
    /// Agrega un personaje a la lista cuando es requerido por cada informante
    /// </summary>
    /// <param name="objectRecept"></param>
    /// <param name="counterX"></param>
    /// <param name="counterY"></param>
    public void AddGameObject(GameObject objectRecept,int counterX,int counterY)
    {
        CharacterObject characterObject = 
            new CharacterObject(objectRecept, objectRecept.name,counterX,counterY, objectRecept.GetInstanceID());

        characterObject.type = DetectTypeObject(objectRecept, characterObject);

        listCharacters.Add(characterObject);

    }

    /// <summary>
    /// Clasifica el tipo de personaje cuando se crea en la lista de personajes
    /// </summary>
    /// <param name="characterObject"></param>
    /// <param name="co"></param>
    /// <returns></returns>
    private CharacterObject.Type DetectTypeObject(GameObject characterObject, CharacterObject co) {

        co = new CharacterObject();
        co.type = CharacterObject.Type.None;

        if (characterObject.GetComponent<Enemy>())
        {
            co.type = CharacterObject.Type.Enemy;
        }
        if (characterObject.GetComponent<Player>())
        {
            co.type = CharacterObject.Type.Player;
        }

        return co.type;
    }

    /// <summary>
    /// Recepciona una notificación de movimiento de algun personaje
    /// </summary>
    /// <param name="characterObject"></param>
    /// <param name="counterX"></param>
    /// <param name="counterY"></param>
    public void NewPosition(GameObject characterObject, int counterX, int counterY)
    {
        UpdatePositionInCharacterList(characterObject, counterX ,counterY );
    }

    /// <summary>
    /// Por cada detección de movimientos de algun personaje actualiza la posicion en la lista de personajes
    /// </summary>
    /// <param name="characterObject"></param>
    /// <param name="counterX"></param>
    /// <param name="counterY"></param>
    private void UpdatePositionInCharacterList(GameObject characterObject, int counterX, int counterY)
    {
        for (int a = 0; a < listCharacters.Count; a++)
        {
            int id = characterObject.GetInstanceID();

            if (id == listCharacters[a].objectId)
            {
                listCharacters[a].posX = counterX;
                listCharacters[a].posY = counterY;
                StartCoroutine(RetardInform());
                
                return;
            }
        }
        
    }

    IEnumerator RetardInform()
    {
        yield return new WaitForSeconds(0f);
        VerifiyCollision();

    }

    /// <summary>
    /// Verifica las posiciones entre los personajes del juego para determinar las posiciones de muerte o ganar puntos al player
    /// </summary>
    private void VerifiyCollision()
    {
        CharacterObject objetoPlayer;
        CharacterObject objetoEnemy;

        for (int a=0;a<listCharacters.Count ;a++)
        {
            for (int b = a+1; b < listCharacters.Count; b++)
            {
                if (listCharacters[a].objectId != listCharacters[b].objectId && 
                    listCharacters[a].posX == listCharacters[b].posX)
                {

                    if (listCharacters[a].characterObject.gameObject.activeSelf == true && listCharacters[b].characterObject.gameObject.activeSelf == true)
                    {

                        if (listCharacters[a].type == CharacterObject.Type.Player)
                        {
                            objetoPlayer = listCharacters[a];
                            objetoEnemy = listCharacters[b];
                        }
                        else
                        {
                            if (listCharacters[b].type == CharacterObject.Type.Player)
                            {
                                objetoPlayer = listCharacters[b];
                                objetoEnemy = listCharacters[a];
                            }
                            else
                            {
                                objetoPlayer = new CharacterObject();
                                objetoEnemy = new CharacterObject();
                            }
                        }


                        if (listCharacters[a].posY == listCharacters[b].posY)
                        {
                            if (listCharacters[a].type == CharacterObject.Type.Player || listCharacters[b].type == CharacterObject.Type.Player)
                            {
                                if(objetoPlayer.objectId != -1)
                                    ActionsInPlayer(objetoPlayer.objectId,TypeAction.DiscountLife);
                                return;
                            }
                        } else
                        {
                            if(objetoPlayer.posY == objetoEnemy.posY + 1)
                            {
                                if (canReact)
                                {
                                    canReact = false;
                                    canAddPoint = true;
                                    //Las posiciones de caer no valen para ganar puntos
                                    foreach (Vector2 vector in playerMovementMap.posNoCaer)
                                    {
                                        if (vector.x == objetoPlayer.posX && vector.y + 1 == objetoPlayer.posY) 
                                            //Es vector.y + 1 ya que la posicion de no caer es al momento de saltar 
                                        {
                                            canAddPoint = false;
                                        }
                                    }
                                    if(canAddPoint)
                                    {
                                        if(QueryIfGivePointEnemy(objetoEnemy.objectId) && objetoPlayer.objectId!=-1)
                                            ActionsInPlayer(objetoPlayer.objectId, TypeAction.AddPoint);
                                    }

                                    canAddPoint = true;
                                    Invoke("ChangeCanReact", 0.5f);
                                }
                                return;
                            }
                        }
                    }

                }

            }
        }
    }

    private void ChangeCanReact()
    {
        canReact = true;
    }

    /// <summary>
    /// Determina si el enemigo que tiene debajo genera puntos por saltarlo
    /// </summary>
    /// <param name="idInstance"></param>
    /// <returns></returns>
    private bool QueryIfGivePointEnemy(int idInstance)
    {
        bool result = false;
        GameObject[] gameObjects = (GameObject[])FindObjectsOfType(typeof(GameObject));
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.GetInstanceID() == idInstance)
            {
                try
                {
                    Enemy e = gameObject.GetComponent<Enemy>();

                    if (e.givesPoints == Enemy.GivesPoints.Yes)
                        result= true;
                }
                catch
                {
                    Debug.LogError("No se encontro el componente player para descontar vida");
                }
                break;
            }
        }
        return result;
    }

    /// <summary>
    /// Las acciones que puede obtener el player son Morir o Ganar puntos
    /// </summary>
    /// <param name="idInstance"></param>
    /// <param name="typeAction"></param>
    private void ActionsInPlayer(int idInstance,TypeAction typeAction)
    {
        GameObject[] gameObjects = (GameObject[]) FindObjectsOfType(typeof(GameObject));
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.GetInstanceID() == idInstance)
            {
                try
                {
                    if(typeAction == TypeAction.DiscountLife && canDiscountLife)
                    {
                        canDiscountLife = false;

                        StartCoroutine(RecoverCanDiscountLife());

                        gameObject.GetComponent<PlayerOperator>().DeadNotification();
                    }
                    else if (typeAction == TypeAction.AddPoint)
                    {
                        gameObject.GetComponent<PlayerOperator>().WinPointNotification();
                    }
                }
                catch
                {
                    Debug.LogError("No se encontro el componente player para descontar vida");
                }
                break;
            }
        }
    }

    IEnumerator RecoverCanDiscountLife()
    {
        yield return new WaitForSeconds(0.5f);
        canDiscountLife = true;
    }
}



public class CharacterObject
{
    public GameObject characterObject;
    public enum Type { Player, Enemy,None}
    public Type type;
    public string name;
    public int posX;
    public int posY;
    public int objectId;

    public CharacterObject() {
        objectId = -1;
    }
    public CharacterObject(GameObject _characterObject,string _name,int _posX,int _posY,int _objectId, Type _type = Type.None) {
        characterObject = _characterObject;
        type = _type;
        name = _name;
        posX = _posX;
        posY = _posY;
        objectId = _objectId;
    }
}
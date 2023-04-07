using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAddPointOrDead : MonoBehaviour 
{
    enum TypeAction { AddPoint,DiscountLife }
    TypeAction typeAction;

    private  List<CharacterObject> listCharacters =new List<CharacterObject>();
    private MovementMap movementMap;
    bool canReact = true;
    bool canAddPoint = true;

    private void Start()
    {
        movementMap = FindObjectOfType<MovementMap>();
    }
    public void AddGameObject(GameObject objectRecept,int counterX,int counterY)
    {
        CharacterObject characterObject = 
            new CharacterObject(objectRecept, objectRecept.name,counterX,counterY, objectRecept.GetInstanceID());

        characterObject.type = DetectTypeObject(objectRecept, characterObject);
        listCharacters.Add(characterObject);
    }

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

    public void NewPosition(GameObject characterObject, int counterX, int counterY)
    {
        ReassignPosition(characterObject, counterX ,counterY );
    }

    private void ReassignPosition(GameObject characterObject, int counterX, int counterY)
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
        yield return new WaitForSeconds(0.5f);
        VerifiyCollision();

    }

    private void VerifiyCollision()
    {
        int j;
        CharacterObject objetoPlayer;
        CharacterObject objetoEnemy;

        for (int a=0;a<listCharacters.Count ;a++)
        {
            for (int b = a+1; b < listCharacters.Count; b++)
            {
                if (listCharacters[a].objectId != listCharacters[b].objectId && 
                    listCharacters[a].posX == listCharacters[b].posX)
                {
                        if (listCharacters[a].type == CharacterObject.Type.Player)
                    {
                        objetoPlayer = listCharacters[a];
                        objetoEnemy = listCharacters[b];
                    }
                    else
                    {
                        objetoPlayer = listCharacters[b];
                        objetoEnemy = listCharacters[a];
                    }


                    if (listCharacters[a].posY == listCharacters[b].posY)
                    {
                        if (listCharacters[a].type == CharacterObject.Type.Player || listCharacters[b].type == CharacterObject.Type.Player )
                        {
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
                                foreach (Vector2 vector in movementMap.posNoCaer)
                                {
                                    if (vector.x == objetoPlayer.posX && vector.y + 1 == objetoPlayer.posY) 
                                        //Es vector.y + 1 ya que la posicion de no caer es al momento de saltar 
                                    {
                                        canAddPoint = false;
                                    }
                                }
                                if(canAddPoint)
                                {
                                    if(QueryIfGivePointEnemy(objetoEnemy.objectId))
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

    private void ChangeCanReact()
    {
        canReact = true;
    }

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
                catch (Exception e)
                {
                    Debug.LogError("No se encontro el componente player para descontar vida");
                }
                break;
            }
        }
        return result;
    }

    private void ActionsInPlayer(int idInstance,TypeAction typeAction)
    {
        GameObject[] gameObjects = (GameObject[]) FindObjectsOfType(typeof(GameObject));
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.GetInstanceID() == idInstance)
            {
                try
                {
                    Player p = gameObject.GetComponent<Player>();
                    
                    if(typeAction == TypeAction.DiscountLife)
                    {
                        if(p.ItsAlive)
                        {
                           gameObject.GetComponent<InformantMovement>().DeadNotification();
                           p.DiscountLife();
                        }

                    }
                    else if (typeAction == TypeAction.AddPoint)
                    {
                        if (p.ItsAlive)
                        {
                            p.AddPoint();
                        }
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

    public CharacterObject() { }
    public CharacterObject(GameObject _characterObject,string _name,int _posX,int _posY,int _objectId, Type _type = Type.None) {
        characterObject = _characterObject;
        type = _type;
        name = _name;
        posX = _posX;
        posY = _posY;
        objectId = _objectId;
    }
}
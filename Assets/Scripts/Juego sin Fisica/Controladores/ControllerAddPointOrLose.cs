using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAddPointOrLose : MonoBehaviour 
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
    public void AddGameObject(GameObject objectRecept,int counterX=0,int counterY=0)
    {
        CharacterObject characterObject = 
            new CharacterObject(objectRecept, objectRecept.name,counterX,counterY, objectRecept.GetInstanceID());

        characterObject.type = DetectTypeObject(objectRecept, characterObject);
        listCharacters.Add(characterObject);
    }

    private CharacterObject.Type DetectTypeObject(GameObject characterObject, CharacterObject co) {

        co = new CharacterObject();

        if (characterObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            co.type = CharacterObject.Type.Enemy;
        }
        if (characterObject.TryGetComponent<Player>(out Player player))
        {
            co.type = CharacterObject.Type.Player;
        }
        else
            co.type = CharacterObject.Type.None;

        return co.type;
    }

    public void NewPosition(GameObject characterObject, int counterX = 0, int counterY = 1)
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
            if(a + 1 < listCharacters.Count )
            {
                if (listCharacters[a].objectId != listCharacters[a + 1].objectId && listCharacters[a].posX == listCharacters[a + 1].posX)
                {
                    if (listCharacters[a].type == CharacterObject.Type.Player)
                    {
                        objetoPlayer = listCharacters[a];
                        objetoEnemy = listCharacters[a + 1];
                    }
                    else
                    {
                        objetoPlayer = listCharacters[a + 1];
                        objetoEnemy = listCharacters[a];
                    }


                    if (listCharacters[a].posY == listCharacters[a + 1].posY)
                    {
                        if (listCharacters[a].type == CharacterObject.Type.Player || listCharacters[a+1].type == CharacterObject.Type.Player )
                        {
                            Action(objetoPlayer.objectId,TypeAction.DiscountLife);
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
                                    if (vector.x == objetoPlayer.posX && vector.y + 1 == objetoPlayer.posY) //vector.y + 1 ya que la posicion de no caer es al momento de saltarwdw 
                                    {
                                        canAddPoint = false;
                                    }
                                }
                                if(canAddPoint)
                                    Action(objetoPlayer.objectId, TypeAction.AddPoint);

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

    private void Action(int idInstance,TypeAction typeAction)
    {
        GameObject[] gameObjects = (GameObject[]) FindObjectsOfType(typeof(GameObject));
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.GetInstanceID() == idInstance)
            {
                try
                {
                    if(typeAction == TypeAction.DiscountLife)
                        gameObject.GetComponent<Player>().DiscountLife();
                    else if (typeAction == TypeAction.AddPoint)
                        gameObject.GetComponent<Player>().AddPoint();
                }
                catch (Exception e)
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
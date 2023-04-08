using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// La clase controla la logica entre la posicion de la llave y el player.
/// </summary>
public class ControllerMovementKey : MonoBehaviour
{
    private int positionKeyX;
    private int positionKeyY;
    private int positionPlayerX;
    private int positionPlayerY;
    
    private int idKey;

    public void AddKey(GameObject keyObject)
    {
        idKey = keyObject.GetInstanceID();
    }

    public void NewPositionPlayer(GameObject playerObject, int counterX, int counterY)
    {
        positionPlayerX = counterX;
        positionPlayerY = counterY;
        VerifiyCollision();
    }

    public void NewPositionKey(GameObject keyObject, int counterX, int counterY)
    {
        positionKeyX = counterX;
        positionKeyY = counterY;
        VerifiyCollision();
    }

    private void VerifiyCollision()
    {
         if (positionKeyX == positionPlayerX && positionKeyY == positionPlayerY)
            MoveKey(idKey);
    }

    private void MoveKey(int idInstance)
    {
        GameObject[] gameObjects = (GameObject[])FindObjectsOfType(typeof(GameObject));
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.GetInstanceID() == idInstance)
            {
                try
                {
                   Key k = gameObject.GetComponent<Key>();
                   k.SetNewPositionKey();
                }
                catch
                {
                    Debug.LogError("No se encontro el componente key para ser movido");
                }
                break;
            }
        }
    }
}

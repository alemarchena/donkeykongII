using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// La clase controla la logica entre la posicion de la llave y el player.
/// </summary>
public class ControllerCollisionPlayerKey : MonoBehaviour
{
    private int positionKeyX;
    private int positionKeyY;
    private int positionPlayerX;
    private int positionPlayerY;
    
    private Key Key;
    private bool okNotify=true;
    public void AddKey(Key KeyObject)
    {
        Key = KeyObject;
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
        if (okNotify && ( positionKeyX == positionPlayerX && positionKeyY == positionPlayerY) )
        {
            okNotify = false;
            Key.SetNewPositionKey();
            StartCoroutine(RestartNotify());
        }
    }

    IEnumerator RestartNotify()
    {
        yield return new WaitForSeconds(0.2f);
        okNotify = true;
    }
}

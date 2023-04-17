using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// El informante reporta la posicion de la llave y el player al ControllerMovementKey
/// </summary>
public class InformantKey : MonoBehaviour
{
    ControllerCollisionPlayerKey cmk;
    Key key;

    [SerializeField] private int actualCounterX;
    [SerializeField] private int actualCounterY;
    private void Awake()
    {
        try
        {
            cmk = FindObjectOfType<ControllerCollisionPlayerKey>();
            key = GetComponent<Key>();
        }
        catch
        {
            if (!cmk) Debug.LogError("Falta la clase ControllerMovementKey en el juego");
            if (!key) Debug.LogError("Falta la clase Key en el objeto que contiene el InformantKey");
        }
    }

    private void Start()
    {
        cmk.AddKey(key);
        GetPositionKey();
    }

    private void GetPositionKey()
    {
        actualCounterX = key.CounterX;
        actualCounterY = key.CounterY;
        cmk.NewPositionKey(this.gameObject, actualCounterX, actualCounterY);
    }
    void Update()
    {
        if ((key.CounterX != actualCounterX || key.CounterY != actualCounterY ) && key.LlavesCapturadas < key.LlavesTotales )
        {
            actualCounterX = key.CounterX;
            actualCounterY = key.CounterY;

            cmk.NewPositionKey(this.gameObject, actualCounterX, actualCounterY);
        }
    }

   
}

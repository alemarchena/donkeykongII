using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// El informante reporta la posicion de la llave y el player al ControllerMovementKey
/// </summary>
public class KeyInformant : MonoBehaviour
{
    ControllerCollisionPlayerKey cmk;
    Key key;
    public bool KeyComplete { get; private set;}
    public bool Stoped { get; private set; }

    [SerializeField] private int actualCounterX;
    [SerializeField] private int actualCounterY;
    private void Awake()
    {
        try
        {
            cmk = FindObjectOfType<ControllerCollisionPlayerKey>();
            key = GetComponent<Key>();
            ReInit();

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

    public void ReInit()
    {
        KeyComplete = false;
        Stoped = false;
        key.ReInit();
    }

    private void GetPositionKey()
    {
        actualCounterX = key.CounterX;
        actualCounterY = key.CounterY;
        cmk.NewPositionKey(this.gameObject, actualCounterX, actualCounterY);
    }
    void Update()
    {
        if(!Stoped)
        {
            if ((key.CounterX != actualCounterX || key.CounterY != actualCounterY ) && key.LlavesCapturadas < key.LlavesTotales )
            {
                actualCounterX = key.CounterX;
                actualCounterY = key.CounterY;

                cmk.NewPositionKey(this.gameObject, actualCounterX, actualCounterY);
            }

            if(key.LlavesCapturadas >= key.LlavesTotales)
            {
                KeyComplete = true;
                Stoped = true;
            }
        }
    }
}

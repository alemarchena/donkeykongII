using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// El informante reporta la posicion de la llave y el player al ControllerMovementKey
/// </summary>
public class InformantKey : MonoBehaviour
{
    ControllerMovementKey cmk;
    Key key;

    [SerializeField] private int actualCounterX;
    [SerializeField] private int actualCounterY;

    private void Awake()
    {
        try
        {
            cmk = FindObjectOfType<ControllerMovementKey>();
            key = FindObjectOfType<Key>();
        }
        catch
        {
            if (!cmk) Debug.LogError("Falta la clase ControllerMovementKey en el juego");
            if (!key) Debug.LogError("Falta la clase Key en el juego");
        }
    }

    private void Start()
    {
        cmk.AddKey(this.gameObject);
    }

    void Update()
    {
        if (key.CounterX != actualCounterX || key.CounterY != actualCounterY)
        {
            actualCounterX = key.CounterX;
            actualCounterY = key.CounterY;

            cmk.NewPositionKey(this.gameObject, actualCounterX, actualCounterY);
        }
    }
}

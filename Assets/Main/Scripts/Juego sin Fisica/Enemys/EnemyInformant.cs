using System;
using UnityEngine;

/// <summary>
/// La clase informant comunica al ControllerWinLose los contadores de movimiento en X e Y del objeto
/// </summary>

public class EnemyInformant : MonoBehaviour
{
    private EnemyMovementAutomaticWithCounter eMAWC;
    ControllerAddPointOrDead cAddDead;

    private int actualCounterX;
    private int actualCounterY;

    private EnemyInformant()
    {

    }

    private void Awake()
    {
        cAddDead = FindObjectOfType<ControllerAddPointOrDead>();
    }
  
    private void Start()
    {
        try
        {
            eMAWC = GetComponent<EnemyMovementAutomaticWithCounter>();
            cAddDead.AddGameObject(this.gameObject, eMAWC.CounterX, eMAWC.CounterY);
        }catch
        {
            if(!cAddDead)Debug.LogError("El objeto necesita contener la clase ControllerAddPointOrDead para utilizar el informante");

            if (!eMAWC) Debug.LogError("El objeto necesita contener la clase EnemyMovementAutomaticWithCounter para utilizar el informante");
        }
    }

    private void Update()
    {
        if (eMAWC.CounterX != actualCounterX || eMAWC.CounterY != actualCounterY)
        {
            actualCounterX = eMAWC.CounterX;
            actualCounterY = eMAWC.CounterY;
            cAddDead.NewPosition(this.gameObject, eMAWC.CounterX,eMAWC.CounterY);
        }
    }
}

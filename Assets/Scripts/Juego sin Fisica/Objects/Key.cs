using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class Key : MonoBehaviour
{
    [SerializeField] private SecuenceKey secuenceKey;

    public int CounterX { get; private set; }
    public int CounterY { get; private set; }
    public int LlavesCapturadas { get; private set; }
    public int LlavesTotales { get; private set; }

    private int nextPosicionKey;
    private int temporalPosition = 1;
    private bool EstaEnCandado = false;

    private Lock listLock;

    private void Start()
    {
        listLock = FindObjectOfType<Lock>();
        ReInit();
        secuenceKey.GeneratePositionsKey();
    }
    private void ReInit()
    {
        try
        {
            temporalPosition = 1;
            nextPosicionKey = 0;
            LlavesCapturadas = 0;
            LlavesTotales = secuenceKey.ListCounterPosition.Count - 2;
            ResetVectorOriginalPosition();
            secuenceKey.GeneratePositionsKey();

            listLock.ActivateLocks();
        }
        catch (Exception e)
        {
            Debug.LogError($"Error {e}");
        }
    }
  

    public void SetNewPositionKey()
    {
        nextPosicionKey += 1;

        if (nextPosicionKey < 2)
        {
            transform.position = secuenceKey.UIListTransformKey[secuenceKey.ListIndexPositionKey[nextPosicionKey]];

            Vector2 vector = secuenceKey.ListCounterPosition[secuenceKey.ListIndexPositionKey[nextPosicionKey]];
            CounterX = (int)vector.x;
            CounterY = (int)vector.y;
            EstaEnCandado = false;

        }
        else
        {
            if(!EstaEnCandado)
            {

                temporalPosition += 1;

                transform.position = secuenceKey.UIListTransformKey[secuenceKey.ListIndexPositionKey[temporalPosition]];

                Vector2 vector = secuenceKey.ListCounterPosition[secuenceKey.ListIndexPositionKey[temporalPosition]];
                CounterX = (int)vector.x;
                CounterY = (int)vector.y;
                EstaEnCandado = true;

            }
            else
            {
                int index = 0;
                switch (secuenceKey.ListIndexPositionKey[temporalPosition])
                {
                    case 2:index = 0;break;
                    case 3:index = 1;break;
                    case 4:index = 2;break;
                    case 5:index = 3;break;
                }

                listLock.DeactiveLock(index);

                LlavesCapturadas += 1;
                if(LlavesCapturadas >= LlavesTotales)
                {
                    ReInit();
                    return;
                }
                ResetVectorOriginalPosition();
            }
        }

    }

    public void ResetVectorOriginalPosition()
    {
        nextPosicionKey = 0;

        Vector2 vector = secuenceKey.ListCounterPosition[0];
        CounterX = (int)vector.x;
        CounterY = (int)vector.y;

        transform.position = secuenceKey.UIListTransformKey[0];
    }
}
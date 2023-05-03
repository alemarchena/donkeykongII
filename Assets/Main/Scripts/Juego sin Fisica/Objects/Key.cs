using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class Key : MonoBehaviour
{

    [SerializeField] private SecuenceKey secuenceKey;
    private ControllerSound controllerSound;
    private ControllerKeyPoint controllerKeyPoint;
    private ControllerUI controllerUI;

    public int CounterX { get; private set; }
    public int CounterY { get; private set; }
    public int LlavesCapturadas { get; private set; }
    public int LlavesTotales { get; private set; }
    public bool Stoped { get; private set; }

    private int nextPosicionKey;
    private int temporalPosition = 1;
    private bool EstaEnCandado = false;


    private Dictionary<int, bool> dictionaryFreeKey = new();

    public Dictionary<int, bool>  DictionaryFreeKey
    {
        get { return dictionaryFreeKey; }
    }

    private void Awake()
    {
        secuenceKey.GeneratePositionsKey();

        controllerSound = FindObjectOfType<ControllerSound>();
        controllerKeyPoint = FindObjectOfType<ControllerKeyPoint>();
        controllerUI  = FindObjectOfType<ControllerUI>();

        if (!controllerSound) Debug.LogError("Falta el ControllerSound en el juego");
        if (!controllerKeyPoint) Debug.LogError("Falta el ControllerKeyPoint en el juego");
        if (!controllerUI) Debug.LogError("Falta el ControllerUI en el juego");


    }

    public void ReInit()
    {
        try
        {
            temporalPosition = 1;
            nextPosicionKey = 0;
            LlavesCapturadas = 0;
            LlavesTotales = secuenceKey.ListCounterPosition.Count - 2;
            ResetVectorOriginalPosition();
            secuenceKey.GeneratePositionsKey();

            dictionaryFreeKey.Clear();
            dictionaryFreeKey.Add(0, false);
            dictionaryFreeKey.Add(1, false);
            dictionaryFreeKey.Add(2, false);
            dictionaryFreeKey.Add(3, false);

            Stoped = false;
        }
        catch (Exception e)
        {
            Debug.LogError($"Error {e}");
        }
    }
  

    public void SetNewPositionKey()
    {
        if (!Stoped)
        {
            nextPosicionKey += 1;

            if (nextPosicionKey < 2)
            {
                transform.position = secuenceKey.UIListTransformKey[secuenceKey.ListIndexPositionKey[nextPosicionKey]];

                Vector2 vector = secuenceKey.ListCounterPosition[secuenceKey.ListIndexPositionKey[nextPosicionKey]];
                CounterX = (int)vector.x;
                CounterY = (int)vector.y;
                EstaEnCandado = false;
                controllerKeyPoint.CapturedKey();
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
                    controllerKeyPoint.CapturedKey();
                }
                else
                {
                    switch (secuenceKey.ListIndexPositionKey[temporalPosition])
                    {
                        case 2:dictionaryFreeKey[0] = true;break;
                        case 3:dictionaryFreeKey[1] = true;break;
                        case 4:dictionaryFreeKey[2] = true;break;
                        case 5:dictionaryFreeKey[3] = true;break;
                    }


                    LlavesCapturadas += 1;
                    controllerKeyPoint.OpenLock();
                    controllerSound.PlayCapturedKey();
                    controllerUI.CheckLock();

                    if (LlavesCapturadas >= LlavesTotales)
                    {
                        Stoped = true;
                        return;
                    }else
                    {
                    }
                    ResetVectorOriginalPosition();
                }
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
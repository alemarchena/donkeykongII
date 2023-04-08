using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Key : MonoBehaviour
{

    [SerializeField] private SecuenceKey secuenceKey;
    private List<Vector2> listPositionInUI = new List<Vector2>();
    private int counterX;
    private int counterY;
    /// <summary>
    /// Lista de posiciones Transform.position de la secuencia de la llave
    /// </summary>
    public List<Vector2> ListPositionInUI
    {
        get { return listPositionInUI; }
    }

    public int CounterX {
        get { return counterX; }
    }
    public int CounterY {
        get { return counterY; }
    }

    private Vector2 vectorOriginalPosition;
 
    private void Start()
    {
        ResetVectorOriginalPosition();
        secuenceKey.AddPositions(out listPositionInUI);
    }

    public void ResetVectorOriginalPosition()
    {
        secuenceKey.ResetPositionInSecuence();
        transform.position = vectorOriginalPosition;

        if(listPositionInUI.Count>0)
            vectorOriginalPosition = listPositionInUI[0];

    }

    public void SetNewPositionKey()
    {
        Vector2 vector = secuenceKey.IncrementPosition();
        counterX = (int) vector.x;
        counterY = (int) vector.y;
    }

}




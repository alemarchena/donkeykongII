using System.Collections.Generic;
using UnityEngine;


public class Key : MonoBehaviour
{

    [SerializeField] private SecuenceKey secuenceKey;
    private List<Vector2> listPositionInUI = new List<Vector2>();
    private int counterX;
    private int counterY;
    ///// <summary>
    ///// Lista de posiciones Transform.position de la secuencia de la llave
    ///// </summary>
    //public List<Vector2> ListPositionInUI
    //{
    //    get { return listPositionInUI; }
    //}

    public int CounterX {
        get { return counterX; }
    }
    public int CounterY {
        get { return counterY; }
    }

    private Vector2 vectorOriginalPosition;

    private void Awake()
    {
        secuenceKey.AddPositions(out listPositionInUI);
        ResetVectorOriginalPosition();
    }
 

    private void UpdatePositionCounter()
    {
        Vector2 vector = secuenceKey.SecuencePositionKey[secuenceKey.ActualPosition];
        counterX = (int) vector.x;
        counterY = (int) vector.y;

    }
    public void ResetVectorOriginalPosition()
    {
        secuenceKey.ResetPositionInSecuence();

        if (listPositionInUI.Count > 0)
        {
            vectorOriginalPosition = listPositionInUI[0];
            transform.position = vectorOriginalPosition;
        }

        UpdatePositionCounter();
    }

    public void SetNewPositionKey()
    {
        Vector2 vector = secuenceKey.IncrementPosition();
        counterX = (int)vector.x;
        counterY = (int)vector.y;
        transform.position = new Vector2(listPositionInUI[secuenceKey.ActualPosition].x, listPositionInUI[secuenceKey.ActualPosition].y);

        if (secuenceKey.ActualPosition > 2)
        {
            listPositionInUI.RemoveAt(secuenceKey.ActualPosition-1); //elimina de la lista la posicion usada de la UI
            secuenceKey.RemovePosition(vector);
            ResetVectorOriginalPosition();
        }
        
        
    }

}




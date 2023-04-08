
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SecuenceKey", menuName = "Items/SecuenceKey")]
public class SecuenceKey : ScriptableObject
{
    private int actualPositionInSecuence =0;
    [SerializeField] private List<Vector2> secuencePositionKey;
    [SerializeField] private List<Vector2> UiSecuencePositionKey;

    public int ActualPosition
    {
        get { return actualPositionInSecuence ; }
    }

    public List<Vector2> SecuencePositionKey
    {
        get { return secuencePositionKey; }
    }

    [Space]
    [Tooltip("Posiciones de las llaves según los contadores de X e Y establecidos en el análisis")]
    [SerializeField] private List<Vector2> listStablePositionKey = new List<Vector2>();
    [Tooltip("Posiciones de Transform.position de las llaves en X e Y en la UI")]
    [SerializeField] private List<Vector2> UiListStablePositionKey = new List<Vector2>();

    [Space]
    [Tooltip("Posiciones aleatorias de las llaves según los contadores de X e Y establecidos en el análisis")]
    [SerializeField] private List<Vector2> listAleatoryPositionKey = new List<Vector2>();
    [Tooltip("Posiciones de Transform.position de las llaves en X e Y en la UI")]
    [SerializeField] private List<Vector2> UiListAleatoryPositionKey = new List<Vector2>();


    private List<Vector2> listTemporalPosition = new List<Vector2>();
    private List<Vector2> uiListTemporalPosition = new List<Vector2>();

    /// <summary>
    /// Incrementa la posición de la llave en la secuencia. Devuelve la nueva posición en la secuencia.
    /// </summary>
    /// <returns></returns>
    public Vector2 IncrementPosition()
    {
        Vector2 newPos=Vector2.zero;

        actualPositionInSecuence += 1;

        if (actualPositionInSecuence > secuencePositionKey.Count-1)
            actualPositionInSecuence = 0;

        if (actualPositionInSecuence < 2)//Las dos primeras posiciones de la llave son estables
        {
            newPos= secuencePositionKey[actualPositionInSecuence];
        }
        else{
            for (int a = 0; a < secuencePositionKey.Count; a++)
            {
                if (a == actualPositionInSecuence)
                {
                    newPos = secuencePositionKey[a];
                }
            }
        }

        return newPos;
    }

    public void RemovePosition(Vector2 vector)
    {
        for (int a = 0; a < secuencePositionKey.Count; a++)
        {
            if (secuencePositionKey[a].x == vector.x && secuencePositionKey[a].y == vector.y)
            {
                secuencePositionKey.RemoveAt(a-1);      //elimina de la lista la posicion usada del contador
                UiSecuencePositionKey.RemoveAt(a - 1);  //elimina de la lista la posicion usada de la UI
            }
        }
    }

    public void ResetPositionInSecuence()
    {
        actualPositionInSecuence = 0;

    }

    public void AddPositions(out List<Vector2> listPositionInUI)
    {
        listPositionInUI = new List<Vector2>();

        if(listStablePositionKey.Count != UiListStablePositionKey.Count)
        {
            Debug.LogError("Las listas de contadores de posiciones estables y posiciones UI estables deben tener la misma cantidad");
            return;
        }

        if (listAleatoryPositionKey.Count != UiListAleatoryPositionKey.Count)
        {
            Debug.LogError("Las listas aleatorias de contadores de posiciones estables y posiciones aleatorias UI estables deben tener la misma cantidad");
            return;
        }
        //-----------------------------------------------------------------------------

        secuencePositionKey.Clear();
        UiSecuencePositionKey.Clear();
        listTemporalPosition.Clear();
        uiListTemporalPosition.Clear();

        for (int a = 0; a< listStablePositionKey.Count; a++)
        {
            secuencePositionKey.Add(listStablePositionKey[a]);
            UiSecuencePositionKey.Add(UiListStablePositionKey[a]);
            listPositionInUI.Add(UiListStablePositionKey[a]);
        }


        //Lista temporal para armar la lista aleatoria
        for (int t = 0; t < listAleatoryPositionKey.Count; t++)
        {
            listTemporalPosition.Add(listAleatoryPositionKey[t]);
            uiListTemporalPosition.Add(UiListAleatoryPositionKey[t]);
        }

        
        while (listTemporalPosition.Count > 0)
        {
            int randomPosition = UnityEngine.Random.Range(0, listTemporalPosition.Count);

            for (int b = 0; b < listTemporalPosition.Count; b++)
            {
                if (b == randomPosition)
                {
                    secuencePositionKey.Add(listTemporalPosition[b]);
                    UiSecuencePositionKey.Add(uiListTemporalPosition[b]);
                    listPositionInUI.Add(uiListTemporalPosition[b]);

                    listTemporalPosition.RemoveAt(b);
                    uiListTemporalPosition.RemoveAt(b);
                }
            }
        }
    }
}

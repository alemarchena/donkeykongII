
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SecuenceKey", menuName = "Items/SecuenceKey")]
public class SecuenceKey : ScriptableObject
{
    public List<Vector2> ListCounterPosition{get { return listCounterPositionKey; }}
    [SerializeField] private List<Vector2> listCounterPositionKey = new();
    public List<Vector2> UIListTransformKey{get { return UiListTrasformPositionKey; }}
    [SerializeField] private List<Vector2> UiListTrasformPositionKey = new();
    public List<int> ListIndexPositionKey { get { return listIndexPositionKey; } }
    [SerializeField] private List<int> listIndexPositionKey = new();


    
    public void GeneratePositionsKey()
    {
        if(listCounterPositionKey.Count != UiListTrasformPositionKey.Count)
        {
            Debug.LogError("Las listas de contadores de posiciones estables y posiciones UI estables deben tener la misma cantidad");
            return;
        }

        List<int> listTemporalPosition = new();
        listTemporalPosition.Clear();
        for (int s = 0; s < listCounterPositionKey.Count; s++){
            listTemporalPosition.Add(s);
        }
        UnOrderList(listTemporalPosition);
    }

    private void UnOrderList(List<int> listTemporalPosition)
    {
        System.Random random = new System.Random();
        for (int i = 2; i < listTemporalPosition.Count; i++)
        {
            int temp = listTemporalPosition[i];
            int randomIndex = random.Next(i, listTemporalPosition.Count);
            listTemporalPosition[i] = listTemporalPosition[randomIndex];
            listTemporalPosition[randomIndex] = temp;
        }

        listIndexPositionKey.Clear();
        foreach (int element in listTemporalPosition) {
            listIndexPositionKey.Add(element);
        }
    }
}

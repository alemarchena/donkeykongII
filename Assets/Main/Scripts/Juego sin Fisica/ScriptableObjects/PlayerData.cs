
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData", menuName ="Items/Player Data",order = 1)]
public class PlayerData : ScriptableObject
{
    [SerializeField] private int lifes;
    [SerializeField] private int points;
    [SerializeField] int initialLife = 3;
    public int InitialLife
    {
        get { return initialLife; }
    }
    public int Life
    {
        get { return lifes; }
    }

    public int Points
    {
        get { return points; }
    }

 
    public void ReInit()
    {
        lifes = initialLife;
        points = 0;
    }

    public void DiscountLife()
    {
         lifes -= 1;
    }
    public void AddPoint()
    {
        points+=1;
    }
}

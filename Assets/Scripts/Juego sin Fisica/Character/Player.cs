
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int lifes;
    [SerializeField] private int points;
    [SerializeField] int initialLife = 3;
    public bool ItsAlive { get; private set; }
    public bool Win {get;private set;}

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
        ItsAlive = true;
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


using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int lifes;
    [SerializeField] private int points;
    [SerializeField] int initialLife = 2;
    bool itsAlive = true;


    private void Awake()
    {
        ResetLifes();
    }

    public bool ItsAlive{
        get {   return itsAlive;    }
    }

    public void Died()
    {
        itsAlive = false;
    }

    public void Revived()
    {
        itsAlive = true;
    }
    public int Life
    {
        get { return lifes; }
    }

    public int Points
    {
        get { return points; }
    }

    public void DiscountLife()
    {
        lifes -= 1;
    }
    public void AddPoint()
    {
        points+=1;
    }

    public void ResetLifes()
    {
        lifes = initialLife;
    }


}

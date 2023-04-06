
using UnityEngine;

public class Player : MonoBehaviour
{
    private int lifes;
    private int points;
    [SerializeField] int initialLife=2;
    private void Awake()
    {
        ResetLifes();
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

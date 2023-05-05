using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Configuration",menuName ="Configuration",order = 0)]
public class Configuration : ScriptableObject
{
    [Range(1f, 10f)]
    [SerializeField] private float velocityEnemies;
    [Range(0.1f, 1f)]
    [SerializeField] private float incrementVelocityEnemies;
    public int cicle;

    public float VelocityEnemies{
        get { return velocityEnemies; }
        set { velocityEnemies = value; }
    }

    public void ReInit()
    {
        velocityEnemies = 1.5f;
    }

    public void Reset()
    {
        cicle = -1;
        velocityEnemies = 1f;

    }
    public void IncrementVelocity()
    {
        velocityEnemies += incrementVelocityEnemies;
    }

    public void IncrementCicle()
    {
        cicle += 1;
        
    }
}


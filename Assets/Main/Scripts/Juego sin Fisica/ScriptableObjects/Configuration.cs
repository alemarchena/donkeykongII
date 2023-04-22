using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Configuration",menuName ="Configuration",order = 0)]
public class Configuration : ScriptableObject
{
    [Range(1.5f, 10f)]
    [SerializeField] private float velocityEnemies;
 
    public float VelocityEnemies{
        get { return velocityEnemies; }
        set { velocityEnemies = value; }
    }

    public void ReInit()
    {
        velocityEnemies = 1.5f;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum TypeEnemy { Land, Air }
    public enum GivesPoints { Yes,No }
    public GivesPoints givesPoints;
}

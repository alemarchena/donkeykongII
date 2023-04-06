using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsUI : MonoBehaviour
{
    private Player player;
    [Tooltip("Es el objeto de tipo Text ubicado en el canvas del juego para mostrar los puntos")]
    [SerializeField] Text textPoints;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        try
        {
            textPoints.text = player.Points.ToString();
        }
        catch
        {
            Debug.LogError("El componente Player no se encuentra en el juego");
        }
    }
}

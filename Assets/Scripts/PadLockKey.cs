using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadLockKey : MonoBehaviour
{
    [Header("Ubicación Llave abajo")]
    [SerializeField] List<Vector2> PosLlaveAbajo = new List<Vector2>();

    [Header("Ubicación Llave arriba")]
    [SerializeField] List<Vector2> PosLlaveArriba = new List<Vector2>();

    public List<Vector2> posLlaveAbajo { get { return PosLlaveAbajo; } }
    public List<Vector2> posLlaveArriba { get { return PosLlaveArriba; } }


    [Header("Candados")]
    [SerializeField] int quantityPadLock;
    List<bool> padlocksOpen =new List<bool>();

    [Header("Ubicación candados")]
    [SerializeField] List<Vector2> PosCandados = new List<Vector2>();

    private void Start()
    {
        for (int a = 0; a < quantityPadLock;a++)
        {
            padlocksOpen.Add(false);
        }
    }
}

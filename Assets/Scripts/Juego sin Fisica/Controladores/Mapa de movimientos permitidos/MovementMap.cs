using System.Collections.Generic;
using UnityEngine;
using static Movement;


public sealed class MovementMap : MonoBehaviour 
{
    public bool cambioPantalla { get; set; }
    public bool estaEnNivelSuperior { get; set; }
    public bool noCaer { get; set; }
    public int contadorX { get; set; }
    public int contadorY { get; set; }

    [Header("Limites de movimiento")]
    [Tooltip("Vector que indica las posiciones en donde el personaje puede saltar")]
    [SerializeField] List<Vector2> PosSalto = new List<Vector2>();
    [Tooltip("Vector que indica las posiciones en donde el personaje puede subir")]
    [SerializeField] List<Vector2> PosSubir = new List<Vector2>();
    [Tooltip("Vector que indica las posiciones en donde el personaje puede bajar")]
    [SerializeField] List<Vector2> PosBajar = new List<Vector2>();
    [Tooltip("Vector que indica las posiciones en donde el personaje puede moverse a la izquierda")]
    [SerializeField] List<Vector2> PosIzquierda = new List<Vector2>();
    [Tooltip("Vector que indica las posiciones en donde el personaje puede moverse a la derecha")]
    [SerializeField] List<Vector2> PosDerecha   = new List<Vector2>();
    [Tooltip("Vector que indica las posiciones en donde el personaje puede saltar y no debe caer")]
    [SerializeField] List<Vector2> PosNoCaer    = new List<Vector2>();
    [Tooltip("Vector que indica las posiciones en donde el personaje esta para cambiar de pantalla tanto arriba como abajo")]
    [SerializeField] List<Vector2> PosCambioPantalla = new List<Vector2>();
    [Tooltip("Vector que indica las posicion en donde el personaje esta para cambiar de pantalla al bajar")]
    [SerializeField] List<Vector2> PaseDeNivelSuperior = new List<Vector2>();


    public List<Vector2> posSalto{get { return PosSalto; }}
    public List<Vector2> posSubir{get { return PosSubir; }}
    public List<Vector2> posBajar{get { return PosBajar; }}
    public List<Vector2> posDerecha{get { return PosDerecha; }}
    public List<Vector2> posIzquierda{get { return PosIzquierda; } }
    public List<Vector2> posCambioPantalla { get { return PosCambioPantalla; } }
    public List<Vector2> paseDeNivelSuperior { get { return PaseDeNivelSuperior; } }
    public List<Vector2> posNoCaer { get { return PosNoCaer; } }

    private void Start()
    {
        Reiniciar();
    }
    public void Reiniciar()
    {
        cambioPantalla      = false;
        estaEnNivelSuperior = false;
        noCaer = false;
        contadorX = 1;
        contadorY = 0;
    }

    public bool CheckMove(TipoMovimiento tp, Vector2 posicionActual)
    {
        bool estado = false;
        cambioPantalla = false;
        estaEnNivelSuperior = false;
        noCaer = false;

        List<Vector2> lista = new List<Vector2>();

        switch (tp)
        {
            case TipoMovimiento.tup:
                lista = posSubir; break;
            case TipoMovimiento.tdown:
                lista = posBajar; break;
            case TipoMovimiento.tleft:
                lista = posIzquierda; break;
            case TipoMovimiento.tright:
                lista = posDerecha; break;
            case TipoMovimiento.tpush:
                lista = posSalto; break;
        }

        foreach (Vector2 pos in lista)
        {
            if (posicionActual.x == pos.x && posicionActual.y == pos.y)
                estado = true;

            foreach (Vector2 posCP in posCambioPantalla)
            {
                if (posicionActual.x == posCP.x && posicionActual.y == posCP.y)
                    cambioPantalla = true;
            }

            foreach (Vector2 posNS in paseDeNivelSuperior)
            {
                if (posicionActual.x == posNS.x && posicionActual.y == posNS.y)
                    estaEnNivelSuperior = true;
            }

            foreach (Vector2 posNC in posNoCaer)
            {
                if (posicionActual.x == posNC.x && posicionActual.y == posNC.y)
                    noCaer = true;
            }
        }
        return estado;
    }
}

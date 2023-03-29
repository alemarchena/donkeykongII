using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public enum TipoMovimiento { tup, tdown, tleft, tright, tpush }
    public TipoMovimiento tipoMovimiento { get; set; }

    private Vector3 p;
    private bool canMove;
    private int contadorX;
    private int contadorY;
    Movement()
    {
        _stepY = 0.6f;
        _stepX = 0.75f;
    }
    MovementMap mMap;

    private void Awake()
    {
        mMap = FindObjectOfType<MovementMap>();
        canMove = false;
        ResetCounter();
    }
    public float _stepY { get; }
    public float _stepX { get; }

    private void ResetCounter()
    {
        contadorX = 1;
        contadorY = 0;
    }
    public void MovementNoControlled(Transform t, TipoMovimiento tm)
    {
        try
        {
            canMove = false;
            switch (tm)
            {
                case TipoMovimiento.tup:
                    p = new Vector3(t.position.x, t.position.y + _stepY);

                    break;
                case TipoMovimiento.tdown:
                    p = new Vector3(t.position.x, t.position.y - _stepY);

                    break;
                case TipoMovimiento.tleft:
                    p = new Vector3(t.position.x - _stepX, t.position.y);

                    break;
                case TipoMovimiento.tright:
                    p = new Vector3(t.position.x + _stepX, t.position.y);
                    break;
                case TipoMovimiento.tpush:
                    
                    p = new Vector3(t.position.x, t.position.y + _stepY);
                    break;
            }
           
            t.position = p;
        }
        catch (Exception e)
        {
            Debug.LogError("No se encontró el mapa de movimientos sin física");
        }
    }


    public void Move(Transform t, TipoMovimiento tm)
    {
        try
        {
            canMove = false;
            switch (tm)
            {
                case TipoMovimiento.tup:
                    if (mMap.CheckMove(TipoMovimiento.tup, new Vector2(mMap.contadorX, mMap.contadorY)))
                    {
                        contadorY += 1;
                        mMap.contadorY = contadorY;
                        if(mMap.cambioPantalla == true)
                        {
                            p = new Vector3(t.position.x, t.position.y + _stepY * 3);
                        }else
                            p = new Vector3(t.position.x, t.position.y + _stepY);

                        canMove = true;
                    }

                    break;
                case TipoMovimiento.tdown:
                    if (mMap.CheckMove(TipoMovimiento.tdown, new Vector2(mMap.contadorX, mMap.contadorY)))
                    {
                        contadorY -= 1;
                        mMap.contadorY = contadorY;

                        if (mMap.cambioPantalla == true)
                        {
                            if(mMap.estaEnNivelSuperior)
                                p = new Vector3(t.position.x, t.position.y - _stepY * 3);
                            else
                                p = new Vector3(t.position.x, t.position.y - _stepY);
                        }
                        else
                            p = new Vector3(t.position.x, t.position.y - _stepY);

                        canMove = true;

                    }
                    break;
                case TipoMovimiento.tleft:
                    if (mMap.CheckMove(TipoMovimiento.tleft, new Vector2(mMap.contadorX, mMap.contadorY)))
                    {
                        contadorX -= 1;
                        mMap.contadorX = contadorX;
                        p = new Vector3(t.position.x - _stepX, t.position.y);

                        canMove = true;
                    }

                    break;
                case TipoMovimiento.tright:
                    if (mMap.CheckMove(TipoMovimiento.tright, new Vector2(mMap.contadorX, mMap.contadorY)))
                    {
                        contadorX += 1;
                        mMap.contadorX = contadorX;
                        p = new Vector3(t.position.x + _stepX, t.position.y);
                        canMove = true;

                    }
                    break;
                case TipoMovimiento.tpush:
                    if (mMap.CheckMove(TipoMovimiento.tpush, new Vector2(mMap.contadorX, mMap.contadorY)))
                    {
                        contadorY += 1;
                        mMap.contadorY = contadorY;
                        p = new Vector3(t.position.x, t.position.y + _stepY);
                        canMove = true;

                        if (!mMap.noCaer)
                        StartCoroutine(BajarDelSalto(t));
                    }
                    break;
            }

            if (canMove)
            {
                t.position = p;

            }
        }
        catch (Exception e)
        {
            Debug.LogError("No se encontró el mapa de movimientos sin física");
        }
    }

    IEnumerator BajarDelSalto(Transform t)
    {
        yield return new WaitForSeconds(0.5f);
        contadorY -= 1;
        mMap.contadorY -= contadorY;
        p = new Vector3(t.position.x, t.position.y - _stepY);
        t.position = p;
    }

}

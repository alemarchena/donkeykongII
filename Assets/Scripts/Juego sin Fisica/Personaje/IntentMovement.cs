using System;
using System.Collections;
using UnityEngine;

public class IntentMovement : MonoBehaviour
{
    public enum TipoMovimiento { tup, tdown, tleft, tright, tpush }
    public TipoMovimiento tipoMovimiento { get; set; }

    private Vector3 p;
    private bool canMove;
    [SerializeField] int originalCounterPositionX = 1;
    [SerializeField] int originalCounterPositionY = 0;
    [SerializeField] private Vector3 vectorOriginalPosition;

    [SerializeField] private int counterX;
    [SerializeField] private int counterY;
    bool isJumping = false;
    private Player player;

    public int CounterX
    {
        get { return counterX; }
    }
    public int CounterY
    {
        get { return counterY;}
    }

    IntentMovement()
    {
        _stepY = 0.6f;
        _stepX = 0.75f;
    }
    MovementMap mMap;

    private void Awake()
    {
        mMap = FindObjectOfType<MovementMap>();
        player =FindObjectOfType<Player>();
        canMove = true;
        vectorOriginalPosition = transform.position;
        ResetCounter();

    }

    public float _stepY { get; }
    public float _stepX { get; }

    private void ResetCounter()
    {
        counterX = originalCounterPositionX;
        counterY = originalCounterPositionY;
    }
    public void ResetVectorOriginalPosition()
    {
        ResetCounter();
        transform.position = vectorOriginalPosition;
        mMap.Reiniciar();
        

        if (isJumping) //Es el caso de saltar y morir en el salto
        {
           StartCoroutine(SubirDeLaMuerte());
        }else
        {
            player.Revived();
        }
        
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
            if(player.ItsAlive)
            {
                canMove = false;
                switch (tm)
                {
                    case TipoMovimiento.tup:
                        if (mMap.CheckMove(TipoMovimiento.tup, new Vector2(mMap.contadorX, mMap.contadorY)))
                        {
                            isJumping = false;

                            counterY += 1;
                            mMap.contadorY = counterY;
                            if(mMap.cambioPantalla == true)
                            {
                                p = new Vector3(t.position.x, t.position.y + _stepY * 3);
                            }else
                                p = new Vector3(t.position.x, t.position.y + _stepY);

                            canMove = true;
                            t.position = p;


                        }

                        break;
                    case TipoMovimiento.tdown:
                        if (mMap.CheckMove(TipoMovimiento.tdown, new Vector2(mMap.contadorX, mMap.contadorY)))
                        {
                            isJumping = false;

                            counterY -= 1;
                            mMap.contadorY = counterY;

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
                            t.position = p;

                        }
                        break;
                    case TipoMovimiento.tleft:
                        if (mMap.CheckMove(TipoMovimiento.tleft, new Vector2(mMap.contadorX, mMap.contadorY)))
                        {
                            isJumping = false;

                            counterX -= 1;
                            mMap.contadorX = counterX;
                            p = new Vector3(t.position.x - _stepX, t.position.y);

                            canMove = true;
                            t.position = p;

                        }

                        break;
                    case TipoMovimiento.tright:
                        if (mMap.CheckMove(TipoMovimiento.tright, new Vector2(mMap.contadorX, mMap.contadorY)))
                        {
                            isJumping = false;

                            counterX += 1;
                            mMap.contadorX = counterX;
                            p = new Vector3(t.position.x + _stepX, t.position.y);
                            canMove = true;
                            t.position = p;


                        }
                        break;
                    case TipoMovimiento.tpush:
                        if (mMap.CheckMove(TipoMovimiento.tpush, new Vector2(mMap.contadorX, mMap.contadorY)))
                        {
                            isJumping = true;

                            counterY += 1;
                            mMap.contadorY = counterY;
                            p = new Vector3(t.position.x, t.position.y + _stepY);
                            t.position = p;

                            if (!mMap.noCaer)
                                StartCoroutine(BajarDelSalto(t));
                            else
                            {
                                canMove = true;
                            }
                        }
                        break;
                }
            }

        }
        catch
        {
            if(!mMap)
                Debug.LogError("No se encontró el mapa de movimientos sin física");

            if (!player)
                Debug.LogError("No se encontró el Player");
        }
    }

    IEnumerator BajarDelSalto(Transform t)
    {
        yield return new WaitForSeconds(1f);
        counterY -= 1;
        mMap.contadorY = counterY;
        p = new Vector3(t.position.x, t.position.y - _stepY);
        t.position = p;
        canMove = true;

    }

    IEnumerator SubirDeLaMuerte()
    {
        yield return new WaitForSeconds(1.1f);
        counterY += 1;
        mMap.contadorY = counterY;
        transform.position = new Vector3(transform.position.x, transform.position.y + _stepY);
        canMove = true;
        isJumping = false;
        player.Revived();
    }
}

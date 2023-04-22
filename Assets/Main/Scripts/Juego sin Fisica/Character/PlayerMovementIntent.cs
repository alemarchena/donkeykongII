using System;
using System.Collections;
using UnityEngine;

public class PlayerMovementIntent : MonoBehaviour
{
    [SerializeField] PlayerMovementMap playerMovementMap;
    [SerializeField] PlayerData playerData;
    public enum TipoMovimiento { tup, tdown, tleft, tright, tpush }
    public TipoMovimiento tipoMovimiento { get; set; }

    private Vector3 p;
    private bool canMove;
    [SerializeField] int originalCounterPositionX = 1;
    [SerializeField] int originalCounterPositionY = 0;
    [SerializeField] private Vector3 vectorOriginalPosition;

    private int counterX;
    private int counterY;
    private PlayerOperator playerOperator;

    public int CounterX
    {
        get { return counterX; }
    }
    public int CounterY
    {
        get { return counterY;}
    }

    PlayerMovementIntent()
    {
        _stepY = 0.6f;
        _stepX = 0.75f;
    }

    private void Awake()
    {
        playerOperator =FindObjectOfType<PlayerOperator>();
        canMove = true;
        ResetCounter();
    }

    private void Start()
    {
        playerMovementMap.Reiniciar();
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
        playerMovementMap.Reiniciar();
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
        catch
        {
            Debug.LogError("No se encontró el mapa de movimientos sin física");
        }
    }

    public void Move(Transform t, TipoMovimiento tm, out bool okMove)
    {
        okMove = false;
        try
        {
             if(playerData.Life>0)
            {
                canMove = false;
                switch (tm)
                {
                    case TipoMovimiento.tup:
                        if (playerMovementMap.CheckMove(TipoMovimiento.tup, new Vector2(playerMovementMap.contadorX, playerMovementMap.contadorY)))
                        {
                            counterY += 1;
                            playerMovementMap.contadorY = counterY;
                            if(playerMovementMap.cambioPantalla == true)
                            {
                                p = new Vector3(t.position.x, t.position.y + _stepY * 3);
                            }else
                                p = new Vector3(t.position.x, t.position.y + _stepY);

                            canMove = true;
                            t.position = p;
                            okMove = true;
                        }

                        break;
                    case TipoMovimiento.tdown:
                        if (playerMovementMap.CheckMove(TipoMovimiento.tdown, new Vector2(playerMovementMap.contadorX, playerMovementMap.contadorY)))
                        {
                            counterY -= 1;
                            playerMovementMap.contadorY = counterY;

                            if (playerMovementMap.cambioPantalla == true)
                            {
                                if(playerMovementMap.estaEnNivelSuperior)
                                    p = new Vector3(t.position.x, t.position.y - _stepY * 3);
                                else
                                    p = new Vector3(t.position.x, t.position.y - _stepY);
                            }
                            else
                                p = new Vector3(t.position.x, t.position.y - _stepY);

                            canMove = true;
                            t.position = p;
                            okMove = true;

                        }
                        break;
                    case TipoMovimiento.tleft:
                        if (playerMovementMap.CheckMove(TipoMovimiento.tleft, new Vector2(playerMovementMap.contadorX, playerMovementMap.contadorY)))
                        {
                            counterX -= 1;
                            playerMovementMap.contadorX = counterX;
                            p = new Vector3(t.position.x - _stepX, t.position.y);

                            canMove = true;
                            t.position = p;
                            okMove = true;

                        }
                        break;
                    case TipoMovimiento.tright:
                        if (playerMovementMap.CheckMove(TipoMovimiento.tright, new Vector2(playerMovementMap.contadorX, playerMovementMap.contadorY)))
                        {
                            counterX += 1;
                            playerMovementMap.contadorX = counterX;
                            p = new Vector3(t.position.x + _stepX, t.position.y);
                            canMove = true;
                            t.position = p;
                            okMove = true;

                        }
                        break;
                    case TipoMovimiento.tpush:
                        if (playerMovementMap.CheckMove(TipoMovimiento.tpush, new Vector2(playerMovementMap.contadorX, playerMovementMap.contadorY)))
                        {
                            counterY += 1;
                            playerMovementMap.contadorY = counterY;
                            p = new Vector3(t.position.x, t.position.y + _stepY);
                            t.position = p;

                            if (!playerMovementMap.noCaer)
                                StartCoroutine(BajarDelSalto(t));
                            else
                            {
                                canMove = true;
                            }
                            okMove = true;

                        }
                        break;
                }
            }
        }
        catch
        {
            if(!playerMovementMap)
                Debug.LogError("No se encontró el mapa de movimientos sin física");

            if (!playerOperator)
                Debug.LogError("No se encontró el Player Informant");
        }
    }

    IEnumerator BajarDelSalto(Transform t)
    {
        yield return new WaitForSeconds(1f);

        if(transform.position != vectorOriginalPosition)
        {
            counterY -= 1;
            playerMovementMap.contadorY = counterY;
            p = new Vector3(t.position.x, t.position.y - _stepY);
            t.position = p;
            canMove = true;
        }

    }
}

using System;
using UnityEngine;

public class MovementAutomaticWithCounter : MonoBehaviour
{
    public enum TypeMovementAutomatic { HorizontalBounce, HorizontalFromLeft, HorizontalFromRight }
    public enum InitialPosition { Left, Right }

    [SerializeField] TypeMovementAutomatic typeMovementRobot;
    [SerializeField] InitialPosition initialPosition;
    [Space]

    [Header("Displacement Quantity")]
    [SerializeField] float _step;

    [Range(0, 10)]
    [SerializeField] float _velocity;


    /// <summary>
    /// Un valor flotante entre cero y diez
    /// </summary>
    public float VelocityMovement
    {
        get
        {
            return _velocity;
        }
        set
        {
            if (value > 0 && value <= 10)
                _velocity = value;
            else
                Debug.LogError("El tiempo debe ser un valor entre cero y uno");
        }
    }

    [Space]
    [Header("Horizontal Parameters")]
    [SerializeField] int _limitCounterRightX;
    [SerializeField] int _limitCounterLeftX;
    [SerializeField] private bool canTeletransport;


    private bool movingToRight;
    private bool LookAtRight;
    private SpriteRenderer sprite;
    private Transform t;
    private float counterTime;
    private bool canMove;
    private int contadorX;
    private Vector3 positionStart;
    

    private void Awake()
    {
        t = GetComponent<Transform>();
        positionStart = t.position;

        sprite = GetComponent<SpriteRenderer>();

        ResetTime();
        ResetCounter();
    }
    
    private void ResetTime()
    {
        counterTime = 0;
        canMove = true;
    }
    private void ResetCounter()
    {
        switch (typeMovementRobot)
        {
            case TypeMovementAutomatic.HorizontalBounce:
                if (initialPosition == InitialPosition.Right)
                    contadorX = _limitCounterRightX;
                else
                    contadorX = _limitCounterLeftX;
                
                break;
            case TypeMovementAutomatic.HorizontalFromLeft:
                contadorX = _limitCounterLeftX;
                transform.position = positionStart;
                break;
            case TypeMovementAutomatic.HorizontalFromRight:
                contadorX = _limitCounterRightX;
                transform.position = positionStart;
                break;
        }
        Visibilidad();
    }
    
    private void Update()
    {
        counterTime += _velocity * Time.deltaTime;
        if(counterTime > 1)
        {
            ResetTime();
        }
        if(canMove)
        {
            try
            {
                switch (typeMovementRobot)
                {
                    case TypeMovementAutomatic.HorizontalBounce:
                        HorizontalBounceCounter();
                        break;
                    case TypeMovementAutomatic.HorizontalFromLeft:
                        HorizontalFromLeft();
                        break;
                    case TypeMovementAutomatic.HorizontalFromRight:
                        HorizontalFromRight();
                        break;
                }
            }catch(Exception e)
            {
                Debug.LogError("No se ha encontrado el componente Transform en el objeto actual");
            }
            canMove = false;
        }
    }

    private void Visibilidad()
    {
        if (contadorX == _limitCounterRightX || contadorX == _limitCounterLeftX)
            sprite.enabled = false;
        else
            sprite.enabled = true;
    }
    private void HorizontalBounceCounter()
    {

        if (contadorX >= _limitCounterRightX)
        {
            MoveToLeft();
            contadorX = _limitCounterRightX;
        }
        else if (contadorX <= _limitCounterLeftX)
        {
            MoveToRight();
            contadorX = _limitCounterLeftX;
        }


        if (movingToRight)
        {
            t.position = new Vector3(t.position.x - _step, t.position.y, t.position.z);
            contadorX -= 1;
        }
        else
        {
            t.position = new Vector3(t.position.x + _step, t.position.y, t.position.z);
            contadorX += 1;
        }
        Visibilidad();
    }

    private void MoveToLeft()
    {
        movingToRight = true;
        if (LookAtRight)
        {
            sprite.flipX = false;
            LookAtRight = false;
        }
    }

    private void MoveToRight()
    {
        movingToRight = false;
        sprite.flipX = true;
        if (!LookAtRight)
        {
            LookAtRight = true;
            sprite.flipX = true;
        }
    }


    private void HorizontalFromLeft()
    {
        if (contadorX <= _limitCounterLeftX)
        {
            MoveToRight();
            contadorX = _limitCounterLeftX;
        }

        if (contadorX < _limitCounterRightX)
        {
            t.position = new Vector3(t.position.x + _step, t.position.y, t.position.z);
            contadorX += 1;
        }
        Visibilidad();
        Teletransport();

    }

    private void HorizontalFromRight()
    {
        if (contadorX >= _limitCounterRightX)
        {
            MoveToLeft();
            contadorX = _limitCounterRightX;
        }

        if (contadorX > _limitCounterLeftX)
        {
            t.position = new Vector3(t.position.x - _step, t.position.y, t.position.z);
            contadorX -= 1;
        }

        Visibilidad();
        Teletransport();
    }

    private void Teletransport()
    {
        if (canTeletransport && (contadorX == _limitCounterLeftX || contadorX == _limitCounterRightX))
        {
            ResetCounter();
        }
    }
}

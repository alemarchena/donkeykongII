using System;
using UnityEngine;

public class EnemyMovementAutomaticWithCounter : MonoBehaviour
{
    [SerializeField] Configuration configuration;
    public enum TypeMovementAutomatic { HorizontalBounce, HorizontalFromLeft, HorizontalFromRight, DownHorizontalBounce }
    public enum InitialPosition { Left, Right }
    public enum ViewPosition { Left, Right }

    [SerializeField] TypeMovementAutomatic typeMovementRobot;
    [Tooltip("Establece la posicion inicial en pantalla del objeto")]
    [SerializeField] InitialPosition initialPosition;
    [Space]

    [Header("Displacement Quantity")]
    [SerializeField] float _stepX=0.75f;
    [SerializeField] float _stepY=0.6f;


    [Tooltip("Establece el contador inicial en X del objeto")]
    [SerializeField] private int counterX;
    [Tooltip("Establece el contador inicial en Y del objeto")]
    [SerializeField] private int counterY;

    public int CounterX
    {
        get { return counterX; }
    }
    public int CounterY
    {
        get { return counterY; }
    }

    /// <summary>
    /// Un valor flotante entre cero y diez
    /// </summary>
    public float VelocityMovement
    {
        get
        {
            return configuration.VelocityEnemies;
        }
        set
        {
            if (value > 0 && value <= 10)
                configuration.VelocityEnemies = value;
            else
                Debug.LogError("El tiempo debe ser un valor entre cero y uno");
        }
    }

    [Space]
    [Header("Horizontal Parameters")]
    [SerializeField] int _limitCounterRightX;
    [SerializeField] int _limitCounterLeftX;
    [SerializeField] private bool canTeletransportX = false;

    [Header("Vertical Parameters")]
    [SerializeField] int _limitDownCounterY;
    [SerializeField] private bool canTeletransportY=false;
    int _initCounterY;


    private bool moveToRight;
    private SpriteRenderer sprite;
    private Transform t;
    private float counterTime;
    private bool canMove;


    private Vector3 positionStart;
    

    private void Awake()
    {
        t = GetComponent<Transform>();
        positionStart = t.position;

        sprite = GetComponent<SpriteRenderer>();
        ResetTime();
        _initCounterY = counterY;

        ResetCounter();

        if (!configuration) Debug.LogError("Falta asignar el archivo Configuration");

        if(initialPosition == InitialPosition.Left) 
            moveToRight = true;
        else 
            moveToRight = false;

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
                    counterX = _limitCounterRightX;
                else
                    counterX = _limitCounterLeftX;
                
                break;
            case TypeMovementAutomatic.DownHorizontalBounce:
                if (initialPosition == InitialPosition.Right)
                    counterX = _limitCounterRightX;
                else
                    counterX = _limitCounterLeftX;

                counterY = _initCounterY;

                break;
            case TypeMovementAutomatic.HorizontalFromLeft:
                counterX = _limitCounterLeftX;
                transform.position = positionStart;
                break;
            case TypeMovementAutomatic.HorizontalFromRight:
                counterX = _limitCounterRightX;
                transform.position = positionStart;
                break;
        }
        Visibilidad();
    }
    
    private void Update()
    {
        counterTime += configuration.VelocityEnemies * Time.deltaTime;
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
                    case TypeMovementAutomatic.DownHorizontalBounce:
                        DownHorizontalBounceCounter();
                        break;
                    case TypeMovementAutomatic.HorizontalFromLeft:
                        HorizontalFromLeft();
                        break;
                    case TypeMovementAutomatic.HorizontalFromRight:
                        HorizontalFromRight();
                        break;
                }
            }catch
            {
                Debug.LogError("No se ha encontrado el componente Transform en el objeto actual");
            }
            canMove = false;
        }
    }

    private void Visibilidad()
    {
        if (counterX == _limitCounterRightX || counterX == _limitCounterLeftX)
            sprite.enabled = false;
        else
            sprite.enabled = true;
    }
    private void HorizontalBounceCounter()
    {
        Visibilidad();

        if (counterX >= _limitCounterRightX)
        {
            FinRightPosition();
            counterX = _limitCounterRightX;
        }
        else if (counterX <= _limitCounterLeftX)
        {
            FinLeftPosition();
            counterX = _limitCounterLeftX;
        }


        if (moveToRight)
        {
            t.position = new Vector3(t.position.x - _stepX, t.position.y, t.position.z);
            counterX -= 1;
        }
        else
        {
            t.position = new Vector3(t.position.x + _stepX, t.position.y, t.position.z);
            counterX += 1;
        }
        Visibilidad();

    }

    private void DownHorizontalBounceCounter()
    {
        Visibilidad();

        if (counterX >= _limitCounterRightX)
        {
            FinRightPosition();
            counterX = _limitCounterRightX;
            t.position = new Vector3(t.position.x, t.position.y - _stepY, t.position.z);
            counterY -= 1;
        }
        else if (counterX <= _limitCounterLeftX)
        {
            FinLeftPosition();
            counterX = _limitCounterLeftX;
            t.position = new Vector3(t.position.x , t.position.y - _stepY, t.position.z);
            counterY -= 1;

        }


        if (moveToRight)
        {

            t.position = new Vector3(t.position.x - _stepX, t.position.y, t.position.z);
            counterX -= 1;
        }
        else
        {
            t.position = new Vector3(t.position.x + _stepX, t.position.y, t.position.z);
            counterX += 1;
        }

        if (canTeletransportY)
        {
            if (counterY < _limitDownCounterY)
            {
                transform.position = positionStart;
                ResetCounter();
            }
        }
        else
        {
            if (counterY < _limitDownCounterY)
                canMove = false;
        }

        Visibilidad();
    }
    private void FinRightPosition()
    {
        moveToRight = true;
        sprite.flipX = !sprite.flipX;
    }

    private void FinLeftPosition()
    {
        moveToRight = false;
        sprite.flipX = !sprite.flipX;
    }

    private void HorizontalFromLeft()
    {
        if (counterX <= _limitCounterLeftX)
        {
            FinLeftPosition();
            counterX = _limitCounterLeftX;
        }

        if (counterX < _limitCounterRightX)
        {
            t.position = new Vector3(t.position.x + _stepX, t.position.y, t.position.z);
            counterX += 1;
        }
        Visibilidad();
        TeletransportX();

    }

    private void HorizontalFromRight()
    {
        if (counterX >= _limitCounterRightX)
        {
            FinRightPosition();
            counterX = _limitCounterRightX;
        }

        if (counterX > _limitCounterLeftX)
        {
            t.position = new Vector3(t.position.x - _stepX, t.position.y, t.position.z);
            counterX -= 1;
        }

        Visibilidad();
        TeletransportX();
    }

    private void TeletransportX()
    {
        if (canTeletransportX && (counterX == _limitCounterLeftX || counterX == _limitCounterRightX))
        {
            ResetCounter();
        }
    }
}

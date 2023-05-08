using System;
using System.Collections;
using UnityEngine;

public class EnemyMovementAutomaticWithCounter : MonoBehaviour
{
    ControllerGame controllerGame;

    [SerializeField] Configuration configuration;
    [Range(0f,9f)]
    [SerializeField] float velocityAddEnemy;
    private float originalVelocity;
    public enum TypeMovementAutomatic { HorizontalBounce, HorizontalFromLeft, HorizontalFromRight, DownHorizontalBounce }
    public enum InitialPosition { Left, Right }
    public enum ViewPosition { Left, Right }

    [Tooltip("It Wait a time for initialize")]
    [SerializeField] float startTimeDelayInSeconds;
    [Space]
    [SerializeField] TypeMovementAutomatic typeMovementRobot;
    [Tooltip("Set the start position object in the screen ")]
    [SerializeField] InitialPosition initialPosition;
    [Space]

    [Header("Displacement Quantity")]
    [SerializeField] float _stepX=0.75f;
    [SerializeField] float _stepXlimit=0.375f;
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
    private float counterDelayTime=0;
    private bool okStart=false;

    private void Awake()
    {
        controllerGame = FindObjectOfType<ControllerGame>();
        if (!controllerGame) Debug.LogError("Falta ControllerGame en el juego");

        t = GetComponent<Transform>();
        positionStart = t.position;
        ResetTime();
        sprite = GetComponent<SpriteRenderer>();
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
    }
    

    private void Update()
    {

        if (controllerGame.Playing)
        {
            if (counterDelayTime <= startTimeDelayInSeconds && !okStart)
                counterDelayTime += Time.deltaTime;

            if(counterDelayTime > startTimeDelayInSeconds && !okStart)
                okStart = true;

            counterTime += (configuration.VelocityEnemies + velocityAddEnemy) * Time.deltaTime;
            if(counterTime > 1)
                ResetTime();

            if(okStart)
            {
                if (canMove)
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

        }
    }

  
    private void Visibilidad()
    {
        StartCoroutine(Blink());

    }

    IEnumerator Blink()
    {
        Color originalColor = sprite.color;
       

        sprite.color = Color.green;
        yield return new WaitForSeconds(0.03f);
        sprite.enabled = false;
        yield return new WaitForSeconds(0.03f);
        sprite.enabled = true;
        yield return new WaitForSeconds(0.03f);
        sprite.enabled = false;
        yield return new WaitForSeconds(0.03f);
        sprite.enabled = true;
        sprite.color = Color.green;
        yield return new WaitForSeconds(0.03f);
        sprite.enabled = true;
        sprite.color = originalColor;



    }
    private void HorizontalBounceCounter()
    {
        
        
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
        


    }

    private void DownHorizontalBounceCounter()
    {

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
            if (counterX == _limitCounterRightX || counterX == _limitCounterLeftX)
                t.position = new Vector3(t.position.x - _stepX, t.position.y, t.position.z);
            else
                t.position = new Vector3(t.position.x - _stepX, t.position.y, t.position.z);

            counterX -= 1;
        }
        else
        {
            if (counterX == _limitCounterRightX || counterX == _limitCounterLeftX)
                t.position = new Vector3(t.position.x + _stepX, t.position.y, t.position.z);
            else
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

    }
    private void FinRightPosition()
    {
        moveToRight = true;
        sprite.flipX = !sprite.flipX;
        Visibilidad();


    }

    private void FinLeftPosition()
    {
        moveToRight = false;
        sprite.flipX = !sprite.flipX;
        Visibilidad();

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

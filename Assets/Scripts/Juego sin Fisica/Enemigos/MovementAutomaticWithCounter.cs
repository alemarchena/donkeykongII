using System;
using UnityEngine;

public class MovementAutomaticWithCounter : MonoBehaviour
{
    enum TypeMovementBot { HorizontalBounce,VerticalBounce,HorizontalFromLeft, HorizontalFromRight,VerticalFromAbove, VerticalFromBelow }

    [SerializeField] TypeMovementBot typeMovementRobot;
    [Space]

    [Header("Displacement Quantity")]
    [SerializeField] float _step;

    [SerializeField] float _velocity;

    public float VelocityMovement
    {
        get
        {
            return _velocity;
        }
        set
        {
            if (value > 0 && value <= 1)
                _velocity = value;
            else
                Debug.LogError("El tiempo debe ser un valor entre cero y uno");
        }
    }

    public enum InitialPosition { Left,Right}

    [Space]
    [Header("Horizontal Parameters")]
    [SerializeField] float _limitCounterRightX;
    [SerializeField] float _limitCounterLeftX;
    [SerializeField] InitialPosition initialPosition;

    private bool movingToRight;
    private bool LookAtRight;

    [Header("Vertical Parameters")]
    [SerializeField] float _limitCounterAboveY;
    [SerializeField] float _limitCounterBelowY;
    private bool movingUp;

    private SpriteRenderer sprite;
    private Transform t;
    private float counterTime;
    private bool canMove;

    [SerializeField] private int contadorX;
    [SerializeField] private int contadorY;
    private void Awake()
    {
        //t = GetComponent<Transform>();
        t = transform;
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
        if (initialPosition == InitialPosition.Right)
            contadorX = 7;
        else
            contadorX = 0;

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
                    case TypeMovementBot.HorizontalBounce:
                        HorizontalBounceCounter();
                        break;
                    case TypeMovementBot.VerticalBounce:
                        VerticalBounce();
                        break;
                    case TypeMovementBot.HorizontalFromLeft:
                        HorizontalFromLeft();
                        break;
                    case TypeMovementBot.HorizontalFromRight:
                        HorizontalFromRight();
                        break;
                    case TypeMovementBot.VerticalFromAbove:
                        VerticalFromAbove();
                        break;
                    case TypeMovementBot.VerticalFromBelow:
                        VerticalFromBelow();
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
        if (contadorX == 7 || contadorX == 0)
            sprite.enabled = false;
        else
            sprite.enabled = true;
    }
    private void HorizontalBounceCounter()
    {

        if (contadorX >= 7)
        {
            MoveToLeft();
            contadorX = 7;
        }
        else if (contadorX <= 0)
        {
            MoveToRight();
            contadorX = 0;
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

    private void VerticalBounce()
    {
        if (t.position.y > _limitCounterAboveY)
        {
            movingUp= true;
        }
        else if (t.position.y < _limitCounterBelowY)
        {
            movingUp= false;
        }

        if (movingUp)
        {
            t.position = new Vector3(t.position.x, t.position.y - _step, t.position.z);
        }
        else
        {
            t.position = new Vector3(t.position.x, t.position.y + _step, t.position.z);
        }
    }

    private void HorizontalFromLeft()
    {
        if (t.position.x < _limitCounterRightX)
        {
            t.position = new Vector3(t.position.x + _step, t.position.y, t.position.z);
        }

    }

    private void HorizontalFromRight()
    {
        if (t.position.x > _limitCounterLeftX)
        {
            t.position = new Vector3(t.position.x - _step, t.position.y, t.position.z);
        }
    }

    private void VerticalFromAbove()
    {
        if (t.position.y > _limitCounterBelowY)
        {
            t.position = new Vector3(t.position.x, t.position.y - _step, t.position.z);
        }
    }

    private void VerticalFromBelow()
    {
        if (t.position.y < _limitCounterAboveY)
        {
            t.position = new Vector3(t.position.x, t.position.y + _step, t.position.z);
        }
    }
}

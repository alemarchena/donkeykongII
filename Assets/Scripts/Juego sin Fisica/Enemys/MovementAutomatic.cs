using System;
using UnityEngine;

public class MovementAutomatic : MonoBehaviour
{
    enum TypeMovementRobot { HorizontalBounce,VerticalBounce,HorizontalFromLeft, HorizontalFromRight,VerticalFromAbove, VerticalFromBelow }

    [SerializeField] TypeMovementRobot typeMovementRobot;
    [Space]

    [Header("Displacement Quantity")]
    [SerializeField] float _step=0.75f;

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
    [SerializeField] float _limitRightX;
    [SerializeField] float _limitLeftX;
    private bool movingToRight;
    private bool LookAtRight;

    [Header("Vertical Parameters")]
    [SerializeField] float _limitAboveY;
    [SerializeField] float _limitBelowY;
    private bool movingUp;

    private SpriteRenderer sprite;
    private Transform t;
    private float counterTime;
    private bool canMove;

    private void Awake()
    {
        t = GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();
        ResetTime();
    }
    
    private void ResetTime()
    {
        counterTime = 0;
        canMove = true;
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
                    case TypeMovementRobot.HorizontalBounce:
                        HorizontalBounce();
                        break;
                    case TypeMovementRobot.VerticalBounce:
                        VerticalBounce();
                        break;
                    case TypeMovementRobot.HorizontalFromLeft:
                        HorizontalFromLeft();
                        break;
                    case TypeMovementRobot.HorizontalFromRight:
                        HorizontalFromRight();
                        break;
                    case TypeMovementRobot.VerticalFromAbove:
                        VerticalFromAbove();
                        break;
                    case TypeMovementRobot.VerticalFromBelow:
                        VerticalFromBelow();
                        break;
                }
            }catch
            {
                Debug.LogError("No se ha encontrado el componente Transform en el objeto actual");
            }
            canMove = false;
        }

    }

    private void HorizontalBounce()
    {
        if (t.position.x > _limitRightX)
        {
            movingToRight = true;
            if (LookAtRight)
            {
                sprite.flipX = false;
                LookAtRight = false;
            }
        }
        else if(t.position.x < _limitLeftX)
        {
            movingToRight = false;
            sprite.flipX = true;
            if (!LookAtRight)
            {
                LookAtRight = true;
                sprite.flipX = true;
            }
        }

        if (movingToRight)
        {
            t.position = new Vector3(t.position.x - _step, t.position.y, t.position.z);
        }
        else
        {
            t.position = new Vector3(t.position.x + _step, t.position.y, t.position.z);
        }
    }

    private void VerticalBounce()
    {
        if (t.position.y > _limitAboveY)
        {
            movingUp= true;
        }
        else if (t.position.y < _limitBelowY)
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
        if (t.position.x < _limitRightX)
        {
            t.position = new Vector3(t.position.x + _step, t.position.y, t.position.z);
        }

    }

    private void HorizontalFromRight()
    {
        if (t.position.x > _limitLeftX)
        {
            t.position = new Vector3(t.position.x - _step, t.position.y, t.position.z);
        }
    }

    private void VerticalFromAbove()
    {
        if (t.position.y > _limitBelowY)
        {
            t.position = new Vector3(t.position.x, t.position.y - _step, t.position.z);
        }
    }

    private void VerticalFromBelow()
    {
        if (t.position.y < _limitAboveY)
        {
            t.position = new Vector3(t.position.x, t.position.y + _step, t.position.z);
        }
    }
}

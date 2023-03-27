using System;
using UnityEngine;

public class MovementAutomatic : MonoBehaviour
{
    enum TypeMovementBot { HorizontalBounce,VerticalBounce,HorizontalFromLeft, HorizontalFromRight,VerticalFromAbove, VerticalFromBelow }

    [SerializeField] TypeMovementBot typeMovementRobot;
    [Space]

    [Header("Displacement Quantity")]
    [SerializeField] float _step;
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
    

    private void Awake()
    {
        //t = GetComponent<Transform>();
        t = transform;
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        try
        {
            switch (typeMovementRobot)
            {
                case TypeMovementBot.HorizontalBounce:
                    HorizontalBounce();
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

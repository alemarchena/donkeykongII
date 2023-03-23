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
    [SerializeField] private bool movingToRight;
    [SerializeField] private bool LookAtRight;

    private SpriteRenderer sprite;
    private Transform t;
    
    private void Awake()
    {
        t = GetComponent<Transform>();
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
                    break;
                case TypeMovementBot.HorizontalFromLeft:
                    break;
                case TypeMovementBot.HorizontalFromRight:
                    break;
                case TypeMovementBot.VerticalFromAbove:
                    break;
                case TypeMovementBot.VerticalFromBelow:
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
            t.position = new Vector3(t.position.x - _step,t.position.y,t.position.z);
        }
        else
        {
            t.position = new Vector3(t.position.x + _step, t.position.y, t.position.z);
        }
    }

    private void VerticalBounce()
    {

    }

    private void HorizontalFromLeft()
    {

    }

    private void HorizontalFromRight()
    {

    }

    private void VerticalFromAbove()
    {

    }

    private void VerticalFromBelow()
    {

    }
}

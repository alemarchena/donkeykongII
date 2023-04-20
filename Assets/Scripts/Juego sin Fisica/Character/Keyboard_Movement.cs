using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Keyboard_Movement : MonoBehaviour
{

    [SerializeField] KeyCode _up;
    [SerializeField] KeyCode _down;
    [SerializeField] KeyCode _left;
    [SerializeField] KeyCode _right;

    [SerializeField] KeyCode _jump;


    Transform t;
    PlayerMovementIntent m;

    private void Awake()
    {
        t = GetComponent<Transform>();
        m = GetComponent<PlayerMovementIntent>();
    }



    void Update()
    {
        try
        {
            if( Input.GetKeyDown( _up )  )
            {
                m.Move(t,PlayerMovementIntent.TipoMovimiento.tup);
            }
            if (Input.GetKeyDown(_down))
            {
                m.Move(t, PlayerMovementIntent.TipoMovimiento.tdown);
            }
            if (Input.GetKeyDown(_left))
            {
                m.Move(t, PlayerMovementIntent.TipoMovimiento.tleft);
            }
            if (Input.GetKeyDown(_right))
            {
                m.Move(t, PlayerMovementIntent.TipoMovimiento.tright);
            }
            if (Input.GetKeyDown(_jump))
            {
                m.Move(t, PlayerMovementIntent.TipoMovimiento.tpush);
            }
        }
        catch  {
            Debug.LogError("Hay un error en el código");
        }
    }
       
}

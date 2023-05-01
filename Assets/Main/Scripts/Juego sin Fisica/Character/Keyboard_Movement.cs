using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Keyboard_Movement : MonoBehaviour
{

    ControllerSound controllerSound;

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

        controllerSound = FindObjectOfType<ControllerSound>();
        if (!controllerSound) Debug.LogError("Falta el ControllerSound en el juego");

    }



    void Update()
    {
        try
        {
            if( Input.GetKeyDown( _up )  )
            {
                m.Move(t,PlayerMovementIntent.TipoMovimiento.tup,out bool okMove);
                if (okMove) controllerSound.PlayStepPlayer();
            }
            if (Input.GetKeyDown(_down))
            {
                m.Move(t, PlayerMovementIntent.TipoMovimiento.tdown, out bool okMove);
                if (okMove) controllerSound.PlayStepPlayer();
            }
            if (Input.GetKeyDown(_left))
            {
                m.Move(t, PlayerMovementIntent.TipoMovimiento.tleft, out bool okMove);
                if (okMove) controllerSound.PlayStepPlayer();
            }
            if (Input.GetKeyDown(_right))
            {
                m.Move(t, PlayerMovementIntent.TipoMovimiento.tright, out bool okMove);
                if (okMove) controllerSound.PlayStepPlayer();
            }
            if (Input.GetKeyDown(_jump))
            {
                m.Move(t, PlayerMovementIntent.TipoMovimiento.tpush, out bool okMove);
                if (okMove) controllerSound.PlayPlayerJump();
            }
        }
        catch  {
            Debug.LogError("Hay un error en el c�digo");
        }
    }
       
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons_Movement : MonoBehaviour
{
    ControllerSound controllerSound;
    Transform t;
    PlayerMovementIntent m;

    private void Awake()
    {
        t = GetComponent<Transform>();
        try
        {
            m = GetComponent<PlayerMovementIntent>();
        }catch
        {
            Debug.LogError("Falta el asociar el componente Movement al objeto");
            return;
        }

        controllerSound = FindObjectOfType<ControllerSound>();
        if (!controllerSound) Debug.LogError("Falta el ControllerSound en el juego");
    }

    public void Up()
    {
        m.Move(t,PlayerMovementIntent.TipoMovimiento.tup, out bool okMove);
        if (okMove) controllerSound.PlayStepPlayer();
    }

    public void Down()
    {
        m.Move(t, PlayerMovementIntent.TipoMovimiento.tdown, out bool okMove);
        if (okMove) controllerSound.PlayStepPlayer();
    }

    public void Left()
    {
        m.Move(t, PlayerMovementIntent.TipoMovimiento.tleft, out bool okMove);
        if (okMove) controllerSound.PlayStepPlayer();
    }

    public void Right()
    {
        m.Move(t, PlayerMovementIntent.TipoMovimiento.tright, out bool okMove);
        if (okMove) controllerSound.PlayStepPlayer();
    }

    public void Jump()
    {
         m.Move(t, PlayerMovementIntent.TipoMovimiento.tpush, out bool okMove);
        if (okMove) controllerSound.PlayPlayerJump();

    }
}

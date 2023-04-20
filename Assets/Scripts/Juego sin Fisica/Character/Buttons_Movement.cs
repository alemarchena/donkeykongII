using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons_Movement : MonoBehaviour
{
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
    }

    public void Up()
    {
        m.Move(t,PlayerMovementIntent.TipoMovimiento.tup);
    }

    public void Down()
    {
        m.Move(t, PlayerMovementIntent.TipoMovimiento.tdown);
    }

    public void Left()
    {
        m.Move(t, PlayerMovementIntent.TipoMovimiento.tleft);
    }

    public void Right()
    {
        m.Move(t, PlayerMovementIntent.TipoMovimiento.tright);
    }

    public void Jump()
    {
         m.Move(t, PlayerMovementIntent.TipoMovimiento.tpush);
    }
}

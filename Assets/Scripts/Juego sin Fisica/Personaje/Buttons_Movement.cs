using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons_Movement : MonoBehaviour
{
    Transform t;
    Movement m;

    private void Awake()
    {
        t = GetComponent<Transform>();
        try
        {
            m = GetComponent<Movement>();
        }catch
        {
            Debug.LogError("Falta el asociar el componente Movement al objeto");
            return;
        }
    }

    public void Up()
    {
        m.Move(t,Movement.TipoMovimiento.tup);
    }

    public void Down()
    {
        m.Move(t, Movement.TipoMovimiento.tdown);
    }

    public void Left()
    {
        m.Move(t, Movement.TipoMovimiento.tleft);
    }

    public void Right()
    {
        m.Move(t, Movement.TipoMovimiento.tright);
    }

    public void Jump()
    {
        m.Move(t, Movement.TipoMovimiento.tpush);
    }
}

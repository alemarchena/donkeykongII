using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons_Movement : MonoBehaviour
{
    Transform t;
    IntentMovement m;

    private void Awake()
    {
        t = GetComponent<Transform>();
        try
        {
            m = GetComponent<IntentMovement>();
        }catch
        {
            Debug.LogError("Falta el asociar el componente Movement al objeto");
            return;
        }
    }

    public void Up()
    {
        m.Move(t,IntentMovement.TipoMovimiento.tup);
    }

    public void Down()
    {
        m.Move(t, IntentMovement.TipoMovimiento.tdown);
    }

    public void Left()
    {
        m.Move(t, IntentMovement.TipoMovimiento.tleft);
    }

    public void Right()
    {
        m.Move(t, IntentMovement.TipoMovimiento.tright);
    }

    public void Jump()
    {
        m.Move(t, IntentMovement.TipoMovimiento.tpush);
    }
}

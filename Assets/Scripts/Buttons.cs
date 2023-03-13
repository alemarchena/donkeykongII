using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    Transform t;
    Movement m;

    private void Awake()
    {
        t = GetComponent<Transform>();
        m = GetComponent<Movement>();
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

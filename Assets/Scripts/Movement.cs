using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public enum TipoMovimiento { tup,tdown,tleft,tright,tpush }
    public TipoMovimiento tipoMovimiento { get; set; }

    private Vector3 p;


    Movement()
    {
        _step = 0.6f;
    }
    public float _step { get; }

    
    public void Move(Transform t, TipoMovimiento tm)
    {
        switch (tm)
        {
            case TipoMovimiento.tup:
                p = new Vector3(t.position.x, t.position.y + _step);
                break;
            case TipoMovimiento.tdown:
                p = new Vector3(t.position.x, t.position.y - _step);
                break;
            case TipoMovimiento.tleft:
                p = new Vector3(t.position.x - _step, t.position.y);
                break;
            case TipoMovimiento.tright:
                p = new Vector3(t.position.x + _step, t.position.y);
                break;
            case TipoMovimiento.tpush:
                p = new Vector3(t.position.x, t.position.y + _step);
                break;
        }
        t.position = p;
    }
}

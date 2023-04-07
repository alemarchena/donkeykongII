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
    IntentMovement m;

    private void Awake()
    {
        t = GetComponent<Transform>();
        m = GetComponent<IntentMovement>();
    }



    void Update()
    {
        try
        {
            if( Input.GetKeyDown( _up )  )
            {
                m.Move(t,IntentMovement.TipoMovimiento.tup);
            }
            if (Input.GetKeyDown(_down))
            {
                m.Move(t, IntentMovement.TipoMovimiento.tdown);
            }
            if (Input.GetKeyDown(_left))
            {
                m.Move(t, IntentMovement.TipoMovimiento.tleft);
            }
            if (Input.GetKeyDown(_right))
            {
                m.Move(t, IntentMovement.TipoMovimiento.tright);
            }
            if (Input.GetKeyDown(_jump))
            {
                m.Move(t, IntentMovement.TipoMovimiento.tpush);
            }
        }
        catch  {
            Debug.LogError("Hay un error en el código");
        }
    }
        
}

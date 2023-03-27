using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    [Header("Rotación")]
    [SerializeField] bool rotate;
    [SerializeField] float angle;


    void Update()
    {
        if(rotate)
            RotatingBounce();
    }

    private void RotatingBounce() {
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle * Time.deltaTime );
        transform.rotation = rotation;
    }
}

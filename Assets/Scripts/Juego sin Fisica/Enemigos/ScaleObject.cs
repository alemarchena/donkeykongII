using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    [Header("Escala")]
    [SerializeField] bool scale;
    [SerializeField] float min;
    [SerializeField] float max;
    [SerializeField] float tiempo;


    Vector3 vectorMax;
    private void Start()
    {
        vectorMax = new Vector3(max, max, max);
    }
    private void Update()
    {
        if (scale)
            ScalingBounce();
    }

    private void ScalingBounce()
    {

        if (transform.localScale == vectorMax)
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(min, min, min), tiempo);
        else
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(max, max, max), tiempo);

    }
}

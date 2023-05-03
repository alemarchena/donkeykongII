using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerKeyPoint : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] int pointForOpenLock;
    [SerializeField] int pointForCapturedKey;

    public bool IsAddingPoint { get; private set; }

    private void Awake()
    {
        if (!playerData) Debug.LogError("Falta el componente PlayerData");
        IsAddingPoint = false;
    }

    public void OpenLock()
    {
        StartCoroutine(AddPointKeyCaptureCoro());
    }

    public void CapturedKey()
    {
        for (int i = 0; i < pointForCapturedKey; ++i)
            playerData.AddPoint();
    }
    IEnumerator AddPointKeyCaptureCoro()
    {
        IsAddingPoint = true;
        for (int i = 0; i < pointForOpenLock; ++i)
        {
            yield return new WaitForSeconds(0.3f);
            playerData.AddPoint();
        }
        IsAddingPoint = false;
    }

}

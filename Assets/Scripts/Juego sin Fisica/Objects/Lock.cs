using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField] List<GameObject> uiListLockGameObjects = new();

    public List<GameObject> UIListLock
    {
        get {return uiListLockGameObjects; }
    }

    public void DeactiveLock(int index)
    {
        uiListLockGameObjects[index].SetActive(false);
    }

    public void ActivateLocks()
    {
        foreach (GameObject gameObject in uiListLockGameObjects)
        {
            gameObject.SetActive(true);
        }
    }

}

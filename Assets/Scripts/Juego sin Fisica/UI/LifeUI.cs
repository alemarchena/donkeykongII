
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    [Tooltip("Es el sprite que muestra la cantidad de vidas en el juego")]
    [SerializeField] GameObject prefabSpritelife;
    [Tooltip("Es el contenedor que tendra como hijo al sprite que muestra la cantidad de vidas en el juego")]
    [SerializeField] Transform content;

    private float stepPositionPrefab = 0.5f;
    private Player player;
    private int initialLife;
    private List<GameObject> listGameObjectsPrefab = new List<GameObject>();
    private bool canCreatePrefab = false;
    private void Start()
    {

        player = FindObjectOfType<Player>();

        try
        {
            initialLife = player.Life;
            ResetSpriteLifes();
        }
        catch
        {
            Debug.LogError("El componente Player no se encuentra en el juego");
        }
    }

    void Update()
    {
        try
        {
            if(player.Life != listGameObjectsPrefab.Count && canCreatePrefab)
            {
                canCreatePrefab = false;
                ResetSpriteLifes();
            }
        }
        catch
        {
            if (!player)
                Debug.LogError("El componente Player no se encuentra en el juego");

        }
    }
    public void ResetSpriteLifes()
    {
        foreach(GameObject go in listGameObjectsPrefab)
        {
            DestroyImmediate(go);
        }

        try
        {
            for (int a = 1; a <= player.Life; a++)
            {
                Vector3 newPosition = new Vector3(content.position.x + a * stepPositionPrefab, content.position.y, content.position.z);
                GameObject gaob = Instantiate(prefabSpritelife, newPosition, Quaternion.identity, content) as GameObject;
                listGameObjectsPrefab.Add(gaob);
            }
            StartCoroutine(CanCreate());
        }
        catch
        {
            if(!content)
                Debug.LogError("Falta asignar el contenedor del prefab Sprite no se encuentra asignado al script LifeUI"); ;

            if(!prefabSpritelife)
                Debug.LogError("Falta asignar el prefab del Sprite de vida al script LifeUI"); ;

        }
    }

    IEnumerator CanCreate()
    {
        yield return new WaitForSeconds(0.5f);
        canCreatePrefab = true;
    }

}

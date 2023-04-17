
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int lifes;
    [SerializeField] private int points;
    [SerializeField] int initialLife = 3;
    public bool ItsAlive { get; private set; }
    public bool Win {get;private set;}

    public void Winner()
    {
        Win = true;
    }

    public int Life
    {
        get { return lifes; }
    }

    public int Points
    {
        get { return points; }
    }

    [Space]
    [Tooltip("Es el sprite que muestra la cantidad de vidas en el juego")]
    [SerializeField] GameObject prefabSpritelife;
    [Tooltip("Es el contenedor que tendra como hijo al sprite que muestra la cantidad de vidas en el juego")]
    [SerializeField] Transform content;
    private float stepPositionPrefab = 0.5f;
    private List<GameObject> listGameObjectsPrefab = new();

    private SpriteRenderer sp;
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ResetLifes();
    }
    private void Update()
    {
        if(lifes<=0 && ItsAlive)
        {
            Died();
        }
    }

    public void Died()
    {
        ItsAlive = false;
        try
        {
            sp.enabled = false;
        }
        catch
        {
            Debug.LogError("Falta el componente SpriteRenderer del Player");
        }
    }

    public void Play()
    {
        ResetLifes();
        CreateUIlife();
        Win = false;
        ItsAlive = true;
        try
        {
            sp.enabled=true;
        }
        catch
        {
            Debug.LogError("Falta el componente SpriteRenderer del Player");
        }
    }
    public void Revived()
    {
        CreateUIlife();
        if(lifes>0)
        {
            ItsAlive = true;
            try
            {
               sp.enabled =true;
            }
            catch
            {
                Debug.LogError("Falta el componente SpriteRenderer del Player");
            }
        }
    }

    private void CreateUIlife()
    {
        foreach (GameObject go in listGameObjectsPrefab)
        {
            DestroyImmediate(go);
        }
        listGameObjectsPrefab.Clear();
        for (int a = 1; a < Life; a++)
        {
            Vector3 newPosition = new Vector3(content.position.x + a * stepPositionPrefab, content.position.y, content.position.z);
            GameObject gaob = Instantiate(prefabSpritelife, newPosition, Quaternion.identity, content) as GameObject;
            listGameObjectsPrefab.Add(gaob);
        }
    }
  

    public void DiscountLife()
    {
        lifes -= 1;
    }
    public void AddPoint()
    {
        points+=1;
    }

    public void ResetLifes()
    {
        lifes = initialLife;
    }


}

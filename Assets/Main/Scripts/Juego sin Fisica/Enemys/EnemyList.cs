using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    [SerializeField] Configuration configuration;
    [SerializeField] List<GameObject> enemiesList = new List<GameObject>();

   
    public void ReInit()
    {
        foreach (GameObject g in enemiesList)
        {
            g.SetActive(false);
        }
        configuration.IncrementCicle();

        for (int a = 0;a< enemiesList.Count; a++)
        {
            if(a<=configuration.cicle)
                enemiesList[a].SetActive(true);
        }

        if (configuration.cicle >= enemiesList.Count)
        {
            System.Random rnd = new();

            for (int i = 0; i < enemiesList.Count; i++)
            {
                int j = rnd.Next(i, enemiesList.Count);
                GameObject temp = enemiesList[i];
                enemiesList[i] = enemiesList[j];
                enemiesList[j] = temp;
            }

            int aleatoryPosition = Random.Range(0,enemiesList.Count);
            for (int b = 0; b < enemiesList.Count; b++)
            {
                if (b == aleatoryPosition)
                    enemiesList[b].SetActive(false);
            }
            configuration.cicle = enemiesList.Count;
        }
    }
}

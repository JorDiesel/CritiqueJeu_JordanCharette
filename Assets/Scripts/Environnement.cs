using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environnement : MonoBehaviour
{
    [Header("Positionnement")]
    public Vector3 debut;
    public Vector3 fin;

    [Header("Plancher")]
    public GameObject prototypePlancher;
    public float taillePlanche = 7.5f;

    [Header("Table de jeu")]
    public GameObject prototypeTable;

    void Start()
    {
        // Validation que debut<fin
        if (debut.x > fin.x)
        {
            debut += fin;
            fin = debut - fin;
            debut -= fin;
        }

        GenererPlancher();
        GenererTable();
    }

    private void GenererPlancher()
    {
        float nombreSegments = (fin.x - debut.x) / taillePlanche;

        for (int i = 0; i < nombreSegments; i++)
        {
            CreerPlanche(PositionPlanche(i));
        }
    }

    private void GenererTable()
    {
        Instantiate(prototypeTable, transform);
    }

    private void CreerPlanche(Vector3 position)
    {
        GameObject segmentPlanche = Instantiate(prototypePlancher, transform);
        segmentPlanche.transform.position = position;
    }

    private Vector3 PositionPlanche(int indicePlanche)
    {
        return debut + taillePlanche * indicePlanche * Vector3.right;
    }
}

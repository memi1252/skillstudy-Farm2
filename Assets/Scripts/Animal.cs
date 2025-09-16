using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public Tile tile;

    private int index;
    private Transform target;

    private void Awake()
    {
        target = Camera.main.transform;
    }


    private void Update()
    {
        if (tile != null)
        {
            if (Vector3.Distance(transform.position, tile.animalMovePoints[index].position) > 0.1)
            {
                transform.position = Vector3.MoveTowards(transform.position, tile.animalMovePoints[index].position, 1.0f * Time.deltaTime);
            }
            else
            {
                index = Random.Range(0, tile.animalMovePoints.Length);
            }
        }
    }

    public void Set(Tile tile)
    {
        this.tile = tile;
        index = Random.Range(0, tile.animalMovePoints.Length);
    }
}

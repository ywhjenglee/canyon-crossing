using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject tree = null;

    // Dynamically spawn trees at random positions in its given grid
    public void SpawnTrees(int n)
    {
        // First set of trees
        if (n == 1)
        {
            for (int i = 0; i < 8; i++)
            {
                var newTree = Instantiate(tree, transform);
                newTree.transform.position = transform.position + new Vector3(7*i - 24.5f + Random.Range(-3f, 3f), 0, -10 + Random.Range(-4f, 4f));
            }
        }
        // Second set of trees
        else if (n == 2)
        {
            for (int i = 0; i < 8; i++)
            {
                var newTree = Instantiate(tree, transform);
                newTree.transform.position = transform.position + new Vector3(7*i - 24.5f + Random.Range(-3f, 3f) , 0, Random.Range(-4f, 4f));
            }
        }
        // Last set of trees
        else
        {
            for (int i = 0; i < 8; i++)
            {
                var newTree = Instantiate(tree, transform);
                newTree.transform.position = transform.position + new Vector3(7*i - 24.5f + Random.Range(-3f, 3f) , 0, 10 + Random.Range(-4f, 4f));
            }

        }
    }
}

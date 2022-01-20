using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    private bool collided;

    // Destroy projectile after 3 seconds
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Destroy projectile on collision
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player" && !collided)
        {
            // If projectile hits a tree generate a row of the maze
            if (collision.gameObject.tag == "Tree")
            {
                MazeScript instanceofM = GameObject.Find("Bridge-Maze").GetComponent<MazeScript>();
                instanceofM.GenerateRow();
                Destroy(collision.gameObject);
            }
            collided = true;
            Destroy(gameObject);
        }
    }
}

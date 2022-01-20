using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZoneDetection : MonoBehaviour
{

    [SerializeField] CharacterController playerBody = null;
    private bool completedMaze = false;
    private bool zone1 = false;
    private bool zone2 = false;
    private bool zone3 = false;

    // Trigger events when player enters a specified zone
    public void OnTriggerEnter(Collider collider)
    {
        TreeSpawner trees = GameObject.Find("Tree Zone").GetComponent<TreeSpawner>();
        switch (collider.gameObject.name)
        {
            case "Top Zone":
                MazeScript maze = GameObject.Find("Bridge-Maze").GetComponent<MazeScript>();
                if (!maze.getComplete() || completedMaze)
                {
                    SceneManager.LoadScene("GameOver");
                }
                break;
            case "Canyon Bottom":

                SceneManager.LoadScene("GameOver");
                break;
            case "End Zone":
                completedMaze = true;
                playerBody.Move(new Vector3(0, 15, 5));
                break;
            case "Goal Zone":
                SceneManager.LoadScene("Win");
                break;
            case "Zone1":
                if (!zone1)
                {
                    trees.SpawnTrees(1);
                    zone1 = true;
                }
                break;
            case "Zone2":
                if (!zone2)
                {
                    trees.SpawnTrees(2);
                    zone2 = true;
                }
                break;
            case "Zone3":
                if (!zone3)
                {
                    trees.SpawnTrees(3);
                    zone3 = true;
                }
                break;
        }
    }
}

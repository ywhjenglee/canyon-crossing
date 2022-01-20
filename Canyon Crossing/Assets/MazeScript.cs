/*
Eller's algorithm was used to generate the maze. The current problem with my implementation is
that the maze is somewhat vertically bias. This is due to the way opening bottom walls was implemented.
Instead of choosing at least one, I iterate through each element in a set (deciding to remove or keep).
If on the last element no walls have been removed, we must remove it. If a wall has been removed, I
still flip a coin to decide whether I remove it or not. This in consequence increases the chance for
the last element of the set to be removed. Therefore the last column of the maze is generally very open.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeScript : MonoBehaviour
{

    [SerializeField] private GameObject wall = null;
    private Vector3 mazePosition;
    private int[] currentRow = new int[6];
    private int setCounter = 1;
    private int rowCounter = 0;
    private bool isComplete = false;

    // Get position of the maze and offset it to align walls to the ground
    void Start()
    {
        mazePosition = gameObject.transform.position + new Vector3(0, 1.5f, 0);
    }

    // Create and display a row of the maze
    public void GenerateRow()
    {
        // Stop generating rows when limit is reached
        if (rowCounter > 14)
        {
            return;
        }

        // Assign a set to any remaining cell
        for (int i = 0; i < 6; i++)
        {
            if (currentRow[i] == 0)
            {
                currentRow[i] = setCounter;
                setCounter++;
            }
        }

        // Render seperator wall if part of same set
        for (int i = 0; i < 5; i++)
        {
            if (currentRow[i] == currentRow[i+1])
            {
                RenderSeperatorWall(i, rowCounter);
            }
        }
        // Render seperator wall if we don't decide to merge different adjacent sets
        for (int i = 0; i < 5; i++)
        {
            if (rowCounter == 14)
            {
                if (currentRow[i] != currentRow[i+1])
                {
                    currentRow[i+1] = currentRow[i];
                }
                else
                {
                    RenderSeperatorWall(i, rowCounter);
                }
            }
            else
            {
                if (currentRow[i] != currentRow[i+1] && Random.value < 0.5f)
                {
                    currentRow[i+1] = currentRow[i];
                }
                else
                {
                    RenderSeperatorWall(i, rowCounter);
                }
            }
        }

        // Return if on last row and mark maze as complete
        if (rowCounter == 14)
        {
            isComplete = true;
            rowCounter++;
            return;
        }

        // Keep count of number of elements in each set for current row
        var sets = new Dictionary<int, int>();
        for (int i = 0; i < 6; i++)
        {
            if (sets.ContainsKey(currentRow[i]))
            {
                sets[currentRow[i]] += 1;
            }
            else
            {
                sets.Add(currentRow[i], 1);
            }
        }

        // Create vertical connections (at least one for each set on the row)
        int[] nextRow = new int[6];
        for (int i = 0; i < 6; i++)
        {
            if (sets[currentRow[i]] > 1)
            {
                if (Random.value < 0.5f)
                {
                    nextRow[i] = currentRow[i];
                }
                else
                {
                    sets[currentRow[i]] -= 1;
                    RenderBottomWall(i, rowCounter);
                }
            }
            else
            {
                nextRow[i] = currentRow[i];
            }
        }
        currentRow = nextRow;
        rowCounter++;
    }

    // Render the wall to seperate sets within a row
    private void RenderSeperatorWall(int i, int n)
    {
        var seperatorWall = Instantiate(wall, transform);
        seperatorWall.transform.position = mazePosition + new Vector3(-9+3*i+3 , 0, -22.5f+3*n+1.5f);
    }

    // Render the walls to seperate each row
    private void RenderBottomWall(int i, int n)
    {
        var bottomWall = Instantiate(wall, transform);
        bottomWall.transform.position = mazePosition + new Vector3(-9+3*i+1.5f , 0, -22.5f+3*n+3);
        bottomWall.transform.eulerAngles = new Vector3(0, 90, 0);
    }

    // Getter to check if maze is complete
    public bool getComplete()
    {
        return isComplete;
    }
}

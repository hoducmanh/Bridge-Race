using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GridSpawner : MonoBehaviour
{
    public int gridX;
    public int gridZ;
    private int randomizeBrick;
    public float gridSpacingOffset;
    public Vector3 gridOrigin;
    public bool isSpawnOnFloor2, isSpawnOnFloor3;
    void Start()
    {
        SpawnGrid(30);
    }

    public void SpawnOnSecondFloor(Vector3 spawnPos, int maxBrick)
    {
        if (!isSpawnOnFloor2)
        {
            gridOrigin = spawnPos;
            for (int x = 0; x < gridX; x++)
            {
                for (int z = 0; z < gridZ; z++)
                {
                    Vector3 spawnPosition = new Vector3(x * gridSpacingOffset, 0, z * gridSpacingOffset) + gridOrigin;
                    PickAndSpawn(spawnPosition, Quaternion.identity, BrickPooler.Instance, maxBrick);
                }
            }
            isSpawnOnFloor2 = true;
        }
    }
    public void SpawnOnThirdFloor(Vector3 spawnPos, int maxBrick)
    {
        if (!isSpawnOnFloor3)
        {
            gridOrigin = spawnPos;
            for (int x = 0; x < gridX; x++)
            {
                for (int z = 0; z < gridZ; z++)
                {
                    Vector3 spawnPosition = new Vector3(x * gridSpacingOffset, 0, z * gridSpacingOffset) + gridOrigin;
                    PickAndSpawn(spawnPosition, Quaternion.identity, BrickPooler.Instance, maxBrick);
                }
            }
            isSpawnOnFloor3 = true;
        }
    }
    void SpawnGrid(int maxBrick)
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                Vector3 spawnPosition = new Vector3(x * gridSpacingOffset, 0, z * gridSpacingOffset) + gridOrigin;
                PickAndSpawn(spawnPosition, Quaternion.identity, BrickPooler.Instance, maxBrick);
            }
        }
    }

    private void PickAndSpawn(Vector3 spawnPosition, Quaternion spawnRotation, BrickPooler objPool, int maxBrick)
    {
        randomizeBrick = Random.Range(1, 4);
        SpawnRandomizeBrick(spawnPosition, maxBrick);
    }

    private void SpawnRandomizeBrick(Vector3 spawnPosition, int maxBrick)
    {
        if (randomizeBrick == 1)
            RandomSpawnCondition(Value.BLUE_BRICK, spawnPosition, maxBrick, 1);
        else if (randomizeBrick == 2)
            RandomSpawnCondition(Value.RED_BRICK, spawnPosition, maxBrick, 2);
        else if (randomizeBrick == 3)
            RandomSpawnCondition(Value.GREEN_BRICK, spawnPosition, maxBrick, 3);
    }
    private void RandomSpawnCondition(string tag, Vector3 spawnPosition, int maxBrick, int brickColorValue)
    {
        if(BrickPooler.Instance.BrickCounter[tag] == maxBrick)
        {
            if(brickColorValue == 3)
            {
                randomizeBrick = 1;
                SpawnRandomizeBrick(spawnPosition, maxBrick);
            }
            else 
                randomizeBrick++;
        }

        else
            BrickPooler.Instance.SpawnFromPool(tag, spawnPosition, Quaternion.identity);
    }
}

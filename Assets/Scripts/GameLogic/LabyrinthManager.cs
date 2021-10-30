using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

public class LabyrinthManager : MonoBehaviour
{
    [SerializeField]
    [Range(1,100)]
    private int width = 10;
    [SerializeField]
    [Range(1, 100)]
    private int height = 10;

    [SerializeField]
    private Transform wall = null;
    [SerializeField]
    private float cellSize = 1;


    private void Start()
    {
        var labyrinth = LabyrinthGenerator.Generate(width, height);
        Initialize(labyrinth);
    }
    
    private void Initialize(WallType[,] labyrinth)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var cell = labyrinth[i, j];
                var position = new Vector3(-width / 2 + i * cellSize, 0, -height / 2 + j * cellSize);

                if (cell.HasFlag(WallType.Up))
                {
                    var upWall = Instantiate(wall, transform) as Transform;
                    upWall.position = position;
                    upWall.localScale = new Vector3(cellSize, upWall.localScale.y, upWall.localScale.z);
                }

                if (cell.HasFlag(WallType.Left))
                {
                    var leftWall = Instantiate(wall, transform) as Transform;
                    leftWall.position = position + new Vector3(-cellSize / 2, 0, -cellSize / 2);
                    leftWall.localScale = new Vector3(cellSize, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                }

                if (i == width - 1)
                {
                    if (cell.HasFlag(WallType.Right))
                    {
                        var rightWall = Instantiate(wall, transform) as Transform;
                        rightWall.position = position + new Vector3(cellSize / 2, 0, -cellSize / 2);
                        rightWall.localScale = new Vector3(cellSize, rightWall.localScale.y, rightWall.localScale.z);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                    }
                }

                if (j == 0)
                {
                    if (cell.HasFlag(WallType.Down))
                    {
                        var downWall = Instantiate(wall, transform) as Transform;
                        downWall.position = position + new Vector3(0, 0, -cellSize);
                        downWall.localScale = new Vector3(cellSize, downWall.localScale.y, downWall.localScale.z);
                    }
                }
            }
        }

        NavMeshBuilder.ClearAllNavMeshes();
        NavMeshBuilder.BuildNavMesh();
    }
}

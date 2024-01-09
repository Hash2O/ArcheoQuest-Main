using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
//using Unity.AI.Navigation;  //(3)

//(2) How to use a Seed Value to Recreate Procedurally Generated level
//URL : https://www.youtube.com/watch?v=ifedjvt0frg&list=PLx7AKmQhxJFYQqS28_gWgzjF7Jdr4yoEQ&index=2

//(3) Navigation in a procedurally Generated World 
// URL : https://www.youtube.com/watch?v=dOI-N2QVly8&list=PLx7AKmQhxJFYQqS28_gWgzjF7Jdr4yoEQ&index=3

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private MazeCell _mazeCellPrefab;

    [SerializeField]
    private int _mazeWidth;

    [SerializeField]
    private int _mazeDepth;

    //(2)
    [SerializeField]
    private int _seed;

    [SerializeField]
    private bool _useSeed;

    private MazeCell[,] _mazeGrid;

    // Start is called before the first frame update
    void Start()
    {
        if(_useSeed)
        {
            Random.InitState(_seed);
        }
        else
        {
            int randomSeed = Random.Range(1, 1000000);
            Random.InitState(randomSeed);

            //making the new seed value visible to be used again later
            Debug.Log("Seed Value : " + randomSeed);
        }

        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];

        for( int x = 0; x < _mazeWidth; x++ )
        {
            for( int z = 0; z < _mazeDepth; z++ )
            {
                _mazeGrid[x, z] = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity, transform); //parent's transform to make the maze easily scalable
                _mazeGrid[x, z].transform.localPosition = new Vector3(x, 0, z);                                                                                                    //
            }
        }

        GenerateMaze(null, _mazeGrid[0, 0]);

        //Building the navmesh after the building of the maze
        //GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();

        ClearWalls(previousCell, currentCell);

        MazeCell nextCell;
        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);

        
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);    

        return unvisitedCells.OrderBy(_ => Random.Range(0, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)currentCell.transform.localPosition.x;
        int z = (int)currentCell.transform.localPosition.z;

        if(x + 1 < _mazeWidth)
        {
            var CellToRight = _mazeGrid[x + 1, z];

            if(CellToRight.IsVisited == false)
            {
                yield return CellToRight;  
            }
        }

        if (x - 1 >= 0)
        {
            var CellToLeft = _mazeGrid[x - 1, z];

            if (CellToLeft.IsVisited == false)
            {
                yield return CellToLeft;
            }
        }

        if (z + 1 < _mazeDepth)
        {
            var CellToFront = _mazeGrid[x, z + 1];

            if (CellToFront.IsVisited == false)
            {
                yield return CellToFront;
            }
        }

        if (z - 1 >= 0)
        {
            var CellToBack = _mazeGrid[x, z - 1];

            if (CellToBack.IsVisited == false)
            {
                yield return CellToBack;
            }
        }
    }

    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if(previousCell == null )
        {
            return;
        }
        
        if(previousCell.transform.localPosition.x < currentCell.transform.localPosition.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if(previousCell.transform.localPosition.x > currentCell.transform.localPosition.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.localPosition.z < currentCell.transform.localPosition.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        if (previousCell.transform.localPosition.z > currentCell.transform.localPosition.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    // Singleton
    public static GridSystemVisual Instance { get; private set; }

    // Member Variables
    [Serializable]
    public struct GridVisualTypeMaterial
    {
        public GridVisualType gridVisualType;
        public Material material;
    }
    public enum GridVisualType
    {
        White,
        Green,
        Blue,
        Red,
    }
    [SerializeField] private Transform gridSystemVisualSinglePrefab;
    [SerializeField] private List<GridVisualTypeMaterial> gridVisualTypeMaterialList;
    private GridSystemVisualSingle[,] gridSystemVisualSingleArray;

    // Awake - Start - Update Methods
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError(
                "There's more than one GridSystemVisual! " + transform + " - " + Instance
            );
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        gridSystemVisualSingleArray = new GridSystemVisualSingle[
            BoardGrid.Instance.GetWidth(),
            BoardGrid.Instance.GetHeight()
        ];
        for (int x = 0; x < BoardGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < BoardGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridSystemVisualSingleTransform = GameObject.Instantiate(
                    gridSystemVisualSinglePrefab,
                    BoardGrid.Instance.GetWorldPosition(gridPosition),
                    Quaternion.identity
                );
                gridSystemVisualSingleArray[x, z] =
                    gridSystemVisualSingleTransform.GetComponent<GridSystemVisualSingle>();
            }
        }

        BoardGrid.Instance.OnAnyPieceMovedGridPosition += BoardGrid_OnAnyPieceMovedGridPosition;
        UpdateGridVisual();
    }

    // Event Listeners
    private void BoardGrid_OnAnyPieceMovedGridPosition(object sender, EventArgs e)
    {
        UpdateGridVisual();
    }

    // Class Methods
    private void UpdateGridVisual()
    {
        HideAllGridPositions();

        // ! TODO: update visual based on piece type
        GridVisualType gridVisualType = GridVisualType.White;
        List<GridPosition> blankGridPositionList = new List<GridPosition>();

        ShowGridPositionList(blankGridPositionList, gridVisualType);
    }

    public void HideAllGridPositions()
    {
        for (int x = 0; x < BoardGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < BoardGrid.Instance.GetHeight(); z++)
            {
                gridSystemVisualSingleArray[x, z].Hide();
            }
        }
    }

    public void ShowGridPositionList(List<GridPosition> gridPositionList, GridVisualType gridVisualType)
    {
        foreach (GridPosition gridPosition in gridPositionList)
        {
            Material material = GetGridVisualTypeMaterial(gridVisualType);
            gridSystemVisualSingleArray[gridPosition.x, gridPosition.z].Show(material);
        }
    }

    private Material GetGridVisualTypeMaterial(GridVisualType gridVisualType)
    {
        foreach (GridVisualTypeMaterial gridVisualTypeMaterial in gridVisualTypeMaterialList)
        {
            if (gridVisualTypeMaterial.gridVisualType == gridVisualType)
            {
                return gridVisualTypeMaterial.material;
            }
        }

        Debug.LogError("Couldn't find GridVisualTypeMaterial for " + gridVisualType);
        return null;
    }

    // ! TODO:  edit or i dont need this?
    private void ShowGridPositionRange(GridPosition gridPosition, int range, GridVisualType gridVisualType)
    {
        List<GridPosition> gridPositionList = new List<GridPosition>();

        for (int x = -range; x <= range; x++)
        {
            for (int z = -range; z <= range; z++)
            {
                GridPosition testGridPosition = gridPosition + new GridPosition(x, z);

                if (!BoardGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue; // not a valid grid position
                }

                gridPositionList.Add(testGridPosition);
            }
        }

        ShowGridPositionList(gridPositionList, gridVisualType);
    }

}

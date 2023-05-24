using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardGrid : MonoBehaviour
{
    // Singleton
    public static BoardGrid Instance { get; private set; }

    // Event Handlers
    public event EventHandler OnAnyPieceMovedGridPosition;

    // Member Variables
    private GridSystem<GridObject> gridSystem;
    [SerializeField] private Transform gridDebugObjectPrefab;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;

    // Awake - Start - Update Methods
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one BoardGrid! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        gridSystem = new GridSystem<GridObject>(width, height, cellSize,
            (GridSystem<GridObject> g, GridPosition gridPosition) => new GridObject(g, gridPosition));

        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
    }

    // Class Methods
    public int GetWidth()
    {
        return gridSystem.GetWidth();
    }

    public int GetHeight()
    {
        return gridSystem.GetHeight();
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return gridSystem.GetGridPosition(worldPosition);
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return gridSystem.GetWorldPosition(gridPosition);
    }

    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridSystem.IsValidGridPosition(gridPosition);
    }

    public bool HasAnyPieceOnGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.HadAnyPiece();
    }

    public Piece GetPieceAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetPiece();
    }

    public void PieceMovedGridPosition(Piece piece, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemovePieceAtGridPosition(fromGridPosition, piece);

        AddPieceAtGridPosition(toGridPosition, piece);

        OnAnyPieceMovedGridPosition?.Invoke(this, EventArgs.Empty);
    }

    public void AddPieceAtGridPosition(GridPosition gridPosition, Piece piece)
    {
        gridSystem.GetGridObject(gridPosition).AddPiece(piece);
    }

    public void RemovePieceAtGridPosition(GridPosition gridPosition, Piece piece)
    {
        gridSystem.GetGridObject(gridPosition).RemovePiece(piece);
    }

    public List<Piece> GetPieceListAtGridPosition(GridPosition gridPosition)
    {
        return gridSystem.GetGridObject(gridPosition).GetPieceList();
    }

}

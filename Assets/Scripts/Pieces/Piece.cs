using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Piece : MonoBehaviour
{
    // Member Variables
    [SerializeField] private bool isEnemy;
    private GridPosition gridPosition;

    // Awake - Start - Update Methods
    private void Start()
    {
        gridPosition = BoardGrid.Instance.GetGridPosition(transform.position);
        BoardGrid.Instance.AddPieceAtGridPosition(gridPosition, this);
    }

    private void Update()
    {
        GridPosition newGridPosition = BoardGrid.Instance.GetGridPosition(transform.position);
        if (gridPosition != newGridPosition)
        {
            // Piece changed grid position
            GridPosition oldGridPosition = gridPosition;
            gridPosition = newGridPosition;

            BoardGrid.Instance.PieceMovedGridPosition(this, oldGridPosition, newGridPosition);
        }
    }

    // Class Methods
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    public bool IsEnemy()
    {
        return isEnemy;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Piece : MonoBehaviour
{
    // Constants

    // Event Handler

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

    // Position Getter Methods
    public GridPosition GetGridPosition() => gridPosition;
    public Vector3 GetWorldPosition() => transform.position;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PieceSelectedVisual : MonoBehaviour
{
    // Member Variables
    [SerializeField] private Piece piece;
    private MeshRenderer meshRenderer;

    // Awake - Start - Update Methods
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        PieceMovementSystem.Instance.OnSelectedPieceChanged += PieceMovementSystem_OnSelectedPieceChanged;

        UpdateVisual();
    }


    // Class Methods
    private void PieceMovementSystem_OnSelectedPieceChanged(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (PieceMovementSystem.Instance.GetSelectedPiece() == piece)
        {
            meshRenderer.enabled = true;
        }
        else
        {
            meshRenderer.enabled = false;
        }
    }

}

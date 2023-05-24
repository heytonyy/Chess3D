using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class PieceMovementSystem : MonoBehaviour
{
    // Singleton
    public static PieceMovementSystem Instance { get; private set; }

    // Event Handlers
    public event EventHandler OnSelectedPieceChanged;
    public event EventHandler<bool> OnBusyChanged;

    // Member Variables
    [SerializeField] private Piece selectedPiece;
    [SerializeField] private LayerMask pieceLayerMask;
    private bool isBusy;

    // Awake - Start - Update Methods
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one PieceMovementSystem! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SetSelectedPiece(selectedPiece);
    }

    private void Update()
    {

        // ! add when we have UI
        // if (EventSystem.current.IsPointerOverGameObject())
        // {
        //     return; // don't do anything if the mouse is over a UI element
        // }

        if (TryHandlePieceSelection())
        {
            return; // don't do anything if we're selecting a piece
        }

    }

    // Class Methods
    public Piece GetSelectedPiece()
    {
        return selectedPiece;
    }

    private void SetSelectedPiece(Piece piece)
    {
        selectedPiece = piece;

        OnSelectedPieceChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool IsBusy()
    {
        return isBusy;
    }

    private void SetBusy()
    {
        isBusy = true;

        OnBusyChanged?.Invoke(this, isBusy);
    }

    private void ClearBusy()
    {
        isBusy = false;

        OnBusyChanged?.Invoke(this, isBusy);
    }

    private bool TryHandlePieceSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, pieceLayerMask))
            {
                if (raycastHit.transform.TryGetComponent<Piece>(out Piece piece))
                {
                    if (piece == selectedPiece)
                    {
                        return false; // don't do anything if we're clicking on the same piece
                    }

                    if (piece.IsEnemy())
                    {
                        return false; // don't do anything if we're clicking on an enemy piece
                    }

                    SetSelectedPiece(piece);
                    return true;
                }
            }
        }

        return false;
    }

}

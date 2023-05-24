using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    // Member Variables
    private GridPosition gridPosition;
    private GridSystem<GridObject> gridSystem;
    private List<Piece> pieceList;

    // Constructor
    public GridObject(GridSystem<GridObject> gridSystem, GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
        this.gridSystem = gridSystem;
        pieceList = new List<Piece>();
    }

    public override string ToString()
    {
        string pieceString = "";
        foreach (Piece piece in pieceList)
        {
            pieceString += piece + "\n";
        }

        return gridPosition.ToString() + "\n" + pieceString;
    }

    // Class Methods
    public List<Piece> GetPieceList() => pieceList;
    public void AddPiece(Piece piece) => pieceList.Add(piece);
    public void RemovePiece(Piece piece) => pieceList.Remove(piece);
    public Piece GetPiece()
    {
        if (HadAnyPiece())
        {
            return pieceList[0];
        }
        else
        {
            return null;
        }
    }
    public bool HadAnyPiece() => pieceList.Count > 0;

}

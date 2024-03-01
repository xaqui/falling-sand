using Godot;
using System;

public class Water : Liquid
{
    public Water(Vector2I coordinates) : base(coordinates)
    {
        _elementType = ElementType.WATER;
    }

    public override void step(CellularMatrix matrix)
    {
        Vector2I downCoords = _coordinates + Vector2I.Down;
        Element down = matrix.GetCell(downCoords);
        if (down != null)
        {
            if (down is EmptyCell)
            {
                swapPositions(matrix, down);
                return;
            }
        }
        Vector2I downLeftCoords = Vector2I.Down + Vector2I.Left;
        Element downLeft = matrix.GetCell(_coordinates + downLeftCoords);
        if (downLeft != null)
        {
            if (downLeft is EmptyCell)
            {
                swapPositions(matrix, downLeft);
                return;
            }
        }
        Vector2I downRightCoords = Vector2I.Down + Vector2I.Right;
        Element downRight = matrix.GetCell(_coordinates + downRightCoords);
        if (downRight != null)
        {
            if (downRight is EmptyCell)
            {
                swapPositions(matrix, downRight);
                return;
            }
        }
        Vector2I leftCoords = Vector2I.Left;
        Element left = matrix.GetCell(_coordinates + leftCoords);
        if (left != null)
        {
            if (left is EmptyCell)
            {
                swapPositions(matrix, left);
                return;
            }
        }
        Vector2I rightCoords = Vector2I.Right;
        Element right = matrix.GetCell(_coordinates + rightCoords);
        if (right != null)
        {
            if (right is EmptyCell)
            {
                swapPositions(matrix, right);
                return;
            }
        }



        return;
    }
}

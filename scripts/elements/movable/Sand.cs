using Godot;
using System;

public class Sand : MovableSolid
{
    public Sand(Vector2I coordinates) : base(coordinates)
    {
        _elementType = ElementType.SAND;
    }

    public override void step(CellularMatrix matrix)
    {
        Vector2I downCoords = _coordinates + Vector2I.Down;
        Element down = matrix.GetCell(downCoords);
        if(down != null)
        {
            if(down is EmptyCell || down is Liquid)
            {
                swapPositions(matrix, down);
                return;
            }
        }
        Vector2I downLeftCoords = Vector2I.Down + Vector2I.Left;
        Element left = matrix.GetCell(_coordinates + downLeftCoords);
        if(left != null)
        {
            if(left is EmptyCell || left is Liquid)
            {
                swapPositions(matrix, left);
                return;
            }
        }
        Vector2I downRightCoords = Vector2I.Down + Vector2I.Right;
        Element right = matrix.GetCell(_coordinates + downRightCoords);
        if(right != null)
        {
            if(right is EmptyCell || right is Liquid)
            {
                swapPositions(matrix, right);
                return;
            }
        }

        return;
        //throw new NotImplementedException();
    }



}

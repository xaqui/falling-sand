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
        Element down = matrix.GetCell(_coordinates + Vector2I.Down);
        if(down != null)
        {
            if(down is EmptyCell)
            {
                swapPositions(matrix, down);
                return;
            }
        }
        Element left = matrix.GetCell(_coordinates + Vector2I.Down + Vector2I.Left);
        if(left != null)
        {
            if(left is EmptyCell)
            {
                swapPositions(matrix, left);
                return;
            }
        }
        Element right = matrix.GetCell(_coordinates + Vector2I.Down + Vector2I.Right);
        if(right != null)
        {
            if(right is EmptyCell)
            {
                swapPositions(matrix, right);
                return;
            }
        }

        return;
        //throw new NotImplementedException();
    }



}

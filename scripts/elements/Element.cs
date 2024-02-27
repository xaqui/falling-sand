using Godot;
using System;

public abstract class Element
{
	protected Vector2I _coordinates;
	protected ElementType _elementType = ElementType.NONE;
	public Element(Vector2I coordinates)
	{
		_coordinates = coordinates;
	}

	public ElementType GetElementType() { return _elementType; }

    public void SetCoordinates(Vector2I coordinates)
    {
        _coordinates = coordinates;
    }
	public abstract void step(CellularMatrix matrix);

    public void swapPositions(CellularMatrix matrix, Element toSwap)
    {
        swapPositions(matrix, toSwap, toSwap._coordinates);
    }

    public void swapPositions(CellularMatrix matrix, Element toSwap, Vector2I coordinatesToSwap)
    {
        if (_coordinates.X == toSwap._coordinates.X && _coordinates.Y == toSwap._coordinates.Y)
        {
            return;
        }
        matrix.setElementAtIndex(_coordinates, toSwap);
        matrix.setElementAtIndex(coordinatesToSwap, this);
    }

}

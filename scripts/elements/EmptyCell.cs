using Godot;
using System;

public class EmptyCell : Element
{
    public EmptyCell(Vector2I coordinates) : base(coordinates)
    {
        _coordinates = coordinates;
        _elementType = ElementType.NONE;
    }

    public override void step(CellularMatrix matrix)
    {
        return;
    }
}

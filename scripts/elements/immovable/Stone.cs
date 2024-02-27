using Godot;
using System;

public class Stone : ImmovableSolid
{
    public Stone(Vector2I coordinates) : base(coordinates)
    {
        _elementType = ElementType.STONE;
    }

    public override void step(CellularMatrix matrix)
    {
        return;
    }
}

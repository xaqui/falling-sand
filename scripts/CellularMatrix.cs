using Godot;
using System;

public class CellularMatrix
{
    private Element[,] Matrix;
    private Vector2I _size;

    public CellularMatrix(Vector2I size)
    {
        _size = size;
        Matrix = new Element[size.X,size.Y];
        for (int x = 0; x < size.X; x++)
        {
            for (int y = 0; y < size.Y; y++)
            {
                Matrix[x, y] = new EmptyCell(new Vector2I(x,y));
            }
        }
    }

    public int GetSizeY() { return _size.Y; }
    public int GetSizeX() { return _size.X; }

    public Element GetCell(Vector2I coordinates)
    {
        if (IsValidCoordinates(coordinates))
        {
            return Matrix[coordinates.X,coordinates.Y];
        }
        return null;
    }
    public bool setElementAtIndex(Vector2I coordinates, Element element)
    {
        Matrix[coordinates.X,coordinates.Y] = element;
        element.SetCoordinates(coordinates);
        return true;
    }

    private Element CreateElementBasedOnType(ElementType type, Vector2I coordinates)
    {
        if(type == ElementType.NONE)
        {
            return new EmptyCell(coordinates);
        } else if(type == ElementType.SAND)
        {
            return new Sand(coordinates);
        } else if (type == ElementType.STONE)
        {
            return new Stone(coordinates);
        }
        return new EmptyCell(coordinates);
    }

    public void SetCell(ElementType type, Vector2I coordinates)
    {
        Matrix[coordinates.X,coordinates.Y] = CreateElementBasedOnType(type, coordinates);
    }

    public void StepAll()
    {
        for (int x = _size.X-1; x > 0; x--)
        {
            for (int y = _size.Y-1; y > 0; y--)
            {
                Matrix[x, y].step(this);
            }
        }
    }

    public bool IsValidCoordinates(Vector2I coordinates)
    {
        if(coordinates > _size || coordinates < Vector2I.Zero)
        {
            return false;
        }
        return true;
    }

}

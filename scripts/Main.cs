using Godot;
using Godot.Collections;
using System;

public partial class Main : Node
{
    [Export] private TileMap _TileMap;
    private CellularMatrix _CellularMatrix;

    [Export] Dictionary<ElementType, Vector2I> cellDictionary = new Dictionary<ElementType, Vector2I>();

    public override void _Ready()
    {
        _CellularMatrix = new CellularMatrix(new Vector2I(960/4,540/4));
        CreateFloor();
    }

    private void CreateFloor()
    {
        for (int x = 0; x < _CellularMatrix.GetSizeX(); x++)
        {
            _CellularMatrix.SetCell(ElementType.STONE, new Vector2I(x,_CellularMatrix.GetSizeY()-1 ));
        }
    }

    private void PaintCells()
    {
        if (Input.IsActionPressed("click"))
        {
            Vector2 mousePos = GetViewport().GetMousePosition();
            Vector2I coordinates = new Vector2I((int)(mousePos.X), (int)(mousePos.Y));
            if (_CellularMatrix.IsValidCoordinates(coordinates))
            {
                _CellularMatrix.SetCell(ElementType.SAND, coordinates/4);
            }
        }

    }

    public void _on_timer_timeout()
    {
        PaintCells();
        StepAllAndDraw();
    }

    private void StepAllAndDraw()
    {
        DrawCells();
        StepAll();
    }

    private void StepAll()
    {
        _CellularMatrix.StepAll();
    }

    private Vector2I? LookupTile(Element element)
    {
        ElementType type = element.GetElementType();
        if (type == ElementType.NONE) return null;
        Vector2I atlasCoords;
        cellDictionary.TryGetValue(type, out atlasCoords);
        //GD.Print(type +", " +atlasCoords);
        return atlasCoords;
    }

    private void DrawCells()
    {
        for (int x = 0; x < _CellularMatrix.GetSizeX(); x++)
        {
            for (int y = 0; y < _CellularMatrix.GetSizeY(); y++)
            {
                var coords = new Vector2I(x, y);
                Element cell = _CellularMatrix.GetCell(coords);
                _TileMap.SetCell(0, coords, 0, LookupTile(cell));
            }
        }
    }

    public override void _Process(double delta)
    {

    }
}

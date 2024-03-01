using Godot;
using Godot.Collections;
using System;

public partial class Main : Node
{
    [Export] private TileMap _TileMap;
    private CellularMatrix _CellularMatrix;
    private int selectedType = 0;
    private const int MAX_TYPE = 1;

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

    private void PaintSize(Vector2I coordinates, int size, ElementType type)
    {
        for(int x = coordinates.X;x < coordinates.X+size;x++)
        {
            for(int y = coordinates.Y; y < coordinates.Y+size; y++)
            {
                PaintSelected(new Vector2I(x,y), type);
            }
        }
    }

    private void PaintSelected(Vector2I coordinates, ElementType type) {
        if (_CellularMatrix.IsValidCoordinates(coordinates))
        {
            _CellularMatrix.SetCell(type, coordinates);
        }
    }

    private void PaintCells()
    {
        if (Input.IsActionPressed("left_click"))
        {
            Vector2 mousePos = GetViewport().GetMousePosition();
            Vector2I coordinates = new Vector2I((int)(mousePos.X), (int)(mousePos.Y));
            //GD.Print(coordinates / 4);
            ElementType type = ElementType.NONE;
            if (selectedType == 0) { type = ElementType.SAND; }
            else if (selectedType == 1) { type = ElementType.WATER; }
            PaintSize(coordinates/4, 4, type);
        } else if (Input.IsActionPressed("right_click"))
        {
            Vector2 mousePos = GetViewport().GetMousePosition();
            Vector2I coordinates = new Vector2I((int) (mousePos.X), (int) (mousePos.Y));
            //GD.Print(coordinates / 4);
            if (_CellularMatrix.IsValidCoordinates(coordinates / 4))
            {
                PaintSize(coordinates / 4, 4, ElementType.STONE);
            }
        }

    }

    private void cycleSelection()
    {
        if(selectedType < MAX_TYPE)
        {
            selectedType++;
        } else
        {
            selectedType = 0;
        }
    }

    private void ProcessInput()
    {
        if (Input.IsActionJustPressed("key_one"))
        {
            cycleSelection();
        }
    }

    public void _on_timer_timeout()
    {
        ProcessInput();
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

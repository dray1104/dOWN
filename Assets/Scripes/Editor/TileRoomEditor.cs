using UnityEngine;
using UnityEditor;

public class TileRoomEditor : EditorWindow
{
    private TileRoomTemplate template;

    private TileType selectedTile = TileType.Floor;

    private const int CellSize = 20;

    [MenuItem("Tools/Room Editor")]
    public static void Open()
    {
        GetWindow<TileRoomEditor>("Room Editor");
    }

    private void OnGUI()
    {
        GUILayout.Space(10);

        template = (TileRoomTemplate)
            EditorGUILayout.ObjectField(
                "Template",
                template,
                typeof(TileRoomTemplate),
                false);

        if (template == null)
            return;

        GUILayout.Space(10);

        selectedTile =
            (TileType)EditorGUILayout.EnumPopup(
                "Brush",
                selectedTile);

        GUILayout.Space(10);

        if (template.tiles == null ||
            template.tiles.Length !=
            template.width * template.height)
        {
            if (GUILayout.Button("Initialize Room"))
            {
                template.Init();

                EditorUtility.SetDirty(template);
            }

            return;
        }

        DrawGrid();
    }

    private void DrawGrid()
    {
        for (int y = template.height - 1; y >= 0; y--)
        {
            GUILayout.BeginHorizontal();

            for (int x = 0; x < template.width; x++)
            {
                TileType tile =
                    template.GetTile(x, y);

                GUI.backgroundColor =
                    GetColor(tile);

                Rect rect = GUILayoutUtility.GetRect(
                    CellSize,
                    CellSize,
                    GUILayout.Width(CellSize),
                    GUILayout.Height(CellSize));

                EditorGUI.DrawRect(rect, GetColor(tile));

                Event e = Event.current;

                if ((e.type == EventType.MouseDown ||
                    e.type == EventType.MouseDrag) &&
                    e.button == 0 &&
                    rect.Contains(e.mousePosition))
                {
                    template.SetTile(x, y, selectedTile);

                    EditorUtility.SetDirty(template);

                    Repaint();
                }
                if ((e.type == EventType.MouseDown ||
                    e.type == EventType.MouseDrag) &&
                    e.button == 1 &&
                    rect.Contains(e.mousePosition))
                {
                    template.SetTile(x, y, TileType.Empty);

                    EditorUtility.SetDirty(template);

                    Repaint();
                }
            }

            GUILayout.EndHorizontal();
        }

        GUI.backgroundColor = Color.white;
    }

    private Color GetColor(TileType tile)
    {
        switch (tile)
        {
            case TileType.Floor:
                return Color.green;

            case TileType.Wall:
                return Color.gray;

            case TileType.EnemySpawn:
                return Color.red;

            case TileType.Obstacle:
                return Color.yellow;
            case TileType.DoorUp:
                return Color.cyan;
            case TileType.DoorDown:
                return Color.cyan;
            case TileType.DoorLeft:
                return Color.cyan;
            case TileType.DoorRight:
                return Color.cyan;

            default:
                return Color.black;
        }
    }
}
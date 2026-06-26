using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [Header("Room Templates")]
    public TileRoomTemplate startRoom;
    public TileRoomTemplate enemyRoom;
    public TileRoomTemplate shopRoom;
    public TileRoomTemplate itemRoom;
    public TileRoomTemplate bossRoom;

    [Header("Tile Set")]
    public TileSet tileSet;

    [Header("Room Settings")]
    public float roomSpacing = 30f;

    private Dictionary<Vector2Int, TileRoomTemplate> dungeonRooms =
        new Dictionary<Vector2Int, TileRoomTemplate>();

    void Start()
    {
        GenerateDungeon();
        SpawnDungeon();
    }

   void GenerateDungeon()
{
    dungeonRooms.Clear();

    List<Vector2Int> path = new List<Vector2Int>();

    Vector2Int current = Vector2Int.zero;

    path.Add(current);

    dungeonRooms[current] = startRoom;

    int enemyRoomCount = 8;

    for (int i = 0; i < enemyRoomCount; i++)
    {
        Vector2Int next = current + RandomDirection();

        while (dungeonRooms.ContainsKey(next))
        {
            next = current + RandomDirection();
        }

        dungeonRooms[next] = enemyRoom;

        path.Add(next);

        current = next;
    }

    // Last room becomes boss
    dungeonRooms[current] = bossRoom;

    CreateSpecialRooms(path);
}
Vector2Int RandomDirection()
{
    Vector2Int[] dirs =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    return dirs[Random.Range(0, dirs.Length)];
}
void CreateSpecialRooms(List<Vector2Int> path)
{
    List<Vector2Int> candidates = new List<Vector2Int>();

    foreach (Vector2Int room in path)
    {
        if (room == Vector2Int.zero)
            continue;

        int neighbors = CountNeighbors(room);

        if (neighbors == 1)
            candidates.Add(room);
    }

    if (candidates.Count > 0)
    {
        Vector2Int itemPos =
            candidates[Random.Range(0, candidates.Count)];

        dungeonRooms[itemPos] = itemRoom;

        candidates.Remove(itemPos);
    }

    if (candidates.Count > 0)
    {
        Vector2Int shopPos =
            candidates[Random.Range(0, candidates.Count)];

        dungeonRooms[shopPos] = shopRoom;
    }
}
int CountNeighbors(Vector2Int pos)
{
    int count = 0;

    if (dungeonRooms.ContainsKey(pos + Vector2Int.up))
        count++;

    if (dungeonRooms.ContainsKey(pos + Vector2Int.down))
        count++;

    if (dungeonRooms.ContainsKey(pos + Vector2Int.left))
        count++;

    if (dungeonRooms.ContainsKey(pos + Vector2Int.right))
        count++;

    return count;
}

    void SpawnDungeon()
    {
        foreach (var room in dungeonRooms)
        {
            Vector2Int gridPos = room.Key;
            TileRoomTemplate template = room.Value;

            Vector3 worldPos = new Vector3(
                gridPos.x * roomSpacing,
                0,
                gridPos.y * roomSpacing
            );

            GameObject roomObj =
                new GameObject($"Room_{gridPos.x}_{gridPos.y}");

            roomObj.transform.position = worldPos;
            RoomController roomController =
                roomObj.AddComponent<RoomController>();
                switch (template)
                {
                        case var t when t == startRoom:
                            roomController.isStartRoom = true;
                            break;

                        case var t when t == bossRoom:
                            roomController.isBossRoom = true;
                            break;

                        case var t when t == shopRoom:
                            roomController.isShopRoom = true;
                            break;

                        case var t when t == itemRoom:
                            roomController.isItemRoom = true;
                            break;
                }

            roomController.roomGridPosition = gridPos;
            TileRoomBuilder builder =
                roomObj.AddComponent<TileRoomBuilder>();

            builder.tileSet = tileSet;
            bool hasUp =
                dungeonRooms.ContainsKey(
                    gridPos + Vector2Int.up);

            bool hasDown =
                dungeonRooms.ContainsKey(
                    gridPos + Vector2Int.down);

            bool hasLeft =
                dungeonRooms.ContainsKey(
                    gridPos + Vector2Int.left);

            bool hasRight =
                dungeonRooms.ContainsKey(
                    gridPos + Vector2Int.right);
            builder.Build(template, hasUp,
    hasDown,
    hasLeft,
    hasRight);
        }
    }
}
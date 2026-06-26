using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon/Room Template")]
public class TileRoomTemplate : ScriptableObject
{
    public int width = 25;
    public int height = 25;

    public TileType[] tiles;

    public void Init()
    {
        tiles = new TileType[width * height];
    }

    public TileType GetTile(int x, int y)
    {
        return tiles[y * width + x];
    }

    public void SetTile(int x, int y, TileType type)
    {
        tiles[y * width + x] = type;
    }
}
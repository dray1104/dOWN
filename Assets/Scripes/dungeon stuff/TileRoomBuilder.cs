using UnityEngine;

public class TileRoomBuilder : MonoBehaviour
{
    public TileSet tileSet;

    public float tileSize = 1f;

    public void Build(TileRoomTemplate template,bool hasUp,
    bool hasDown,
    bool hasLeft,
    bool hasRight)
    {
        for (int y = 0; y < template.height; y++)
        {
            for (int x = 0; x < template.width; x++)
            {
                TileType tile = template.GetTile(x, y);

                Vector3 pos =
                    transform.position +
                    new Vector3(
                        x * tileSize,
                        0,
                        y * tileSize
                    );

                switch (tile)
                {
                    case TileType.Floor:

                        Instantiate(
                            tileSet.floorTile,
                            pos,
                            Quaternion.identity,
                            transform);

                        break;

                    case TileType.Wall:

                        Instantiate(
                            tileSet.wallTile,
                            pos,
                            Quaternion.identity,
                            transform);

                        break;

                    case TileType.Obstacle:

                        Instantiate(
                            tileSet.floorTile,
                            pos,
                            Quaternion.identity,
                            transform);

                        Instantiate(
                            tileSet.obstacleTile,
                            pos,
                            Quaternion.identity,
                            transform);

                        break;

                    case TileType.EnemySpawn:

                        Instantiate(
                            tileSet.floorTile,
                            pos,
                            Quaternion.identity,
                            transform);

                        GameObject spawner =
                            new GameObject("EnemySpawner");

                        spawner.transform.parent = transform;
                        spawner.transform.position = pos;

                        EnemySpawner enemySpawner =
                            spawner.AddComponent<EnemySpawner>();

                        enemySpawner.enemyPrefab = tileSet.enemyPrefab;

                        break;
                    case TileType.DoorUp:

    if (hasUp)
    {
        SpawnDoor(pos, DoorDirection.Up);
    }
    else
    {
        Instantiate(
            tileSet.wallTile,
            pos,
            Quaternion.identity,
            transform);
    }

    break;
                                case TileType.DoorDown:

    if (hasDown)
    {
        SpawnDoor(pos, DoorDirection.Down);
    }
    else
    {
        Instantiate(
            tileSet.wallTile,
            pos,
            Quaternion.identity,
            transform);
    }

    break;
                                    case TileType.DoorLeft:

    if (hasLeft)
    {
        SpawnDoor(pos, DoorDirection.Left);
    }
    else
    {
        Instantiate(
            tileSet.wallTile,
            pos,
            Quaternion.identity,
            transform);
    }

    break;
                                    case TileType.DoorRight:

    if (hasRight)
    {
        SpawnDoor(pos, DoorDirection.Right);
    }
    else
    {
        Instantiate(
            tileSet.wallTile,
            pos,
            Quaternion.identity,
            transform);
    }

    break;
                }
            }
        }
    }
    private void SpawnDoor(Vector3 pos, DoorDirection direction)
{
    // Place the floor under the door
    Instantiate(
        tileSet.floorTile,
        pos,
        Quaternion.identity,
        transform);

    GameObject door = Instantiate(
        tileSet.doorPrefab,
        pos,
        Quaternion.identity,
        transform);

    DoorController controller = door.GetComponent<DoorController>();

    controller.direction = direction;
    controller.currentRoom = GetComponent<RoomController>();
}
}
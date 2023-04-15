using UnityEngine;
using UnityEngine.Tilemaps;
//A class responsible for the procedural arena generation and generating and placing enemies.
public class ArenaGenerator : MonoBehaviour
{

    public static Evolution evolution;

    const int size = 21 * 2 + 1; // Ensuring the arena size is always odd. "21" should be changed and it is a half (almost) of the size of the one arena dimension.
    const int water = 0;
    const int ground = 1;
    const int middlePoint = (size - 1) / 2;//A middle point of the arena. 
    public int[,] map = new int[size, size];
    [Range(0, 100)]
    public int randomRatio;
    public int smoothingSteps;

    public GameObject wall;
    public Tile waterTiles;
    public Tile[] groundTiles;
    public Tile[] groundTilesCliffs;
    public Tilemap tileMap;
    public GameObject[] destructables;
    public GameObject enemyPrefab;

    public GameObject player;

    public TileBase waterAnimTile;
    [SerializeField]
    int numberOfEnemies;
    int enemiesNumber;
    int chestCount;
    public GameObject spellChest;
    public GameObject chest;
    public bool removeSmallIslands;
    bool playerNotOnTheGround = true;
    public static Vector2 portalSpawnPoint;

    void Start()
    {
        chestCount = 20;
        enemiesNumber = numberOfEnemies;
        GenerateEnemies();
        InitializeMap();
        SmoothMap(smoothingSteps);
        DrawMap();
        Spiral(size * size, middlePoint, middlePoint, "enemies");
    }
    //Generating the evolution object for later enemies instantiating.
    void GenerateEnemies()
    {
        evolution = new Evolution(numberOfEnemies, GameControler.collectedGenes);
    }
    //Initializing the binary array for the map for later smoothing. All edges are always water.
    void InitializeMap()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (x == 0 || y == 0 || x == size - 1 || y == size - 1)
                {
                    map[x, y] = water;
                }
                else
                    map[x, y] = (Random.Range(0, 100) < randomRatio) ? ground : water;
            }
        }
    }
    //Function that calls the smooth function certain number of times.
    void SmoothMap(int times)
    {
        for (int i = 0; i < times; i++)
        {
            Spiral(size * size, middlePoint, middlePoint, "smooth");
        }
    }
    //A smooth function for a single tile.
    private void Smooth(int x, int y)
    {
        if (x != 0 && y != 0 && x != size - 1 && y != size - 1)
        {
            if (GetNeighbours(water, x, y) > 4)
            {
                map[x, y] = water;
            }
            else if (GetNeighbours(water, x, y) < 4)
            {
                map[x, y] = ground;
            }
        }
    }
    //Function for moving the player so they do not start in the water.
    void MovePlayerToLand(int x, int y)
    {

        if (map[x, y] == 1 && playerNotOnTheGround)
        {
            if (WavesCounter.wavesCounter == 1)
            {
                Instantiate(player, transform.position, Quaternion.identity);
            }
            portalSpawnPoint = new Vector2(x - middlePoint + 0.5f, y - middlePoint + 0.5f);
            Player.player.transform.position = portalSpawnPoint;
            playerNotOnTheGround = false;
        }
    }
    //Prototype{
    //A prototype function for removing small islands, currently not used in the game because does not work in 100% of cases. Can be turned on by changing the "removeSmallIslands" variable to true.
    void RemoveSmallIslands()
    {
        int increment = 0;
        while (map[middlePoint + increment, middlePoint] != 1)
        {
            increment++;
        }
        map[middlePoint + increment, middlePoint] = 2;
        for (int i = 0; i < 100; i++)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (map[x, y] == 2)
                    {
                        SetNeighbours(x, y);
                    }
                }
            }
        }
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (map[x, y] == ground)
                {
                    map[x, y] = water;
                }
            }
        }
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (map[x, y] == 2)
                {
                    map[x, y] = ground;
                }
            }
        }
    }
    //function used only in the prototype function
    void SetNeighbours(int x, int y)
    {
        if (map[x - 1, y] == 1)
        {
            map[x - 1, y] = 2;
        }
        if (map[x, y - 1] == 1)
        {
            map[x, y - 1] = 2;
        }
        if (map[x - 1, y - 1] == 1)
        {
            map[x - 1, y - 1] = 2;
        }
        if (map[x - 1, y + 1] == 1)
        {
            map[x - 1, y + 1] = 2;
        }
        if (map[x + 1, y - 1] == 1)
        {
            map[x + 1, y - 1] = 2;
        }
        if (map[x + 1, y + 1] == 1)
        {
            map[x + 1, y + 1] = 2;
        }
        if (map[x + 1, y] == 1)
        {
            map[x + 1, y] = 2;
        }
        if (map[x, y + 1] == 1)
        {
            map[x, y + 1] = 2;
        }
    }
    //}Prototype

    //Function that checks all the neighbours of current position in the array.
    int GetNeighbours(int type, int x, int y)
    {
        int count = 0;
        if (map[x - 1, y] == type)
        {
            count++;
        }
        if (map[x + 1, y] == type)
        {
            count++;
        }
        if (map[x, y - 1] == type)
        {
            count++;
        }
        if (map[x, y + 1] == type)
        {
            count++;
        }
        if (map[x + 1, y + 1] == type)
        {
            count++;
        }
        if (map[x + 1, y - 1] == type)
        {
            count++;
        }
        if (map[x - 1, y + 1] == type)
        {
            count++;
        }
        if (map[x - 1, y - 1] == type)
        {
            count++;
        }
        return count;
    }
    //Function for drawing a wall around the arena.
    void DrawWall()
    {
        for (int i = 0; i <= size; i++)
        {
            Instantiate(wall, new Vector2(i - middlePoint + 0.5f, size - middlePoint + 0.5f), Quaternion.identity);//up
            Instantiate(wall, new Vector2(size - middlePoint + 0.5f, i - middlePoint - 0.5f), Quaternion.identity);//right
            Instantiate(wall, new Vector2(i - middlePoint - 0.5f, 0 - middlePoint - 0.5f), Quaternion.identity);//down
            Instantiate(wall, new Vector2(0 - middlePoint - 0.5f, i - middlePoint + 0.5f), Quaternion.identity);//left
        }
    }

    //Function that draws already smoothed map, also instantiating chests, destrucrables and enemies.
    void DrawMap()
    {//The prototype call
        if (removeSmallIslands)
        {
            RemoveSmallIslands();
        }
        Spiral(size * size, middlePoint, middlePoint, "placePlayer");
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                //Placing cliffs.
                if (map[x, y] == ground && map[x, y - 1] == 0)
                {
                    DrawTile(groundTilesCliffs[RandomNumber(4)], x - middlePoint, y - middlePoint);
                }

                else if (map[x, y] == ground)
                {
                    //Placing ground tiles.
                    DrawTile(groundTiles[RandomNumber(64)], x - middlePoint, y - middlePoint);
                    //Placing destructables.
                    if (RandomNumber(10) == 0)
                    {
                        Instantiate(destructables[RandomNumber(4)], new Vector2(x - middlePoint + 0.5f, y - middlePoint + 0.5f), Quaternion.identity);
                    }
                    else if (RandomNumber(40) == 0 && chestCount > 0)
                    {//Placing chests.
                        if (RandomNumber(2) == 0)
                        {
                            Instantiate(spellChest, new Vector2(x - middlePoint + 0.5f, y - middlePoint + 0.5f), Quaternion.identity);
                            chestCount--;
                        }
                        else if (RandomNumber(2) == 0)
                        {
                            Instantiate(chest, new Vector2(x - middlePoint + 0.5f, y - middlePoint + 0.5f), Quaternion.identity);
                            chestCount--;
                        }
                    }
                }//Placing the water tiles
                else if (map[x, y] == water)
                {
                    DrawWaterTile(waterAnimTile, x - middlePoint, y - middlePoint);
                }
            }
        }
        //Placing the wall around the arena.
        DrawWall();
    }
    //Random number from 0 to range function.
    int RandomNumber(int range)
    {
        return Random.Range(0, range);
    }
    //Function fo placing a tile.
    void DrawTile(Tile tile, int x, int y)
    {
        tileMap.SetTile(new Vector3Int(x, y, 0), tile);
    }
    //Function for placing a water tile.
    void DrawWaterTile(TileBase tile, int x, int y)
    {
        tileMap.SetTile(new Vector3Int(x, y, 0), tile);
    }
    //Function for placing enemies. 
    void PlaceEnemies(int x, int y, int spawnStart)
    {
        GameObject enemy;
        //If the place is ground, chance based on size of the map and how far it is from the middle point.
        if (map[x, y] == 1 && RandomNumber((size - 1) / 4) == 0 && spawnStart / 1.6 > nFunc(middlePoint) && enemiesNumber > 0)
        {
            enemy = Instantiate(enemyPrefab, new Vector2(x - middlePoint + 0.5f, y - middlePoint + 0.5f), Quaternion.identity);
            enemy.GetComponent<Enemy>().SetGene(evolution.GetNextGene(enemiesNumber - 1));
            enemiesNumber--;
        }
    }
    //A spiral function for more evenly spread smoothing steps and placing of enemies.
    void Spiral(int spiralLength, int x, int y, string spiralFunction)
    {
        const int right = 1;
        const int up = 1;
        const int left = -1;
        const int down = -1;

        int xpos = x;
        int ypos = y;

        int steps = 0;
        int counter = 0;
        int i = 0;
        int switchCon;
        while (counter < spiralLength)
        {
            switchCon = i % 4;
            if (i % 2 == 0)
            {
                steps++;
            }
            switch (switchCon)
            {
                case 0:
                    xpos = moveX(steps, right, xpos, ypos, spiralFunction);
                    break;
                case 1:
                    ypos = moveY(steps, up, xpos, ypos, spiralFunction);
                    break;
                case 2:
                    xpos = moveX(steps, left, xpos, ypos, spiralFunction);
                    break;
                case 3:
                    ypos = moveY(steps, down, xpos, ypos, spiralFunction);
                    break;
            }
            counter += steps;
            i++;
        }
    }
    int moveX(int steps, int dir, int x, int y, string function)
    {
        for (int i = 0; i < steps; i++)
        {
            switch (function)
            {
                case "smooth":
                    Smooth(x, y);
                    break;
                case "enemies":
                    PlaceEnemies(x, y, nFunc(steps));
                    break;
                case "placePlayer":
                    MovePlayerToLand(x, y);
                    break;
            }
            x += dir;
        }
        return x;
    }
    int moveY(int steps, int dir, int x, int y, string function)
    {
        for (int i = 0; i < steps; i++)
        {
            switch (function)
            {
                case "smooth":
                    Smooth(x, y);
                    break;
                case "enemies":
                    PlaceEnemies(x, y, nFunc(steps));
                    break;
                case "placePlayer":
                    MovePlayerToLand(x, y);
                    break;

            }
            y += dir;
        }
        return y;
    }
    //Function for calculating the value of steps after which enemies should start to be placed.
    int nFunc(int n)
    {
        return n * n + n;
    }
}


using System.Collections.Generic;
using UnityEngine;

public class SignalGridManager : MonoBehaviour
{
    public static SignalGridManager Instance;

    [Header("Grid Settings")]
    public int gridSize = 5;
    public GameObject tilePrefab;
    public Transform gridParent;

    private Tile[,] grid;
    private Dictionary<Vector2Int, ToggleRuleType> tileRules = new();
    private List<ToggleRuleType> selectedKnightRules = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        GenerateGrid();
        SelectKnightRules();
        AssignRandomToggleRules();
        ScrambleBoard(5); 
    }

    void GenerateGrid()
    {
        grid = new Tile[gridSize, gridSize];

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                GameObject tileObj = Instantiate(tilePrefab, gridParent);
                Tile tile = tileObj.GetComponent<Tile>();
                Vector2Int position = new(x, y);
                tile.Init(position);
                grid[x, y] = tile;
            }
        }
    }

    void SelectKnightRules()
    {
        List<ToggleRuleType> allKnightRules = new()
        {
            ToggleRuleType.KnightMove1, ToggleRuleType.KnightMove2,
            ToggleRuleType.KnightMove3, ToggleRuleType.KnightMove4,
            ToggleRuleType.KnightMove5, ToggleRuleType.KnightMove6,
            ToggleRuleType.KnightMove7, ToggleRuleType.KnightMove8
        };

        while (selectedKnightRules.Count < 2)
        {
            int index = Random.Range(0, allKnightRules.Count);
            selectedKnightRules.Add(allKnightRules[index]);
            allKnightRules.RemoveAt(index);
        }
    }

    void AssignRandomToggleRules()
    {
        List<ToggleRuleType> rulePool = new()
        {
            ToggleRuleType.SelfOnly,
            ToggleRuleType.Diagonal,
            ToggleRuleType.AdjacentMirrors,
            ToggleRuleType.RowMirrors,
            ToggleRuleType.ColumnMirrors
        };

        rulePool.AddRange(selectedKnightRules); 

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                Vector2Int pos = new(x, y);
                ToggleRuleType rule = rulePool[Random.Range(0, rulePool.Count)];
                tileRules[pos] = rule;
            }
        }
    }

    void ScrambleBoard(int numberOfMoves)
    {
        for (int i = 0; i < numberOfMoves; i++)
        {
            Vector2Int randomPos = new(
                Random.Range(0, gridSize),
                Random.Range(0, gridSize)
            );
            ToggleTileGroup(randomPos, false); 
        }
    }

    public void ToggleTileGroup(Vector2Int center, bool checkWinAfter = true)
    {
        if (!tileRules.ContainsKey(center)) return;

        ToggleRuleType rule = tileRules[center];
        List<Vector2Int> affected = TogglePatternGenerator.GetAffectedTiles(center, rule, gridSize);

        foreach (var pos in affected)
        {
            if (IsInBounds(pos))
                grid[pos.x, pos.y].Toggle();
        }
        UIManager.Instance.ShowToggleFeedback(affected.Count);


        if (checkWinAfter)
            CheckForWin();
    }

    bool IsInBounds(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < gridSize && pos.y >= 0 && pos.y < gridSize;
    }

    void CheckForWin()
    {
        foreach (var tile in grid)
        {
            if (tile.IsOn)
                return;
        }

        UIManager.Instance?.ShowWinScreen();

        Debug.Log("Puzzle Solved!");
    }

    public ToggleRuleType GetRuleForTile(Vector2Int pos)
    {
        return tileRules.TryGetValue(pos, out var rule) ? rule : ToggleRuleType.SelfOnly;
    }
}

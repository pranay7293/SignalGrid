using System.Collections.Generic;
using UnityEngine;

public static class TogglePatternGenerator
{
    public static List<Vector2Int> GetAffectedTiles(Vector2Int center, ToggleRuleType rule, int gridSize)
    {
        List<Vector2Int> result = new();

        void Add(Vector2Int offset)
        {
            Vector2Int pos = center + offset;
            if (pos.x >= 0 && pos.x < gridSize && pos.y >= 0 && pos.y < gridSize)
                result.Add(pos);
        }

        switch (rule)
        {
            case ToggleRuleType.SelfOnly:
                //  only self toggle
                break;

            case ToggleRuleType.Diagonal: // Diagonal 4 corners
                Add(new Vector2Int(-1, -1));
                Add(new Vector2Int(1, -1));
                Add(new Vector2Int(-1, 1));
                Add(new Vector2Int(1, 1));
                break;

            case ToggleRuleType.AdjacentMirrors:
                Add(new Vector2Int(0, -1)); // Up 
                Add(new Vector2Int(-1, 0)); // Left
                Add(new Vector2Int(1, 0));  // Right
                Add(new Vector2Int(0, 1));  // Down
                break;

            case ToggleRuleType.RowMirrors:
                Add(new Vector2Int(-1, 0)); // Left
                Add(new Vector2Int(1, 0));  // Right
                break;

            case ToggleRuleType.ColumnMirrors:
                Add(new Vector2Int(0, -1)); // Up
                Add(new Vector2Int(0, 1));  // Down
                break;

            // Knight move rules
            case ToggleRuleType.KnightMove1: Add(new Vector2Int(0, -1)); Add(new Vector2Int(1, -1)); break; // 2 and 3
            case ToggleRuleType.KnightMove2: Add(new Vector2Int(1, 0)); Add(new Vector2Int(1, 1)); break;   // 6 and 9
            case ToggleRuleType.KnightMove3: Add(new Vector2Int(0, 1)); Add(new Vector2Int(-1, 1)); break;  // 8 and 7
            case ToggleRuleType.KnightMove4: Add(new Vector2Int(-1, 0)); Add(new Vector2Int(-1, -1)); break;// 4 and 1
            case ToggleRuleType.KnightMove5: Add(new Vector2Int(0, -1)); Add(new Vector2Int(-1, -1)); break;// 2 and 1
            case ToggleRuleType.KnightMove6: Add(new Vector2Int(-1, 0)); Add(new Vector2Int(-1, 1)); break; // 4 and 7
            case ToggleRuleType.KnightMove7: Add(new Vector2Int(0, 1)); Add(new Vector2Int(1, 1)); break;   // 8 and 9
            case ToggleRuleType.KnightMove8: Add(new Vector2Int(1, 0)); Add(new Vector2Int(1, -1)); break;  // 6 and 3
        }

        result.Add(center); // Always toggle self
        return result;
    }
}

public enum ToggleRuleType
{
    SelfOnly,
    Diagonal,
    AdjacentMirrors,
    RowMirrors,
    ColumnMirrors,

    KnightMove1,
    KnightMove2,
    KnightMove3,
    KnightMove4,
    KnightMove5,
    KnightMove6,
    KnightMove7,
    KnightMove8
}


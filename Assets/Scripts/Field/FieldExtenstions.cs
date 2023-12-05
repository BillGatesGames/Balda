using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public static class FieldExtenstions
    {
        public static HashSet<Vector2Int> GetEmptyPosAroundOccupiedPos(this char?[,] field)
        {
            var dxdy = new List<Vector2Int>()
            {
                new Vector2Int(-1, 0),
                new Vector2Int(1, 0),
                new Vector2Int(0, 1),
                new Vector2Int(0, -1)
            };

            System.Func<int, int, bool> inBounds = (x, y) =>
            {
                return x >= 0 && y >= 0 && x < field.GetLength(0) && y < field.GetLength(1);
            };

            var result = new HashSet<Vector2Int>();

            for (int y = 0;  y < field.GetLength(1); y++)
            {
                for (int x = 0;  x < field.GetLength(0); x++)
                {
                    if (field[x, y] != null)
                    {
                        for (int i = 0; i < dxdy.Count; i++)
                        {
                            int dx = x + dxdy[i].x;
                            int dy = y + dxdy[i].y;

                            if (inBounds(dx, dy))
                            {
                                if (field[dx, dy] == null)
                                {
                                    result.Add(new Vector2Int(dx, dy));
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}

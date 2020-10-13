using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DIRECTIONS
{
    public static readonly Vector2 UP = new Vector2(0, 1);
    public static readonly Vector2 DOWN = new Vector2(0, -1);
    public static readonly Vector2 RIGHT = new Vector2(1, 0);
    public static readonly Vector2 LEFT = new Vector2(-1, 0);
    public static readonly Vector2[] AllDirection = { UP, DOWN, RIGHT, LEFT };
}

public class BoardMatrix : BoardElement
{
    public BoardTile[,] matrix { get; set; }
    public Vector2 BoardSize { get; set; }
    public Vector2 MatrixSize { get; set; }
    public void NewBoard(int width, int height, int[,] data = null)
    {
        BoardSize = new Vector2(width, height);
        MatrixSize = new Vector2(width, height + 1);

        //build new matrix

        matrix = new BoardTile[(int)MatrixSize.y, (int)MatrixSize.x];

        int noPiece = 1;
        for (int h = 0; h < height + 1; h++)
        {
            for (int w = 0; w < width; w++)
            {
                matrix[h, w] = new BoardTile();
                if (h == 0 && w != 0)
                {
                    matrix[h, w].IsAccessible = false;
                }

                //set data
                if (h > 0)
                    matrix[h, w].setBoardTile(noPiece++, w, h);

            }
        }
        if (data == null)
        {
            Shuffle();
        } else
        {
            for (int h = 0; h < height + 1; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    matrix[h, w].no = data[h, w];
                }
            }
        }

    }



    public bool NearEmptyCell(Vector2 position)
    {
        foreach (Vector2 dir in DIRECTIONS.AllDirection)
        {
            Vector2 consideringCell = position + dir;
            BoardTile tile = getTileByIndex(consideringCell);
            if (tile != null && tile.IsEmpty)
            {
                return true;
            }
        }
        return false;
    }

    private bool isOutOfMatrix(Vector2 position)
    {
        if (position.x < 0 || position.x >= MatrixSize.x)
        {
            return true;
        }

        if (position.y < 0 || position.y >= MatrixSize.y)
        {
            return true;
        }
        return !matrix[(int)position.y, (int)position.x].IsAccessible;
    }

    public BoardTile getTileByIndex(Vector2 index)
    {
        if (!isOutOfMatrix(index)) return matrix[(int)index.y, (int)index.x];
        return null;
    }


    public BoardTile findEmpty()
    {
        foreach (BoardTile cell in matrix)
        {
            if (cell.IsAccessible && cell.no == 0)
            {
                return cell;
            }
        }
        return null;
    }

    public void swapCell(Vector2 from, Vector2 to)
    {
        BoardTile fromCell = getTileByIndex(from);
        BoardTile toCell = getTileByIndex(to);
        BoardTile temp = fromCell;

        fromCell = toCell;
        toCell = temp;
        fromCell.position = from;
        toCell.position = to;
        matrix[(int)from.y, (int)from.x] = fromCell;
        matrix[(int)to.y, (int)to.x] = toCell;
    }
    public BoardTile MoveCellToSpace(Vector2 dir)
    {
        BoardTile emptyCell = findEmpty();
        BoardTile desCell = getTileByIndex(emptyCell.position + dir);
        if (emptyCell != null && desCell != null)
        {
            swapCell(emptyCell.position, desCell.position);
            board.Notify(BoardNotification.AnimateCell, null, desCell);
            return desCell;
        }
        return null;
    }

    public BoardTile MoveCellToSpace(Tile tile)
    {
        BoardTile emptyCell = findEmpty();
        Vector2 curPos = tile.Position;
        foreach(Vector2 dir in DIRECTIONS.AllDirection)
        {
            BoardTile desCell = getTileByIndex(curPos + dir);
            if (desCell != null && desCell.position == emptyCell.position)
            {
                swapCell(desCell.position, curPos);
                board.Notify(BoardNotification.AnimateCell, null, getTileByIndex(curPos+dir));
                return desCell;
            }
        }
        return null;
    }

    public List<Vector2> getPossibleMove()
    {
        BoardTile tile = findEmpty();
        List<Vector2> possibleMoves = new List<Vector2>();
        foreach (Vector2 dir in DIRECTIONS.AllDirection)
        {
            if (getTileByIndex(tile.position + dir) != null)
            {
                possibleMoves.Add(dir);
            }
        }
        return possibleMoves;
    }

    void Shuffle()
    {
        System.Random rd = new System.Random();
        for (int i = 0; i < 100; i++)
        {
            BoardTile empty = findEmpty();
            List<Vector2> moves = getPossibleMove();
            int r_value = rd.Next(0, moves.Count);
            swapCell(empty.position, empty.position + moves[r_value]);
        }
    }



    public bool IsWin
    {
        get
        {
            int no = 1;
            for (int h = 1; h < MatrixSize.y; h++)
            {
                for (int w = 0; w < MatrixSize.x; w++)
                {
                    if (!(no++ == matrix[h, w].no))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

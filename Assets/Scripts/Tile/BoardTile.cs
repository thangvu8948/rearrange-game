using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UIElements;

public class BoardTile
{
    private Vector2 pos;
    public Vector2 position
    {
        get { return pos; }
        set
        {
            pos = value;
            if (tile != null)
            {
                tile.Position = value;
            }
        }
    }
    public Tile tile { get; set; }
    private int number = 0;
    private bool isAccessible = true;
    public bool IsEmpty
    {
        get
        {
            return this.number > 0 && IsAccessible ? false : true;
        }
    }
    public bool IsAccessible
    {
        get
        {
            return this.isAccessible;
        }
        set
        {
            isAccessible = value;
        }
    }
    public int no
    {
        get { if (tile == null) return number; else return tile.no; }
        set
        {
            number = value;
            if (tile != null)
            {
                tile.setNo(value);
            }
        }
    }

    public void setBoardTile(int no, int x, int y)
    {
        position = new Vector2(x, y);
        this.no = no;
        if (tile != null) tile.Position = position;
    }
}

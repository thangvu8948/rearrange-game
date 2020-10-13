using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum EMode
{
    EASY,
    MEDIUM,
    HARD,
    ENDLESS
}
public class Mode
{
    private EMode mMode;
    public EMode mode { get { return mMode; } }
    public int Time { get; set; }
    public Vector2 Size { get; set; }
    public Sprite Image { get; set; }
    public int IdxImage { get; set; }

    public Mode(EMode mode, Sprite sprite, int idx)
    {
        this.Image = sprite;
        this.mMode = mode;
        IdxImage = idx;
        switch (mode)
        {
            case EMode.EASY:
                Time = LEVEL_TIME.EASY;
                Size = LEVEL_BOARD_SIZE.EASY;
                break;
            case EMode.MEDIUM:
                Time = LEVEL_TIME.MEDIUM;
                Size = LEVEL_BOARD_SIZE.MEDIUM;
                break;
            case EMode.HARD:
                Time = LEVEL_TIME.HARD;
                Size = LEVEL_BOARD_SIZE.HARD;
                break;
            case EMode.ENDLESS:
                Time = -1;
                Size = LEVEL_BOARD_SIZE.HARD;
                break;
        }
    }
}

public static class LEVEL_TIME
{
    public const int EASY = 60;
    public const int MEDIUM = 120;
    public const int HARD = 200;
}

public static class LEVEL_BOARD_SIZE
{
    public static Vector2 EASY = new Vector2(3, 3);
    public static Vector2 MEDIUM = new Vector2(4, 5);
    public static Vector2 HARD = new Vector2(5 , 6);
}

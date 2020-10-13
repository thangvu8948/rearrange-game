using DigitalRuby.Tween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Board : BoardElement, IController
{
    public GameObject prefab;
    public GameObject holder;
    public Sprite image;
    public float padding;
    // public BoardMatrix boardModel;

    int width = (int)GameModel.mode.Size.x;
    int height = (int)GameModel.mode.Size.y;
    BoardTile[,] matrix;
    Sprite[] croppeds;
    private Vector2 v; //cell size
    Vector2 start;
    public Vector2 cellSize { get { return v; } }

    // Start is called before the first frame update
    void Start()
    {
        v = new Vector2(140,140);
        start = new Vector2(-width / 2 * v.x + 0.5f * ((width % 2 == 0) ? 1 : 0) * v.x, height / 2 * v.y);
        animateQueue = new Queue();

        Debug.Log($"{Screen.width}");
        if (start.x - v.x < -Screen.width / 2 + padding)
        {
            float scale = (float)(-Screen.width / 2 + padding) / (start.x - v.x);
            Debug.Log($"{Screen.width} - {scale}");
            this.board.gameObject.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
        }
    }

    public void NewGame()
    {
        image = GameModel.mode.Image;
        if (SaveData.getInstance() != null && SaveData.getInstance().loadedMode == true)
        {
            Debug.Log(GameModel.mode.Size);
            board.model.NewBoard((int)GameModel.mode.Size.x, (int)GameModel.mode.Size.y, SaveData.getInstance().saveMatrixData.matrix); ;
        }
        else
        {
            board.model.NewBoard((int)GameModel.mode.Size.x, (int)GameModel.mode.Size.y);
        }
        matrix = board.model.matrix;
        croppeds = new Sprite[width * height];
        foreach (BoardTile cell in matrix)
        {
            if (cell.IsAccessible)
            {
                Vector3 position = new Vector3(start.x + v.x * cell.position.x, start.y - v.y * cell.position.y, 0);
                GameObject holderObj = Instantiate(holder, position, Quaternion.identity);
                holderObj.transform.SetParent(this.transform);
                holderObj.transform.localPosition = position;
                holderObj.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
        }

        Crop();

        foreach (BoardTile cell in matrix)
        {
            if (cell.IsAccessible && cell.no > 0)
            {
                Vector3 position = new Vector3(start.x + v.x * cell.position.x, start.y - v.y * cell.position.y, 0);
                GameObject pieceImg = Instantiate(prefab, position, Quaternion.identity);
                pieceImg.GetComponent<Tile>().setNo(cell.no, croppeds[cell.no - 1]);
                cell.tile = pieceImg.GetComponent<Tile>();
                cell.tile.Position = cell.position;
                pieceImg.transform.SetParent(this.transform);
                pieceImg.transform.localPosition = position;
                pieceImg.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
        }

        Model.getInstance().gameModel.IsGameOver = false;
        Model.getInstance().gameModel.IsPause = false;
    }

    public Vector3 CalcPosition(BoardTile cell)
    {
        return new Vector3(start.x + v.x * cell.position.x, start.y - v.y * cell.position.y, 0);

    }

    bool isAnimate = false;
    Queue animateQueue;
    public void AnimateMoveCell(BoardTile cell)
    {
        if (animateQueue.Count == 0 && !isAnimate)
        {
            animateQueue.Enqueue(cell);
        }
        if (!isAnimate && animateQueue.Count > 0)
        {
            BoardTile animateCell = (BoardTile)animateQueue.Dequeue();
            Vector3 oldPosition = cell.tile.transform.localPosition;
            Vector3 newPosition = CalcPosition(animateCell);
            isAnimate = true;
            Action<ITween<Vector3>> updateCellPos = (t) =>
            {
                //isAnimate = true;
                animateCell.tile.gameObject.transform.localPosition = t.CurrentValue;
                if (t.CurrentValue == newPosition)
                {
                    isAnimate = false;
                    if (animateQueue.Count > 0)
                    {
                        board.Notify(BoardNotification.AnimateCell, null, animateQueue.Peek());
                    }
                }
            };

            animateCell.tile.gameObject.Tween("MoveCell", oldPosition, newPosition, 0.25f, TweenScaleFunctions.QuinticEaseIn, updateCellPos);
        }
        else
        {
            if (isAnimate)
            {
                animateQueue.Enqueue(cell);
                Debug.Log(animateQueue.Count);

            }
        }
    }

    public void OnNotification(string p_event_path, object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            case BoardNotification.NewGame:
                NewGame();
                break;
            case BoardNotification.MoveCell:
               board.model.MoveCellToSpace((Vector2)p_data[0]);
                
                    //board.Notify(BoardNotification.AnimateCell, null, tile);
                
                break;
            case BoardNotification.AnimateCell:
                board.controller.AnimateMoveCell((BoardTile)p_data[0]);
                if (board.model.IsWin)
                {
                    Model.getInstance().gameModel.IsGameOver = true;
                    ShowCompleteImage();
                }
                break;
            case BoardNotification.MoveCellByTouch:
                board.model.MoveCellToSpace((Tile)p_data[0]);
                break;

        }
    }



    public void Crop()
    {
        Rect rect = image.textureRect;

        Vector2 size = new Vector2(image.rect.width / board.model.BoardSize.x, image.rect.height / board.model.BoardSize.y);
        int pixelsToUnit = 100;
        for (int h = 0; h < board.model.BoardSize.y; h++)
        {
            for (int w = 0; w < board.model.BoardSize.x; w++)
            {
                Rect croppedSpriteRect = rect;
                croppedSpriteRect.width = size.x;
                croppedSpriteRect.height = size.y;
                croppedSpriteRect.x = w * size.x;
                croppedSpriteRect.y = ((int)board.model.BoardSize.y - h - 1) * size.y;
                int idx = h * (int)board.model.BoardSize.x + w;
                Sprite sprite = Sprite.Create(image.texture, croppedSpriteRect, new Vector2(0, 1), pixelsToUnit);
                croppeds[idx] = sprite;
            }
        }
    }

    public void ShowCompleteImage()
    {

        foreach(BoardTile tile in matrix)
        {
            if (tile.tile != null)
            {
                tile.tile.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            }
        }
        board.Notify(PopupNotification.Complete, null, null);
    }
}

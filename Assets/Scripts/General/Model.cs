using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class Model : MonoBehaviour
{
    public BoardMatrix boardModel;
    public GameModel gameModel;

    private static Model instance = null;

    public static Model getInstance()
    {
        return instance;
    }

    private void Start()
    {
        instance = this;
    }

    public static void SaveAll()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGame.dat");
        Debug.Log(Application.persistentDataPath);
        SaveData data = new SaveData();
        BoardMatrix matrix = instance.boardModel;
        GameModel gameModel = instance.gameModel;
        data.saveMatrixData = new MatrixSave(matrix.matrix, matrix.MatrixSize);
        data.saveGameData = new GameSave(GameModel.mode.mode, gameModel.Time, GameModel.mode.IdxImage) ;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("saved");
    }

    public static bool Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGame.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGame.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            data.loadedMode = false;
            file.Close();
            SaveData.SetInstance(data);
            return true;
        }
        else return false;
    }
}
[Serializable]
public class SaveData
{
    public bool loadedMode;
    public MatrixSave saveMatrixData;
    public GameSave saveGameData;

    private static SaveData instance = null;
    public static SaveData getInstance()
    {
        return instance;
    }
    public static void SetInstance(SaveData save)
    {
        instance = new SaveData();
        instance = save;
    }
}
[Serializable]
public class MatrixSave
{
    public int[,] matrix;
    /// <summary>
    /// Save data for Board Model
    /// </summary>
    /// <param name="v1">Matrix data</param>
    /// <param name="v2">MatrixS size</param>
    public MatrixSave(BoardTile[,] v1, Vector2 v2)
    {
        matrix = new int[(int)v2.y, (int)v2.x];
        for (int h = 0; h < (int)v2.y; h++)
        {
            for (int w = 0; w < (int)v2.x; w++)
            {
                matrix[h, w] = v1[h, w].no;
            }
        }
    }
}
[Serializable]
public class GameSave
{
    public EMode mode;
    public int Time;
    public int IdxImage;

    /// <summary>
    /// Save data for game model
    /// </summary>
    /// <param name="v1">Mode</param>
    /// <param name="v2">Time remaining</param>
    /// <param name="v3">Index of Image</param>
    public GameSave(EMode v1, int v2, int v3)
    {
        mode = v1;
        Time = v2;
        IdxImage = v3;
    }
}
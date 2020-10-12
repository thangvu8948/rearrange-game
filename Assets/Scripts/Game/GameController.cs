using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public static class GameNotification
{
    public const string NewGame = "game.newGame";
    public const string GameOver = "game.gameOver";
}
public class GameController : GameElement, IController
{
    public Model model;
    public Button pauseButton;
    private void Start()
    {
        //game.Notify(GameNotification.NewGame, null, null);
        //game.Notify(BoardNotification.NewGame, null, null);
    }

    public void OnNotification(string p_event_path, object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            case GameNotification.NewGame:
                NewGame();
                break;
            case GameNotification.GameOver:
                GameOver();
                break;

        }
    }

    public void NewGame()
    {
        this.game.model.IsGameOver = false;
        if (SaveData.getInstance() == null)
        {
            this.game.model.Time = GameModel.mode.Time;
        } else
        {
            this.game.model.Time = SaveData.getInstance().saveGameData.Time;
        }
        TimeController.getInstance().StartTimer();
    }

    public void GameOver()
    {
        this.game.model.IsGameOver = true;
        game.Notify(PopupNotification.GameOver, null, null);
    }

    public void OnHome()
    {
        Model.SaveAll();
        Loading.getInstance().LoadScene(SceneName.Home);
    }

    public void OnPause()
    {
        game.model.IsPause = !game.model.IsPause;
        pauseButton.GetComponentInChildren<Text>().text = game.model.IsPause ? "resume" : "pause";
        if (!game.model.IsPause)
        {
            TimeController.getInstance().StartTimer();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            Model.SaveAll();
            OnPause();

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public Text text;
    private static TimeController instance;
    public static TimeController getInstance()
    {
        return instance;
    }
    void Start()
    {
        instance = this;
        //text.text = Model.getInstance().gameModel.Time.ToString();
        //StartCoroutine(StartTimer());
    }

    public void StartTimer()
    {
        StartCoroutine(Count());
    }
    IEnumerator Count()
    {
        text.text = Model.getInstance().gameModel.TimeFormat;
        yield return new WaitForSeconds(2);
        while (Model.getInstance() == null)
        {
            yield return null;
        }
        GameModel model = Model.getInstance().gameModel;

        if (GameModel.mode.mode == EMode.ENDLESS)
        {
            model.Time = -1;
            text.text = "∞";
            text.fontSize = 113;
        }
        else
            while (!model.IsGameOver && !model.IsPause && model.Time >= 0)
            {
                text.text = Model.getInstance().gameModel.TimeFormat;
                yield return new WaitForSeconds(1);
                model.Time -= 1;
                if (model.Time < 0 && GameModel.mode.mode != EMode.ENDLESS)
                {
                    //text.text = Model.getInstance().gameModel.TimeFormat;
                    model.game.Notify(GameNotification.GameOver, null, null);
                }
            }
    }



}

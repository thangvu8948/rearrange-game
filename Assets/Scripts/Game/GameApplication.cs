using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[Serializable]
public class GameElement : MonoBehaviour
{
    public GameApplication game { get { return GameObject.FindObjectOfType<GameApplication>(); } }
}
public class GameApplication : MonoBehaviour, IApplication
{
    public GameModel model;
    public GameController controller;
    public void Notify(string p_event_path, object p_target, params object[] p_data)
    {
        IController[] controllers = Controller.getInstance().FetchControllers();
        foreach (IController ctl in controllers)
        {
            ctl.OnNotification(p_event_path, p_target, p_data);
        }
    }

    private void Start()
    {
        StartCoroutine(startGame());
    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(0.5f);
        controller.game.Notify(GameNotification.NewGame, null, null);
        controller.game.Notify(BoardNotification.NewGame, null, null);
    }
}
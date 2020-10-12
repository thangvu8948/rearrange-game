using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Controller: MonoBehaviour
{
    public GameController gameController;
    public Board boardController;
    public PopupController popupController;
    private static Controller instance = null;

    public static Controller getInstance()
    {
        return instance;
    }

    public IController[] FetchControllers()
    {
        IController[] controllers = { gameController, boardController, popupController};
        return controllers;
    }

    private void Start()
    {   
        Controller.instance = this;
    }
}

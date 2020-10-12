using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[Serializable]
public class BoardElement : MonoBehaviour
{
    public BoardApplication board
    {
        get
        {
            return GameObject.FindObjectOfType<BoardApplication>();
        }
    }
}

public class BoardApplication : MonoBehaviour
{
    public Board controller;
    public BoardMatrix model;
    public BoardView view;
    private void Start()
    {
        
    }
    public void Notify(string p_event_path, object p_target, params object[] p_data)
    {
        IController[] controllers = Controller.getInstance().FetchControllers();
        foreach(IController ctl in controllers)
        {
            ctl.OnNotification(p_event_path, p_target, p_data);
        }
    }
}

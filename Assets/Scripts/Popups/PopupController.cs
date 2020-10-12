using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PopupController : MonoBehaviour, IController
{
    public void OnNotification(string p_event_path, object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            case PopupNotification.Complete:
                CompleteImagePopup.getInstance().show();
                break;
            case PopupNotification.GameOver:
                GameOverPopup.getInstance().show();
                break;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IController
{
    void OnNotification(string p_event_path, object p_target, params object[] p_data);
}
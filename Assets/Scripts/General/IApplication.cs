
using UnityEngine;

public interface IApplication
{
    void Notify(string p_event_path, object p_target, params object[] p_data);
}

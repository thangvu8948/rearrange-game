using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class GameModel : GameElement
{
    public static Mode mode { get; set; }
    public bool IsPause { get; set; }
    public bool IsGameOver { get; set; }
    public int Time { get; set; }
    public bool UseTimer { get; set; } = true;
    public string TimeFormat
    {
        get
        {
            int t = Time;
            int m = t / 60;
            int s = t % 60;
            string time = "";
            time += (m < 10) ? "0" + m.ToString() : m.ToString();
            time += ":";
            time += (s < 10) ? "0" + s.ToString() : s.ToString();
            return time;
        }
    }
}
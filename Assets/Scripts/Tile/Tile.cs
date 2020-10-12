using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public Text label;
    public GameObject image;
    public int no { get; set; }
    public void setNo(int no, Sprite sprite)
    {
        this.no = no;
        this.label.text = no.ToString();
        this.image.GetComponent<Image>().sprite = sprite;
    }

    public void setNo(int no)
    {
        this.no = no;
        this.label.text = no.ToString();
    }
}

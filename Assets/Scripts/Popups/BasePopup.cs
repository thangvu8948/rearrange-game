using DigitalRuby.Tween;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BasePopup: MonoBehaviour
{
    public GameObject shadow;
    public GameObject container;
    private GameObject thisObj;
    protected void Start()
    {
        this.gameObject.SetActive(false);
        thisObj = this.gameObject;
        this.GetComponent<CanvasGroup>().alpha = 0;
        this.container.GetComponent<CanvasGroup>().alpha = 0;
        //this.shadow.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0);
    }

    public void show()
    {
        this.gameObject.SetActive(true);
        Action<ITween<float>> updateOpacityContainer = (t) =>
        {
            container.GetComponent<CanvasGroup>().alpha = t.CurrentValue;
        };

        Action<ITween<float>> updateOpacityPopup = (t) =>
        {
            thisObj.GetComponent<CanvasGroup>().alpha = t.CurrentValue;
        };
        this.thisObj.Tween("Popup", 0, 1, 0.5f, TweenScaleFunctions.QuarticEaseIn, updateOpacityPopup).ContinueWith(new FloatTween().Setup(0, 1, 0.5f, TweenScaleFunctions.QuarticEaseIn,updateOpacityContainer));
        //updateOpacity(this.container, 1);
        //tween.ContinueWith(updateOpacity(this.container));
        //tween.Start();
        //shadow.gameObject.Tween("ShowPopup", 0, 1, 0.25f, TweenScaleFunctions.QuinticEaseIn, updateOpacity);
    }

    public Tween<float> updateOpacity(GameObject gameObject, float delay = 0)
    {
        Action<ITween<float>> updateOpacity = (t) =>
        {
            gameObject.GetComponent<CanvasGroup>().alpha = t.CurrentValue;
        };
        Tween<float> tween = gameObject.gameObject.Tween("UpdateOpacity", 0, 1, 0.3f, TweenScaleFunctions.QuinticEaseIn, updateOpacity);
        tween.Delay = delay;
        return tween;
    }
}
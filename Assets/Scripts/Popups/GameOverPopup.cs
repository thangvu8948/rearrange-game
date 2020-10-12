using DigitalRuby.Tween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPopup : BasePopup
{
    private static GameOverPopup instance = null;
    public GameObject nextBtn;
    private Vector3 startPoint;
    private Vector3 endPoint;
    public static GameOverPopup getInstance()
    {
        return instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        endPoint = nextBtn.transform.localPosition;
        startPoint = new Vector3(endPoint.x, -Screen.height / 2 - 200, endPoint.z);
        base.Start();
    }

    public void show()
    {
        this.nextBtn.transform.localPosition = startPoint;
        base.show();
        animateBtn();
    }
    void animateBtn()
    {
        Action<ITween<Vector3>> updateButtonPos = (t) =>
        {
            //isAnimate = true;
            nextBtn.gameObject.transform.localPosition = t.CurrentValue;
        };

        nextBtn.gameObject.Tween("MoveBtn", startPoint, endPoint, 0.75f, TweenScaleFunctions.QuinticEaseIn, updateButtonPos).Delay = 0.5f;
    }


    public void OnNextBtn()
    {
        Loading.getInstance().LoadScene(SceneName.Home);
    }
}

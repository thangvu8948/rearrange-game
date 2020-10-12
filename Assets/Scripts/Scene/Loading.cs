using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class SceneName
{
    public static string Home = "home";
    public static string Game = "game";
    public static string Loading = "Loading";
}
public class Loading : MonoBehaviour
{
    private static Loading instance = null;
    public Slider slider;
    public Text text;
    public static Loading getInstance()
    {
        return instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        instance = this;
        LoadScene(SceneName.Home);
    }
    public void ValueChangeCheck()
    {
        text.text = $"{slider.value * 100} %";
    }
    IEnumerator load(string name)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        async.allowSceneActivation = true;
        for (int i = 0; i <= 100; i++)
        {
            yield return new WaitForSeconds(0.01f);
            slider.value = (float)i / 100;
        }
        yield return new WaitForSeconds(0.5f);
        ShowRootObject(false);
    }

    IEnumerator preloadActiveScene()
    {
        yield return null;
    }
    public void LoadScene(string name)
    {
        Debug.Log("Scene to load: " + name);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        ShowRootObject(true);
        slider.value = 0;
        StartCoroutine(load(name));
    }
    
    void ShowRootObject(bool isShow)
    {
        if (!SceneManager.GetActiveScene().name.Equals("Loading"))
        {
            Loading.getInstance().gameObject.SetActive(isShow);
        }
    }
}

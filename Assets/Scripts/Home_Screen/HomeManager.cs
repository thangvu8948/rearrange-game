using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public enum LEVEL
{
    EASY,
    MEDIUM,
    HARD
}
public class HomeManager : MonoBehaviour
{
    private int selected = 0;
    public Color unselectedColor;
    public Color selectedColor;
    public Button[] buttonsLevel;
    public HorizontalScrollSnap imageSelection;
    public Button ContinueButton;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttonsLevel.Length; i++)
        {
            //buttonsLevel[i].onClick.AddListener(() => onSelected(idx));
            buttonsLevel[i].GetComponent<Image>().color = unselectedColor;
        }
        buttonsLevel[0].GetComponent<Image>().color = selectedColor;
        if (!Model.Load())
        {
            ContinueButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnContinue()
    {
        if (selected != -1)
        {
            int idx = SaveData.getInstance().saveGameData.IdxImage;
            SaveData.getInstance().loadedMode = true;
            imageSelection.ChangePage(idx);
            Sprite sprite = imageSelection.CurrentPageObject().GetComponentInChildren<Image>().sprite;
            GameModel.mode = new Mode(SaveData.getInstance().saveGameData.mode, sprite, idx);

            Loading.getInstance().LoadScene("game");
        }
    }
    public void OnPlay()
    {
        if (selected != -1)
        {
            if (SaveData.getInstance() != null)
            {
                SaveData.getInstance().loadedMode = false;
            }
            Sprite sprite = imageSelection.CurrentPageObject().GetComponentInChildren<Image>().sprite;
            GameModel.mode = new Mode((EMode)selected, sprite, imageSelection.CurrentPage);
            Loading.getInstance().LoadScene("game");
        }
    }

    public void onSelected(int index)
    {
        
        selected = index;
        Debug.Log(index);
        for (int i = 0; i < buttonsLevel.Length; i++)
        {
            if (i == index)
            {
                buttonsLevel[i].GetComponent<Image>().color = selectedColor;
            }
            else
            {
                buttonsLevel[i].GetComponent<Image>().color = unselectedColor;
            }
        }
    }
}

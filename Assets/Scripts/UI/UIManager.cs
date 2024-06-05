using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    private static UIManager instance;
    public static UIManager Instance => instance;

    public Image hPMaskImage;
    public Image mPMaskImage;
    private float originalSize;

    public GameObject battlePanelGo;
    public GameObject talkPanelGo;

    public Text textName;
    public Text textContent;
    public Image characterImage;
    public Sprite[] characterSpriteList;
    private void Awake() {
        instance = this;
        originalSize = hPMaskImage.rectTransform.rect.width;
        SetHPValue(1f);
    }
    private UIManager() {
    }

    public void SetHPValue(float fillPercent) {
        hPMaskImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,fillPercent * originalSize);
    }
    public void SetMPValue(float fillPercent) {
        mPMaskImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,fillPercent * originalSize);
    }
    public void ShowOrHideBattlePanel(bool show) {
        battlePanelGo.SetActive(show);
    }
    public void ShowDialog(string content = null,string name = null) {
        if(content == null) {
            talkPanelGo.SetActive(false);
        }
        else {
            talkPanelGo.SetActive(true);
            if(name != null) {
                if(name == "Luna") {
                    characterImage.sprite = characterSpriteList[0];
                }
                else {
                    characterImage.sprite = characterSpriteList[1];
                }
                characterImage.SetNativeSize();
            }
            textContent.text = content;
            textName.text = name;
        }
    }
}

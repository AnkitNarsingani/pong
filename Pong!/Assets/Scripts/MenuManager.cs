﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public static MenuManager Instance { get; private set; }

    [HideInInspector]
    public int currentLevelNo;

    [HideInInspector]
    public string currentLevelName;

    [SerializeField]
    GameObject menuUI, startUI;

    [SerializeField]
    string Column1 = "Settings";

    [SerializeField]
    Text leftHanded, rightHanded;

    [SerializeField]
    Sprite muteSprite, unmuteSprite;

    bool muted = false;

    [SerializeField]
    Animator anim;

    [SerializeField]
    string Column2 = "Level Selector";

    [SerializeField]
    Text nextLevelName;

    [SerializeField]
    Image faceSprite;

    [SerializeField]
    string Column3 = "Endless Mode";

    [SerializeField]
    Text endlessHighScore;

    Sprite currentLevelSprite;

    [SerializeField]
    string Column4 = "Start UI";

    bool shouldPlayanim = false;

    [SerializeField]
    Color yellow;
    [SerializeField]
    RectTransform baby, levelNameTextRect;

    [SerializeField]
    Text levelNumber, levelNameText;

    [SerializeField]
    Image face;

    [SerializeField]
    Sprite[] levelSprites;

    [SerializeField]
    string[] levelName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        currentLevelNo = PlayerPrefs.GetInt("currentLevel", 17);
        currentLevelName = levelName[currentLevelNo - 1];
        SetUI();
    }

    void Start ()
    {
        Time.timeScale = 1;

        if (GameManager.Instance.isLeftHanded)
        {
            anim.Play("Slide Left");
            leftHanded.color = Color.black;
            rightHanded.color = Color.white;
        }
        else
        {
            anim.Play("Slide Right");
            leftHanded.color = Color.white;
            rightHanded.color = Color.black;
        }

        endlessHighScore.text = PlayerPrefs.GetInt("maxRallies").ToString();
        currentLevelNo = PlayerPrefs.GetInt("currentLevel", 17);
        currentLevelName = levelName[currentLevelNo - 1];
        SetUI();
    }
	

	void Update ()
    {
		if(shouldPlayanim)
        {
            if (baby.localPosition.x < -27)
            {
                baby.localPosition += new Vector3(43, 0, 0);
            }               
            if(levelNameTextRect.localPosition.x > 17)
            {
                levelNameTextRect.localPosition -= new Vector3(43, 0, 0);
            }
        }

        if (levelNameTextRect.localPosition.x < 19 && baby.localPosition.x > -18 && !shouldPlayanim)
        {
            baby.localPosition += new Vector3(43, 0, 0);
            levelNameTextRect.localPosition -= new Vector3(43, 0, 0);
        }
    }

    void SetUI()
    {
        levelNumber.text = currentLevelNo.ToString();
        levelNameText.text = currentLevelName;
        string[] t = currentLevelName.Split(' ');
        nextLevelName.text = currentLevelNo.ToString() + "." + "\n" + t[0] + "\n" + t[1];
        faceSprite.overrideSprite = levelSprites[currentLevelNo - 1];
        face.overrideSprite = levelSprites[currentLevelNo - 1];

    }

    public void OnTouchHand()
    {
        GameManager.Instance.UpdateSettings();

        if(GameManager.Instance.isLeftHanded)
        {
            anim.Play("Slide Left");
            StartCoroutine(ActivateButton(0.5f));
            leftHanded.color = Color.black;
            rightHanded.color = Color.white;
        }
        else
        {
            anim.Play("Slide Right");
            StartCoroutine(ActivateButton(0.5f));
            leftHanded.color = Color.white;
            rightHanded.color = Color.black;
        }
    }

    IEnumerator ActivateButton(float waitTime)
    {
        leftHanded.GetComponentInParent<Button>().interactable = false;
        yield return new WaitForSeconds(waitTime);
        leftHanded.GetComponentInParent<Button>().interactable = true;
    }

    public void OnTouchAudio(Button button)
    {
        muted = !muted;

        if(muted)
        {
            AudioListener.volume = 0;
            button.image.overrideSprite = unmuteSprite;
        }
        else
        {
            AudioListener.volume = 1;
            button.image.overrideSprite = muteSprite;
        }
    }

    public void OnTouchPlay()
    {
        menuUI.SetActive(false);
        Camera.main.backgroundColor = yellow;
        startUI.SetActive(true);
        shouldPlayanim = true;
        StartCoroutine(ChangeLevel());
    }

    IEnumerator ChangeLevel()
    {
        yield return new WaitForSeconds(2.5f);

        shouldPlayanim = false;

        yield return new WaitForSeconds(1f);

        LevelLoad(temp());
    }

    public void LevelLoad(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void LevelLoad(string level)
    {
        SceneManager.LoadScene(level);
    }

    int temp()
    {
        switch(currentLevelNo)
        {
            case 17:
                return 1;
            case 16:
                return 2;
            case 15:
                return 3;
            case 14:
                return 4;
            case 13:
                return 5;
            case 12:
                return 6;
            case 11:
                return 7;
            case 10:
                return 8;
            case 9:
                return 9;
            case 8:
                return 10;
            case 7:
                return 11;
            case 6:
                return 12;
            case 5:
                return 13;
            case 4:
                return 14;
            case 3:
                return 15;
            case 2:
                return 16;
            case 1:
                return 17;
            default:
                return 17;
        }
    }
}

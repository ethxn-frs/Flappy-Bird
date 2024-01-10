using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SocialPlatforms.Impl;

public class UIController : MonoBehaviour
{
    static public UIController Instance;

    public GameObject player;

    public CanvasGroup MainMenu;
    public CanvasGroup Gameplay;
    public CanvasGroup GameOverMenu;
    public CanvasGroup PlayerSelectorMenu;

    public Text ScoreText;

    public GameObject StartGameUI;

    public GameObject RestartGame;
    public GameObject GameOverPanel;
    public GameObject SoundPanel;
    public GameObject PlayerSelectorAccess;
    public GameObject PlayerSelectorLeave;

    public Text GameOverScoreText;
    public Text GameOverHighScoreText;

    public Sprite[] characterSprites;  // Tableau des sprites disponibles
    public RuntimeAnimatorController[] characterAnimators; //Tableau des animations disponibles
    public Sprite[] soundSprites;
    public SpriteRenderer spriteRdr;


    void Awake()
    {
        Instance = this;

        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameEnded += OnGameEnded;
    }

    void Start()
    {
        MainMenu.alpha = 1;
        Gameplay.alpha = 0;
        GameOverMenu.alpha = 0;
        PlayerSelectorMenu.alpha = 0;

        GameOverMenu.gameObject.SetActive(false);
        Gameplay.gameObject.SetActive(false);
        PlayerSelectorMenu.gameObject.SetActive(false);

        // Récupérez le composant Image existant
        Image soundImage = SoundPanel.GetComponent<Image>();
        // Changez la sprite associée
        soundImage.sprite = soundSprites[(int)PlayerPrefs.GetFloat("SoundState")];

        switch (PlayerPrefs.GetInt("ActualPlayer", 1))
        {
            case 1:
                SelectPlayerOne();
                break;
            case 2:
                SelectPlayerTwo();
                break;
            case 3:
                SelectPlayerThree();
                break;
            case 4:
                SelectPlayerFour();
                break;
            default:
                SelectPlayerOne();
                break;
        }

    }


    void OnDestroy()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameEnded -= OnGameEnded;
    }

    void OnGameStarted()
    {
        MainMenu.DOFade(0, 0.2f).OnComplete(() => MainMenu.gameObject.SetActive(false));
        Gameplay.gameObject.SetActive(true);
        Gameplay.DOFade(1, 0.2f);

    }

    void OnGameEnded()
    {
        Gameplay.DOFade(0, 0.2f).OnComplete(() => Gameplay.gameObject.SetActive(false));
        GameOverMenu.gameObject.SetActive(true);

        GameOverScoreText.text = ScoreManager.Instance.score.ToString();
        GameOverHighScoreText.text = ScoreManager.Instance.highScore.ToString();

        GameOverPanel.transform.localScale = Vector3.zero;
        RestartGame.transform.localScale = Vector3.zero;

        GameOverMenu.DOFade(1, 0.4f).SetDelay(0.5f)
        .OnComplete(() => GameOverPanel.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack)
        .OnComplete(() => RestartGame.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack)));
    }

    public void UpdateScore(int score)
    {
        ScoreText.text = score.ToString();
        ScoreText.transform.DOPunchScale(Vector3.one * 0.15f, 0.2f);
    }

    public void TriggerStartGame()
    {
        StartGameUI.SetActive(false);
        GameManager.Instance.StartGame();
    }

    public void OpenSelectPlayerMenu()
    {
        StartGameUI.SetActive(false);
        MainMenu.DOFade(0, 0.2f).OnComplete(() => MainMenu.gameObject.SetActive(false));
        Gameplay.gameObject.SetActive(false);

        PlayerSelectorMenu.gameObject.SetActive(true);
        PlayerSelectorMenu.DOFade(1, 0.4f).SetDelay(0.5f);

        // Réinitialisez l'alpha de GameOverPanel et RestartGame
        GameOverPanel.transform.localScale = Vector3.zero;
        RestartGame.transform.localScale = Vector3.zero;

        PlayerSelectorMenu.DOFade(1, 0.4f).SetDelay(0.5f)
            .OnComplete(() => PlayerSelectorMenu.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack)
                .OnComplete(() => PlayerSelectorLeave.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack)));
    }

    public void CloseSelectPlayerMenu()
    {
        PlayerSelectorMenu.DOFade(0, 0.2f).OnComplete(() =>
        {
            PlayerSelectorMenu.gameObject.SetActive(false);
            MainMenu.gameObject.SetActive(true);
            MainMenu.DOFade(1, 0.4f).SetDelay(0.5f);
            StartGameUI.SetActive(true);
        });
    }

    public void clearPolygonCollider2d()
    {
        if (player.GetComponent<PolygonCollider2D>() != null)
        {
            player.GetComponent<PolygonCollider2D>().points = null;
        }
    }

    public void SelectPlayerOne()
    {
        player.GetComponent<SpriteRenderer>().sprite = characterSprites[0];
        player.GetComponent<Animator>().runtimeAnimatorController = characterAnimators[0];

        player.transform.localScale = new Vector3(3f, 3f, 1f);

        clearPolygonCollider2d();

        Vector2[] colliderPoints = new Vector2[]
        {
            new Vector2(0.131991863f,0.0608011223f),
            new Vector2(0.0719828457f,0.122295275f),
            new Vector2(-0.056965895f,0.124202274f),
            new Vector2(-0.155292034f,0.0348647535f),
            new Vector2(-0.172518581f,-0.027129963f),
            new Vector2(-0.144750282f,-0.0811431855f),
            new Vector2(-0.109500557f,-0.116392881f),
            new Vector2(0.0290261786f,-0.12068899f),
            new Vector2(0.128988609f,-0.101089768f),
            new Vector2(0.153190896f,-0.080584079f),
            new Vector2(0.170000002f,-0.0134613886f),
            new Vector2(0.132685214f,0.00863976032f)

        };

        player.GetComponent<PolygonCollider2D>().points = colliderPoints;

        CloseSelectPlayerMenu();

        PlayerPrefs.SetInt("ActualPlayer", 1);
        PlayerPrefs.Save();
    }

    public void SelectPlayerTwo()
    {
        player.GetComponent<SpriteRenderer>().sprite = characterSprites[1];
        player.GetComponent<Animator>().runtimeAnimatorController = characterAnimators[1];

        player.transform.localScale = new Vector3(0.3894711f, 0.3130755f, 1f);

        clearPolygonCollider2d();

        Vector2[] colliderPoints = new Vector2[]
        {
            new Vector2(-1.38623071f,-0.518875241f),
            new Vector2(-1.55880046f,0.249430656f),
            new Vector2(-1.69466054f,0.490985513f),
            new Vector2(-1.51882064f,0.803006649f),
            new Vector2(-1.20326948f,0.594327688f),
            new Vector2(-1.19209099f,-0.0609164238f),
            new Vector2(-0.77371341f,0.539265037f),
            new Vector2(-0.386791646f,0.725992143f),
            new Vector2(-0.553054929f,1.25563967f),
            new Vector2(-0.267835319f,1.60170257f),
            new Vector2(0.258825779f,1.39809597f),
            new Vector2(0.262644082f,0.784127593f),
            new Vector2(1.22779465f,0.12180353f),
            new Vector2(1.35230029f,0.518447697f),
            new Vector2(1.67942405f,0.556486845f),
            new Vector2(1.71466732f,0.00246754289f),
            new Vector2(1.47362053f,-0.539971471f),
            new Vector2(1.30930996f,-0.909727097f),
            new Vector2(0.543056011f,-1.54996467f),
            new Vector2(-0.998122215f,-1.54919744f),
            new Vector2(-1.4617244f,-0.737041175f)

        };

        player.GetComponent<PolygonCollider2D>().points = colliderPoints;

        CloseSelectPlayerMenu();

        PlayerPrefs.SetInt("ActualPlayer", 2);
        PlayerPrefs.Save();
    }

    public void SelectPlayerThree()
    {
        player.GetComponent<SpriteRenderer>().sprite = characterSprites[2];
        player.GetComponent<Animator>().runtimeAnimatorController = characterAnimators[2];
        player.transform.localScale = new Vector3(0.2671841f, 0.1550287f, 1f);

        clearPolygonCollider2d();

        Vector2[] colliderPoints = new Vector2[]
        {
            new Vector2(-2.23366141f,0.569907308f),
            new Vector2(-2.67018461f,1.07897925f),
            new Vector2(-3.02832246f,2.11551809f),
            new Vector2(-1.18540525f,3.25708818f),
            new Vector2(-0.272484779f,2.56450558f),
            new Vector2(0.655131221f,2.79986382f),
            new Vector2(2.04385877f,2.05330515f),
            new Vector2(1.33656192f,0.336100817f),
            new Vector2(0.850863695f,-2.1713326f),
            new Vector2(0.0707175136f,-2.79428744f),
            new Vector2(-0.936260462f,-2.6117835f),
            new Vector2(-1.35932231f,-1.68745565f),
            new Vector2(-1.6297164f,-0.556378484f),
            new Vector2(-1.51729321f,0.161636472f)

        };

        player.GetComponent<PolygonCollider2D>().points = colliderPoints;


        CloseSelectPlayerMenu();

        PlayerPrefs.SetInt("ActualPlayer", 3);
        PlayerPrefs.Save();
    }

    public void SelectPlayerFour()
    {
        player.GetComponent<SpriteRenderer>().sprite = characterSprites[3];
        player.GetComponent<Animator>().runtimeAnimatorController = characterAnimators[3];
        player.transform.localScale = new Vector3(0.52368f, 0.54372f, 1f);

        clearPolygonCollider2d();

        Vector2[] colliderPoints = new Vector2[]
        {
            new Vector2(0.585805535f,-0.612877369f),
            new Vector2(0.323427796f,-0.944119513f),
            new Vector2(0.100772403f,-1.00189376f),
            new Vector2(-0.275643587f,-0.892027497f),
            new Vector2(-0.46630621f,-0.758534789f),
            new Vector2(-0.66727668f,-0.468470395f),
            new Vector2(-0.66727668f,-0.468470395f),
            new Vector2(-0.66727668f,-0.468470395f),
            new Vector2(-0.66727668f,-0.468470395f),
            new Vector2(-0.772889018f,0.164390698f),
            new Vector2(-0.735802889f,0.715780318f),
            new Vector2(-0.400136799f,1.02493715f),
            new Vector2(0.0248390827f,1.03493977f),
            new Vector2(0.335498512f,0.960776687f),
            new Vector2(0.740952551f,0.612846792f),
            new Vector2(0.734359324f,-0.0896374136f)
        };

        player.GetComponent<PolygonCollider2D>().points = colliderPoints;

        CloseSelectPlayerMenu();

        PlayerPrefs.SetInt("ActualPlayer", 4);
        PlayerPrefs.Save();
    }

    public void AlternSoundImage(int state)
    {
        if (state < soundSprites.Length)
        {
            if (SoundPanel.GetComponent<Image>() != null)
            {
                // Récupérez le composant Image existant
                Image soundImage = SoundPanel.GetComponent<Image>();
                // Changez la sprite associée
                soundImage.sprite = soundSprites[state];
            }
        }
    }

}

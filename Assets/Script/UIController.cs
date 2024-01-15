using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SocialPlatforms.Impl;

public class UIController : MonoBehaviour
{
    static public UIController Instance;

    public CanvasGroup MainMenu;
    public CanvasGroup Gameplay;
    public CanvasGroup GameOverMenu;
    public CanvasGroup PlayerSelectorMenu;

    public GameObject player;
    public GameObject StartGameUI;
    public GameObject RestartGame;
    public GameObject GameOverPanel;
    public GameObject SoundPanel;
    public GameObject PlayerSelectorAccess;
    public GameObject PlayerSelectorLeave;

    public Text ScoreText;
    public Text GameOverScoreText;
    public Text GameOverHighScoreText;

    public SpriteRenderer spriteRdr;

    public Sprite[] characterSprites;  // Tableau des sprites disponibles
    public Sprite[] soundSprites;

    public RuntimeAnimatorController[] characterAnimators; //Tableau des animations disponibles


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

        Image soundImage = SoundPanel.GetComponent<Image>();
        soundImage.sprite = soundSprites[(int)PlayerPrefs.GetFloat("SoundState")];

        switch (PlayerPrefs.GetInt("ActualPlayer", 1))
        {
            case 1:
                SelectPlayerFlappy();
                break;
            case 2:
                SelectPlayerThomas();
                break;
            case 3:
                SelectPlayerRobin();
                break;
            case 4:
                SelectPlayerAurelien();
                break;
            case 5:
                SelectPlayerPrince();
                break;
            case 6:
                SelectPlayerEnzoLpn();
                break;
            case 7:
                SelectPlayerEnzoLptv();
                break;
            case 8:
                SelectPlayerLiam();
                break;
            case 9:
                SelectPlayerLucas();
                break;
            case 10:
                SelectPlayerMicka();
                break;
            default:
                SelectPlayerFlappy();
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

        if (PlayerPrefs.GetInt("HighScoreSound") == 1)
        {
            AudioManager.Instance.PLaySound(AudioType.HighScore, AudioSourceType.Game);
            PlayerPrefs.SetInt("HighScoreSound", 0);
            PlayerPrefs.Save();
        };

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

    public void SelectPlayerFlappy()
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

    public void SelectPlayerThomas()
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

    public void SelectPlayerRobin()
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

    public void SelectPlayerAurelien()
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

    public void SelectPlayerPrince()
    {
        player.GetComponent<SpriteRenderer>().sprite = characterSprites[4];
        player.GetComponent<Animator>().runtimeAnimatorController = characterAnimators[4];
        player.transform.localScale = new Vector3(0.595371723f, 0.631965756f, 1);

        clearPolygonCollider2d();

        Vector2[] colliderPoints = new Vector2[]
        {
            new Vector2(0.417521596f,-0.461230963f),
            new Vector2(0.220994234f,-0.751115024f),
            new Vector2(0.0678473338f,-0.801996231f),
            new Vector2(-0.125651181f,-0.774846137f),
            new Vector2(-0.323630691f,-0.644800007f),
            new Vector2(-0.498992771f,-0.306484461f),
            new Vector2(-0.5131464f,0.191962793f),
            new Vector2(-0.538252056f,0.560687423f),
            new Vector2(-0.40815419f,0.658113003f),
            new Vector2(0.013864059f,0.807470202f),
            new Vector2(0.423298687f,0.664376915f),
            new Vector2(0.583643436f,0.540470064f),
            new Vector2(0.529492021f,-0.165460616f)
        };

        player.GetComponent<PolygonCollider2D>().points = colliderPoints;

        CloseSelectPlayerMenu();

        PlayerPrefs.SetInt("ActualPlayer", 5);
        PlayerPrefs.Save();
    }

    public void SelectPlayerEnzoLpn()
    {
        player.GetComponent<SpriteRenderer>().sprite = characterSprites[5];
        player.GetComponent<Animator>().runtimeAnimatorController = characterAnimators[5];
        player.transform.localScale = new Vector3(0.391203284f, 0.37692526f, 1);

        clearPolygonCollider2d();

        Vector2[] colliderPoints = new Vector2[]
        {
            new Vector2(0.657747865f,-0.617705584f),
            new Vector2(0.690527678f,-1.09759462f),
            new Vector2(-0.0710541606f,-1.45662856f),
            new Vector2(-0.563857198f,-1.25952172f),
            new Vector2(-0.520831704f,-1.05532742f),
            new Vector2(-0.753372908f,-0.724531531f),
            new Vector2(-1.00778544f,0.415389597f),
            new Vector2(-0.877687395f,0.926355302f),
            new Vector2(-0.4750278f,1.32042766f),
            new Vector2(-0.0407326929f,1.45572233f),
            new Vector2(0.641686201f,1.13380086f),
            new Vector2(0.987660468f,0.227520704f),
            new Vector2(0.900750756f,-0.266051471f)
        };

        player.GetComponent<PolygonCollider2D>().points = colliderPoints;

        CloseSelectPlayerMenu();

        PlayerPrefs.SetInt("ActualPlayer", 6);
        PlayerPrefs.Save();
    }

    public void SelectPlayerEnzoLptv()
    {
        player.GetComponent<SpriteRenderer>().sprite = characterSprites[6];
        player.GetComponent<Animator>().runtimeAnimatorController = characterAnimators[6];
        player.transform.localScale = new Vector3(0.545571506f, 0.52219677f, 1);

        clearPolygonCollider2d();

        Vector2[] colliderPoints = new Vector2[]
        {
            new Vector2(0.112533525f,1.02872872f),
            new Vector2(0.658864439f,0.793542206f),
            new Vector2(1.0030911f,0.285833687f),
            new Vector2(0.877153873f,-0.10329169f),
            new Vector2(0.652337074f,-0.266816705f),
            new Vector2(0.310426325f,-0.960870326f),
            new Vector2(-0.394191146f,-0.894898713f),
            new Vector2(-0.875532091f,-0.254071563f),
            new Vector2(-1.03473949f,0.189901114f),
            new Vector2(-0.99717164f,0.603364944f),
            new Vector2(-0.421952039f,1.01895547f)

        };

        player.GetComponent<PolygonCollider2D>().points = colliderPoints;

        CloseSelectPlayerMenu();

        PlayerPrefs.SetInt("ActualPlayer", 7);
        PlayerPrefs.Save();
    }

    public void SelectPlayerLiam()
    {
        player.GetComponent<SpriteRenderer>().sprite = characterSprites[7];
        player.GetComponent<Animator>().runtimeAnimatorController = characterAnimators[7];
        player.transform.localScale = new Vector3(0.468465924f, 0.451368004f, 1);

        clearPolygonCollider2d();

        Vector2[] colliderPoints = new Vector2[]
        {
            new Vector2(0.831707358f,-0.337854445f),
            new Vector2(0.464380264f,-0.601084769f),
            new Vector2(0.355146587f,-0.905953884f),
            new Vector2(-0.102864385f,-1.25049436f),
            new Vector2(-0.590415716f,-1.11851966f),
            new Vector2(-0.822956502f,-0.64328438f),
            new Vector2(-0.912107766f,-0.0269556493f),
            new Vector2(-0.860291541f,0.709696412f),
            new Vector2(-0.657685459f,1.04960406f),
            new Vector2(-0.0668268055f,1.34739304f),
            new Vector2(0.641686201f,1.09769094f),
            new Vector2(0.970264554f,0.660838425f),
            new Vector2(0.857260764f,0.10407415f),

        };

        player.GetComponent<PolygonCollider2D>().points = colliderPoints;

        CloseSelectPlayerMenu();

        PlayerPrefs.SetInt("ActualPlayer", 8);
        PlayerPrefs.Save();
    }

    public void SelectPlayerLucas()
    {
        player.GetComponent<SpriteRenderer>().sprite = characterSprites[8];
        player.GetComponent<Animator>().runtimeAnimatorController = characterAnimators[8];
        player.transform.localScale = new Vector3(0.391203284f, 0.37692526f, 1);

        clearPolygonCollider2d();

        Vector2[] colliderPoints = new Vector2[]
        {
            new Vector2(0.916791916f,-0.400552005f),
            new Vector2(0.411557287f,-1.36645138f),
            new Vector2(0.44144693f,-1.36645126f),
            new Vector2(-0.120870546f,-1.46696925f),
            new Vector2(-0.57403481f,-1.20020115f),
            new Vector2(-0.928079247f,-0.143005043f),
            new Vector2(-0.927503467f,0.802267611f),
            new Vector2(-0.584623218f,1.23770249f),
            new Vector2(-0.0407326929f,1.44538176f),
            new Vector2(0.47231108f,1.38197625f),
            new Vector2(0.907954812f,0.816937387f),
            new Vector2(0.93064028f,0.499156117f)
        };

        player.GetComponent<PolygonCollider2D>().points = colliderPoints;

        CloseSelectPlayerMenu();

        PlayerPrefs.SetInt("ActualPlayer", 9);
        PlayerPrefs.Save();
    }

    public void SelectPlayerMicka()
    {
        player.GetComponent<SpriteRenderer>().sprite = characterSprites[9];
        player.GetComponent<Animator>().runtimeAnimatorController = characterAnimators[9];
        player.transform.localScale = new Vector3(0.353608668f, 0.307005614f, 1);
        Debug.Log("ntm");
        clearPolygonCollider2d();

        Vector2[] colliderPoints = new Vector2[]
        {
            new Vector2(0.802388489f,-0.556854844f),
            new Vector2(0.411557287f,-1.36645138f),
            new Vector2(0.263485998f,-1.57959175f),
            new Vector2(-0.184428021f,-1.82220292f),
            new Vector2(-0.650303781f,-1.35650396f),
            new Vector2(-1.0546186f,-0.306062222f),
            new Vector2(-1.02952552f,0.584072053f),
            new Vector2(-0.790711224f,1.4169631f),
            new Vector2(-0.328513443f,1.80825698f),
            new Vector2(0.933377862f,1.64108002f),
            new Vector2(1.05775523f,0.442318618f)
        };

        player.GetComponent<PolygonCollider2D>().points = colliderPoints;

        CloseSelectPlayerMenu();

        PlayerPrefs.SetInt("ActualPlayer", 10);
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

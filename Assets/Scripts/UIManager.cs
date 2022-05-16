using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

enum States { Start, Play, End }

public class UIManager : MonoBehaviour
{

    #region Paneller
    [SerializeField]
    private GameObject startPanel;
    [SerializeField]
    private GameObject playPanel;
    [SerializeField]
    private GameObject endPanel;
    #endregion


    #region end panelinin Textleri
    [SerializeField]
    private Text endScore;
    #endregion

    #region start panelinin Textleri
    [SerializeField]
    private Text bestScore;
    [SerializeField]
    private Text playerName;
    #endregion


    #region play panelinin Textleri
    [SerializeField]
    private Text healt;
    [SerializeField]
    private Text score;
    [SerializeField]
    private Text time;
    [SerializeField]
    private Text magazine;
    #endregion

    private float reloadMagazine; //mermi dolum süresi

    private PlayerControl playerScript;

    private float timer; // oyuncu ölmez ise oynanacak olan süre

    private States currentState;


    // Start is called before the first frame update
    private void Start()
    {
        if (!File.Exists(Application.persistentDataPath + "/savefile.json"))
        {
            SaveManager.Instance.SaveScore(); //eðer savefile diye bir dosya yoksa oluþtur varsa içini oku
        }
        SaveManager.Instance.LoadScore();

        Time.timeScale = 0; //kimse hareket etmesin player rigidbodysinin isKinametic true da yapabilirdik
                            //fakat ya enemylerin üretimini durduracaktým ya da onlarý býrakacaktým

        SaveManager.Instance.LoadScore();
        bestScore.text = "Best Score: " + SaveManager.Instance.Name + " / " + SaveManager.Instance.Score;

        currentState = States.Start;

        reloadMagazine = 1;
        timer = 60;
        playerScript = GameObject.FindObjectOfType<PlayerControl>();
    }

    // Update is called once per frame
    private void Update()
    {
        switch (currentState)
        {
            case States.Start:
                StartGame();
                break;
            case States.Play:
                PlayGame();
                break;
            case States.End:
                EndGame();
                break;
        }


    }

    #region Statelerin kullanacaðý metotlar
    private void StartGame()
    {
        SaveManager.Instance.LoadScore(); // oyun bittikten sonra tekrar bu panele geleceðiz eðer best score yaptýysak yazýlsýn diye
        bestScore.text = "Best Score: " + SaveManager.Instance.Name + " / " + SaveManager.Instance.Score;

        startPanel.SetActive(true);
        playPanel.SetActive(false);
        endPanel.SetActive(false);
    }

    private void PlayGame()
    {
        Time.timeScale = 1;
        startPanel.SetActive(false);
        playPanel.SetActive(true);
        endPanel.SetActive(false);


        timer -= Time.deltaTime;

        healt.text = "Healt: " + playerScript.healt.ToString();
        time.text = "Remaining Time: " + timer.ToString("0.00");
        score.text = "Score: " + playerScript.Score.ToString();

        if (playerScript.Fired)
        {
            reloadMagazine -= Time.deltaTime;
            magazine.text = "Reloading the magazine: " + reloadMagazine.ToString("0.00");
        }
        else
        {
            magazine.text = "";
            reloadMagazine = 1;
        }

        if (timer <= 0 || playerScript.healt <= 0)
        {
            currentState = States.End;
        }
    }

    private void EndGame()
    {
        Time.timeScale = 0;

        endScore.text = playerScript.Score.ToString();

        startPanel.SetActive(false);
        playPanel.SetActive(false);
        endPanel.SetActive(true);
    }
    #endregion


    #region buttonlarýn kullanacaðý metotlar
    public void StartButton()
    {
        timer = 60;

        playerScript.healt = 100;
        playerScript.Score = 0;
        SaveManager.Instance.Name = playerName.text;
        currentState = States.Play;
    }

    public void EndButton()
    {
        if (SaveManager.Instance.Score < playerScript.Score)
        {
            SaveManager.Instance.Score = (int)playerScript.Score;
            SaveManager.Instance.SaveScore();
        }
        currentState = States.Start;
    }
    #endregion
}

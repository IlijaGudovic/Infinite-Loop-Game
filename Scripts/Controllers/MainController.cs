using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{

    [Header("Panels")]
    public GameObject diePanel;
    public GameObject pausePanel;
    public GameObject scoreInGame;
    public GameObject setingsPanel;
    public Text pauseScoreText;
    public Text gameOverScoreText;
    public Text highScoreText;

    private float bestHighScore;

    [Header("Backgraund")]
    public List<GameObject> Backgraunds;
    public float startPostion = 21;
    public float endPosition = -15;

    [Header("Backgraund speed")]
    public float speedBackground = 2;
    public float speedBurnBackground = 0.25f;
    public float timeBetweenSpeedUp = 1;
    private int groundCount;
    public float maxGroundSpeed = 4f;


    [Header("Mocvara")]
    public bool mocvaraOn;
    public float minChangeTime = 4;
    public float maxChangeTime = 5;
    

    [Header("Enemys")]
    public float EnemySpeed = 1;
    public float EnemySpeedGrow = 0.2f;

    public List<GameObject> AliveEnemys;

    [Header("Bonus")]
    public float bonusSpeed = 1;
    public List<GameObject> AliveBonus;

    [Header("Player")]
    public float playerSpeedGrow = 10;
    private float score;

    private GameObject player;

    [Header("Effects")]
    public List<GameObject> purpleEffects;
    public List<GameObject> greenEffects;
    public int chanceOfSpawn = 20;
    public float xPosition;

    private void Update()
    {

        if (score < 150)
        {
            score += speedBackground * Time.deltaTime;
        }
        else if (score >= 150 && score < 450)
        {
            score += speedBackground * Time.deltaTime * 2;
        }
        else
        {
            score += speedBackground * Time.deltaTime * 3;
        }

        scoreInGame.GetComponent<Text>().text = score.ToString("0");

    }

    private void Awake()
    {

        bestHighScore = PlayerPrefs.GetFloat("highScore", 0);

        for (int i = 0; i < Backgraunds.Count; i++)
        {

            Backgraunds[i].GetComponent<Background>().endY = endPosition;
            Backgraunds[i].GetComponent<Background>().startY = startPostion;
            Backgraunds[i].GetComponent<Background>().speed = speedBackground;

        }

    }

    private void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");

        InvokeRepeating("speedUpEnemys", 1f, timeBetweenSpeedUp);

        int randomInt = Random.Range(0, 2);


        StartCoroutine(ChangeRegion());

    }

    private void speedUpEnemys()
    {

        if (Backgraunds[0].GetComponent<Background>().speed >= maxGroundSpeed)
        {
            return;
        }

        EnemySpeed += EnemySpeedGrow;

        bonusSpeed += EnemySpeedGrow;

        player.GetComponent<FolowMouse>().speed += playerSpeedGrow;

    }

    private IEnumerator ChangeRegion()
    {

        if (mocvaraOn == true)
        {
            StartCoroutine(lerpColor(zelenaMin, zelenaMax));
        }
        else
        {
            StartCoroutine(lerpColor(plavaMin, plavaMax));
        }

        yield return new WaitForSeconds(Random.Range(minChangeTime, maxChangeTime));

        mocvaraOn = !mocvaraOn;

        spawnEffect();

        foreach (GameObject background in Backgraunds)
        {

            background.GetComponent<Background>().AnimationUpdate();

        }

        foreach (GameObject enemy in AliveEnemys)
        {

            enemy.GetComponent<EnemyMove>().updateEnemy();

        }

        foreach (GameObject bonus in AliveBonus)
        {

            bonus.GetComponent<BonusMove>().updateBonus();

        }

        StartCoroutine(ChangeRegion());

    }

    public void checkGroundTime()
    {

        groundCount++;

        if(groundCount > 2)
        {
            groundCount = 0;

            if (Backgraunds[0].GetComponent<Background>().speed >= maxGroundSpeed)
            {
                return;
            }

            for (int i = 0; i < Backgraunds.Count; i++)
            {

                Backgraunds[i].GetComponent<Background>().speed += speedBurnBackground;

            }

        }

    }


    public void diedPlayer()
    {

        if (score > bestHighScore)
        {

            bestHighScore = score;
            PlayerPrefs.SetFloat("highScore", bestHighScore);

        }

        highScoreText.text = "High Score" + "\n" + bestHighScore.ToString("0");
        gameOverScoreText.text = "Score " + score.ToString("0");

        activeScoreInGame();

        diePanel.SetActive(true);

        Time.timeScale = 0;

    }

    public void restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Time.timeScale = 1;

    }

    private void activeScoreInGame()
    {

        scoreInGame.SetActive(!scoreInGame.activeInHierarchy);

    }

    public void pauseGame()
    {

        activeScoreInGame();

        pausePanel.SetActive(!pausePanel.activeInHierarchy);

        if (pausePanel.activeInHierarchy)
        {

            pauseScoreText.text = score.ToString("0");

            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

    }

    public void changeController(int controllerInt)
    {

        PlayerPrefs.SetInt("Controller", controllerInt);

        player.GetComponent<FolowMouse>().wichController = controllerInt;

        if (controllerInt == 0)
        {
            player.GetComponent<FolowMouse>().joystick.SetActive(false);
        }

        opetOptions();

    }

    public void opetOptions()
    {
        setingsPanel.SetActive(!setingsPanel.activeInHierarchy);
    }


    //Efekti

    [Header("Efekti")]
    public ParticleSystem efekat;
    public effectScript particleScript;
    public Color zelenaMin;
    public Color zelenaMax;
    public Color plavaMin;
    public Color plavaMax;


    IEnumerator lerpColor(Color min, Color max)
    {

        //Start
        var main = efekat.main;

        main.startColor = new ParticleSystem.MinMaxGradient(min, max); //Menja boju startnim particlima


        //Lerp

        particleScript.colorBool = true;

        particleScript.mainColor = max;

        Invoke("stopColorEffectLerp", 1);


        yield return null;


    }

    private void stopColorEffectLerp()
    {

        particleScript.colorBool = false;

    }

    private void spawnEffect()
    {

        int randomInt = Random.Range(0, 100);

        if (randomInt < chanceOfSpawn)
        {

            Vector3 spawnPosition = new Vector3(-xPosition, 7 , 0);

            if (mocvaraOn)
            {

                GameObject objectForSpawn = greenEffects[Random.Range(0, greenEffects.Count)];

                Instantiate(objectForSpawn, spawnPosition, Quaternion.identity);

            }
            else
            {

                GameObject objectForSpawn = purpleEffects[Random.Range(0, purpleEffects.Count)];

                Instantiate(objectForSpawn, spawnPosition, Quaternion.identity);

            }

        }

    }

















































































}

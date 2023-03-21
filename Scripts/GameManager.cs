using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("PROCESS")]
    public Slider process;
    public Text processText;

    int currentLevel = 1;
    public Text currentLevelText;
    public Text nextLevelText;

    public int needScore;
    private int currentScore;
    int needScoreCounter;
    public levelDB levelDB;

    public ParticleSystem winParticle;

    [Header("MOVES")]
    public int Moves;
    public Text movesText;

    [Header("LOSE MENU")]
    public GameObject loseMenu;
    public Text loseMenuScoreText;

    [Header("WIN MENU")]
    public GameObject winMenu;
    public Text winMenuScoreText;

    void Start()
    {
        currentLevel = 1;
        currentLevelText.text = currentLevel.ToString();
        nextLevelText.text = (currentLevel + 1).ToString();

        //PROCESS SET UP
        currentScore = 0;
        process.maxValue = levelDB.levelScore[needScoreCounter];
        processText.text = currentScore.ToString() + "/" + levelDB.levelScore[needScoreCounter];

        //MOVES SET UP
        movesText.text = Moves.ToString();
    }

    public void DecreaseMove()
    {
        Moves--;
        movesText.text = Moves.ToString();

        if(Moves == 0 )
        {
            Invoke("LateControl", .5f);
        }
    }

    void LateControl()
    {
        if (currentScore < needScore)
        {
            loseMenu.SetActive(true);
            loseMenuScoreText.text = currentScore.ToString() + "/" + levelDB.levelScore[needScoreCounter];
        }
    }
   

    public void GetScore(int addScore)
    {
        currentScore += addScore;


        if(currentScore >= levelDB.levelScore[needScoreCounter])
        {
            needScoreCounter++;
            //winMenu.SetActive(true);
            winParticle.Play();


            currentLevel++;
            currentLevelText.text = currentLevel.ToString();
            nextLevelText.text = (currentLevel + 1).ToString();

            Moves += 15;

            currentScore = 0;
        }

        process.value = currentScore;
        process.maxValue = levelDB.levelScore[needScoreCounter];
        processText.text = currentScore.ToString() + "/" + levelDB.levelScore[needScoreCounter];
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    
}

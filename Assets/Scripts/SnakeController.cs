using UnityEngine;
using System.Collections;

public enum SnakeStatus
{
    SnakeMoving,
    Snakedead
}


public class SnakeController : MonoBehaviour {

    public static SnakeStatus SnakeState = SnakeStatus.SnakeMoving;
    // Use this for initialization

    internal int PlayerScore;
    static SnakeController _instance;

    public static SnakeController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SnakeController>();
            }
            return _instance;
        }
    }

    void Start () {

        if (_instance = null)
            _instance = this;
        Debug.Log(GetPlayerData());

        PlayerScore = 0;
        UIManager.Instance.OpenPanel(1);
        UIManager.Instance.SetHighScore(PlayerScore);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collider)
    {
        #region obstacles
        if (collider.gameObject.tag == "obstacles")
        {
            SnakeState = SnakeStatus.Snakedead;
            //check if he made new high score
            savePlayerdata();
            //set the right high score
            UIManager.Instance.SetHighScore(GetPlayerData());
            UIManager.Instance.ArrowPanle.SetActive(false);
            UIManager.Instance.OpenPanel(0);
        }
        #endregion

        #region Apple
        else if (collider.gameObject.tag == "Apple")
        {
            PlayerScore++;
            UIManager.Instance.SetScore(PlayerScore);
            FruitsController.Instance.PlayAppleSound();
            Destroy(collider.gameObject);
            FruitsController.Instance.GenerateApple();
            GetComponent<SnakeMotion>().AddSnakPart();
        }
        #endregion
    }

    public void savePlayerdata()
    {
        if(PlayerScore> GetPlayerData())
        PlayerPrefs.SetInt("Score", PlayerScore);
    }

    public int GetPlayerData()
    {
       return PlayerPrefs.GetInt("Score");
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    List<GameObject> panelList;

   
    [SerializeField]
    Text HighScore;

    [SerializeField]
    Text Scoretext;

    [SerializeField]
    GameObject PausePanle;
    public GameObject ArrowPanle;
   

    static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIManager>();
            }
            return _instance;
        }
    }

    // Use this for initialization
    void Start()
    {
        _instance = this;
    }

    /// <summary>
    /// set panel index to open and close all other panels
    /// </summary>
    /// <param name="index"></param>
    public void OpenPanel(int index)
    {
        for (int i = 0; i < panelList.Count; i++)
        {
            panelList[i].SetActive(false);
        }
        panelList[index].SetActive(true);
    }

    /// <summary>
    /// chose scene index to load
    /// </summary>
    /// <param name="index"></param>
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    /// <summary>
    /// on choose swipe controler
    /// </summary>
    public void ClickSwipe()
    {
        SnakeMotion.Motioncontrol = MotionController.Swipe;
        LoadScene(1);
    }
    /// <summary>
    /// on choose click controller for snak
    /// </summary>
    public void Click_ClickController()
    {
        SnakeMotion.Motioncontrol = MotionController.Click;
        LoadScene(1);
    }

    /// <summary>
    /// on choose Arrow controller for snak
    /// </summary>
    public void ClickArrow()
    {
        SnakeMotion.Motioncontrol = MotionController.Arrows;
        LoadScene(1);
    }

    /// <summary>
    /// whan user decided to quitsave data then close game
    /// </summary>
    public void Quit()
    {
        //Save player dataa before quiting
        SnakeController.Instance.savePlayerdata();
        Application.Quit();
    }

    /// <summary>
    /// set HighScore value
    /// </summary>
    /// <param name="highScore"></param>
    public void SetHighScore(int highScore)
    {
        HighScore.text = highScore.ToString();
    }

    /// <summary>
    /// Set gameplay Score value
    /// </summary>
    /// <param name="Score"></param>
    public void SetScore(int Score)
    {
        Scoretext.text = Score.ToString();
    }

    /// <summary>
    /// set direction using arrows
    /// </summary>
    /// <param name="direction"></param>
    public void SetDirection(string direction)
    {
        GameObject.FindObjectOfType<SnakeMotion>().dir = direction;
    }

    public void OnClickPause()
    {
        PausePanle.SetActive(true);
        SnakeController.SnakeState = SnakeStatus.Snakedead;
    }

    public void OnClickResume()
    {
        PausePanle.SetActive(false);
        SnakeController.SnakeState = SnakeStatus.SnakeMoving;
    }
}

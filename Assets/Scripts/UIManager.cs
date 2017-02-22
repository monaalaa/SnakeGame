using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    List<GameObject> panelList;

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
	
	// Update is called once per frame
	void Update () {
	
	}


    public void OpenPanel(int index)
    {
        for (int i = 0; i < panelList.Count - 1; i++)
        {
            panelList[i].SetActive(false);
        }
        panelList[index].SetActive(true);
    }

    public void LoadScene( int index)
    {
        SceneManager.LoadScene(index);
    }


    public void ClickSwipe()
    {
        SnakeMotion.Motioncontrol = MotionController.Swipe;
        LoadScene(1);
    }

    public void Click_ClickController()
    {
        SnakeMotion.Motioncontrol = MotionController.Click;
        LoadScene(1);
    }
}

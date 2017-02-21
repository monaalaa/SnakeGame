using UnityEngine;
using System.Collections;

public class FruitsController : MonoBehaviour
{

    static FruitsController _instance;

    public static FruitsController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<FruitsController>();
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
    void Update()
    {

    }

    public void GenerateApple()
    {
        Debug.Log("generate another apple");
    }
}

using UnityEngine;
using System.Collections;

public class FruitsController : MonoBehaviour
{

    float boardSize= 28;
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

    /// <summary>
    /// generate apple in random positions
    /// </summary>
    public void GenerateApple()
    {
        float xpos = Random.Range(-boardSize, boardSize);
        float zpos = Random.Range(-boardSize, boardSize);
        Vector3 pos = new Vector3(xpos, 0, zpos);
        GameObject apple = Instantiate(Resources.Load<GameObject>("Apple"), pos, Quaternion.identity) as GameObject;
    }

    /// <summary>
    /// play crunch apple sound when snake eats one
    /// </summary>
    public void PlayAppleSound()
    {
        AudioSource aS = GetComponent<AudioSource>();
        aS.Play();
    }
}

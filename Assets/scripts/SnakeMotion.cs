using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnakeMotion : MonoBehaviour
{

    #region Fields
    public string dir = "w";
    public List<GameObject> BodyParts = new List<GameObject>();

    [SerializeField]
    int SnakeSpeed;

    int ticTime;
    Vector3 TempPos = new Vector3();
    #endregion


    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            appendToSnake();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
        SnakeMovment();
    }

    /// <summary>
    /// function to get which direction the snake shourd move on
    /// </summary>
    void Inputs()
    {
        if (Input.GetKeyDown("w"))
        {
            dir = "w";
            ticTime = SnakeSpeed;
        }
        if (Input.GetKeyDown("s"))
        {
            dir = "s";
            ticTime = SnakeSpeed;
        }
        if (Input.GetKeyDown("a"))
        {
            dir = "a";
            ticTime = SnakeSpeed;
        }
        if (Input.GetKeyDown("d"))
        {
            dir = "d";
            ticTime = SnakeSpeed;
        }
        
    }

    /// <summary>
    /// Snake Motion
    /// </summary>
    void SnakeMovment()
    {
        ticTime++;
        if (ticTime >= SnakeSpeed)
        {
            for (int i = 0; i < BodyParts.Count; i++)
            {
                if (i > 0)
                    BodyParts[i].transform.position = BodyParts[i - 1].transform.position;
            }
            BodyParts[0].transform.position = transform.position;
            CheckDirection();
            ticTime = 0;
        }
    }

    /// <summary>
    ///update snake position dependson Chosen direction
    /// </summary>
    void CheckDirection()
    {
        switch (dir)
        {
            case "w":
                TempPos.z++;
                break;
            case "s":
                TempPos.z --;
                break;
            case "a":
                TempPos.x --;
                break;
            case "d":
                TempPos.x ++;
                break;
        }
        transform.position = TempPos;
    }

    /// <summary>
    /// add new snake part if it eats an apple
    /// </summary>
    void appendToSnake()
    {
        GameObject Part = Instantiate(Resources.Load<GameObject>("Part")) as GameObject;
        if (!BodyParts.Contains(Part))
        {
            Debug.Log("here");
            BodyParts.Add(Part);
        }
    }

    /// <summary>
    /// Genrate Rando aplle positions
    /// </summary>
    void GenerateRandomDirection()
    {
        //dir= new random and call it in start
    }
}

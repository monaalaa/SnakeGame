using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnakeMotion : MonoBehaviour
{

    #region Fields
    public string dir = "w";
    public List<GameObject> BodyParts = new List<GameObject>();

    public float minDistance = 0.5f;

    public float RotationSpeed = 50;


    float dis;
    Transform currentBodyPart;
    Transform prevBodyPart;


    [SerializeField]
    int SnakeSpeed;

    int ticTime;
    Vector3 TempPos = new Vector3();
    #endregion


    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            AddSnakPart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<SnakeController>().SnakeState == SnakeStatus.SnakeMoving)
        {
            Inputs();
            SnakeMovment();
        }
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
            for (int i = BodyParts.Count-1; i >0; i--)
            {
                if (i > 0)
                {
                    currentBodyPart = BodyParts[i].transform;
                prevBodyPart = BodyParts[i - 1].transform;

                dis = Vector3.Distance(prevBodyPart.position, currentBodyPart.position);
                Vector3 newpos = prevBodyPart.position;
                newpos.y = BodyParts[0].transform.position.y;

                float T = Time.deltaTime * dis / minDistance * 13;

                if (T > 0.5f)
                    T = 0.5f;

             
                BodyParts[i].transform.position = Vector3.Slerp(currentBodyPart.position, newpos, T);
                //BodyParts[i - 1].transform.position;
                BodyParts[i].transform.rotation = Quaternion.Slerp(currentBodyPart.rotation, prevBodyPart.rotation, T);//BodyParts[i - 1].transform.position;


                 }
            }
            BodyParts[0].transform.position =  transform.position;
            CheckDirection();
            ticTime = 0;
        }
        transform.position = TempPos;
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
      
    }

    /// <summary>
    /// add new snake part if it eats an apple
    /// </summary>
   public void AddSnakPart()
    {
        GameObject Part = Instantiate(Resources.Load<GameObject>("Part"),BodyParts[BodyParts.Count-1].transform.position, BodyParts[BodyParts.Count - 1].transform.rotation) as GameObject;
        if (!BodyParts.Contains(Part))
        {
            if (BodyParts.Count >3)
                Part.tag = "obstacles";
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

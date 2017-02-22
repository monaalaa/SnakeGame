using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MotionController
{
    Swipe,
    Click
}
public class SnakeMotion : MonoBehaviour
{

    #region Fields
    public string dir;
    public List<GameObject> BodyParts = new List<GameObject>();

    public float minDistance = 0.3f;

    public float RotationSpeed = 50;

    internal static MotionController Motioncontrol = MotionController.Swipe;
    float dis;
    Transform currentBodyPart;
    Transform prevBodyPart;


    [SerializeField]
    int SnakeSpeed;

    int ticTime;
    Vector3 TempPos = new Vector3();


    #region SwipeFields
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    #endregion

    #endregion


    void Start()
    {
        RandomDirection();
        SnakeController.SnakeState = SnakeStatus.SnakeMoving;
        for (int i = 0; i < 2; i++)
        {
            AddSnakPart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SnakeController.SnakeState == SnakeStatus.SnakeMoving)
        {
             Swipe();
            //SwipeTouch();
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

    /// <summary>
    /// Controlmoving by swipe
    /// </summary>
    public void SwipeTouch()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    Debug.Log("up swipe");
                    dir = "w";
                }
                //swipe down
                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    Debug.Log("down swipe");
                    dir = "s";
                }
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    Debug.Log("left swipe");
                    dir = "a";
                }
                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    Debug.Log("right swipe");
                    dir = "d";

                }
            }
        }
    }

    public void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            //swipe upwards
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                Debug.Log("up swipe");
                dir = "w";
            }
            //swipe down
            if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                Debug.Log("down swipe");
                dir = "s";
            }
            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                Debug.Log("left swipe");
                dir = "a";
            }
            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                Debug.Log("right swipe");
                dir = "d";
            }
        }
    }


    void RandomDirection()
    {
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                dir = "w";
                break;

            case 1:
                dir = "s";
                break;
            case 2:
                dir = "d";
                break;
            case 3:
                dir = "a";
                break;
        }
    }
}

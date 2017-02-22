using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MotionController
{
    Swipe,
    Click,
    Arrows
}
public class SnakeMotion : MonoBehaviour
{

    #region Fields
    public string dir;
    public List<GameObject> BodyParts = new List<GameObject>();

    public float minDistance = 0.2f;

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

            if (Motioncontrol == MotionController.Swipe)
            {
                UIManager.Instance.ArrowPanle.SetActive(false);
                Swipe();
            }
            //SwipeTouch();
            else if (Motioncontrol == MotionController.Click)
            {
                ClickToMove();
                UIManager.Instance.ArrowPanle.SetActive(false);
            }

            else if (Motioncontrol == MotionController.Arrows)
                UIManager.Instance.ArrowPanle.SetActive(true);
            //ClickToMoveTouch();
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

                float time = Time.deltaTime * dis / minDistance * 40;

                if (time > 0.5f)
                    time = 0.5f;

             
                BodyParts[i].transform.position = Vector3.Slerp(currentBodyPart.position, newpos, time);
                BodyParts[i].transform.rotation = Quaternion.Slerp(currentBodyPart.rotation, prevBodyPart.rotation, time);


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
    /// <summary>
    /// Swipe by mous "for testing in unity editor"
    /// </summary>
    public void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //get start 2d touch point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //get ed 2d touch point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            //Up
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                dir = "w";
            }
            // down
            if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                dir = "s";
            }
            // left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                dir = "a";
            }
            // right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            { 
                dir = "d";
            }
        }
    }


    /// <summary>
    /// move Snake by click on spacific positoin on screen "for testing in unity editor"
    /// </summary>
    void ClickToMove()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //get mouse position
            Vector3 pos = Input.mousePosition;
            Ray ray = GameObject.FindObjectOfType<Camera>().ScreenPointToRay(pos);
            Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, 0));
            float distance;
            xy.Raycast(ray, out distance);

            //final world position
            pos = ray.GetPoint(distance);
            //Left
            if (pos.x < 0.0f && dir != "a" && dir != "d")
            {
                dir = "a";
            }
            //Right
            else if (pos.x > 0.0f && dir != "d" && dir != "a")
            {
                dir = "d";
            }
            //Up
            else if (pos.y > 0.0f && dir != "s" && dir != "w")
            {
                dir = "w";
            }
            //Down
            else if (pos.y < 0.0f && dir != "s" && dir != "w")
            {
                dir = "s";
            }
        }
    }
  
    /// <summary>
    /// move Snake by touching on spacific positoin on screen
    /// </summary>
    void ClickToMoveTouch()
    {

        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            Vector3 pos = t.position;
            Ray ray = GameObject.FindObjectOfType<Camera>().ScreenPointToRay(pos);
            Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, 0));
            float distance;
            xy.Raycast(ray, out distance);
            pos = ray.GetPoint(distance);

            //Left
            if (pos.x < 0.0f && dir != "a" && dir != "d")
            {
                dir = "a";
            }
            //Right
            else if (pos.x > 0.0f && dir != "d" && dir != "a")
            {
                dir = "d";
            }
            //Up
            else if (pos.y > 0.0f && dir != "s" && dir != "w")
            {
                dir = "w";
            }
            //Down
            else if (pos.y < 0.0f && dir != "s" && dir != "w")
            {
                dir = "s";
            }
        }
    }

    /// <summary>
    /// make sname move in random direction at the beginning
    /// </summary>
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

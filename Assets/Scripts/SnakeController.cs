using UnityEngine;
using System.Collections;

public enum SnakeStatus
{
    SnakeMoving,
    Snakedead
}
public class SnakeController : MonoBehaviour {

    public SnakeStatus SnakeState = SnakeStatus.SnakeMoving;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "obstacles")
        {
            SnakeState = SnakeStatus.Snakedead;
            Debug.Log("Game Over");
        }

        else if (collider.gameObject.tag == "Apple")
        {
            Destroy(collider.gameObject);
            FruitsController.Instance.GenerateApple();
            GetComponent<SnakeMotion>().AddSnakPart();
        }
    }
}

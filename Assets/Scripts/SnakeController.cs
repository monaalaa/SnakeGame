using UnityEngine;
using System.Collections;

public class SnakeController : MonoBehaviour {

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

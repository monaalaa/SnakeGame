using UnityEngine;
using System.Collections;

public class SoundSystem : MonoBehaviour {

    static SoundSystem _instance;

    public static SoundSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SoundSystem>();
            }
            return _instance;
        }


    }

    // Use this for initialization
    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            GetComponent<AudioSource>().Play();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

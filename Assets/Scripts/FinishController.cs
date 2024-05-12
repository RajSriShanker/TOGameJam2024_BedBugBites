using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    ChildManager childManager;
    GameController gameManager;
    SoundController soundController;

    public int childrenRequired;
    public int loadNextLevelDelay;

    public bool levelComplete;

    public string nextSceneName;

    // Start is called before the first frame update
    void Start()
    {
        childManager = GameObject.Find("Child Manager").GetComponent<ChildManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameController>();
        soundController = GameObject.Find("Sound Manager").GetComponent<SoundController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadNextLevel()
    { 
        yield return new WaitForSeconds(loadNextLevelDelay);
        gameManager.LoadScene(nextSceneName);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (childManager.numberOfChildren.Count == childrenRequired)
            {
                soundController.PlayWinAudio();
                other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                StartCoroutine(LoadNextLevel());
            }
            else
            {
                Debug.Log("Not enough children");
            }
        }
    }
}

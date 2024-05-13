using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    ChildManager childManager;
    GameController gameManager;
    SoundController soundController;
    Animator wifeAnimator;

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
        wifeAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (childManager.numberOfChildren.Count == childrenRequired)
        {
            wifeAnimator.SetBool("hasKids", true);
        }
        else
        {
            wifeAnimator.SetBool("hasKids", false);
        }
    }

    IEnumerator LoadNextLevel()
    { 
        yield return new WaitForSeconds(loadNextLevelDelay);
        soundController.StopWinAudio();
        gameManager.LoadScene(nextSceneName);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (childManager.numberOfChildren.Count == childrenRequired)
            {
                soundController.PlayWinAudio();
                wifeAnimator.SetBool("isWinning", true);
                other.GetComponent<Animator>().SetBool("isWinning", true);
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

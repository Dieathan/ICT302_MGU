using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreScript : MonoBehaviour {
    public SihouetteGameManagement sgm;
    public AudioClip success;
    public AudioClip fail;
    public AudioSource audioSource;

    int playerScore = 0;

    // Use this for initialization
    void Start () {

        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<TextMesh>().text = "Score: " + playerScore.ToString();
	}

    public int GetScore() { return playerScore; }

    public void SetScore(int val) { playerScore = val; }

    public void AddScore(int val) { playerScore += val; }

    public void PlaySuccessSound() { audioSource.PlayOneShot(success, 1.0f); }

    public void PlayFailSound() { audioSource.PlayOneShot(fail, 1.0f); }
}

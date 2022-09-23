using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerX : MonoBehaviour
{
    public List<GameObject> mTargetPrefabs;
    public TextMeshProUGUI mGameOverText;
    public TextMeshProUGUI mScoreText;
    public GameObject mTitleScreen;
    public Button mRestartButton; 

    private float mSpaceBetweenSquares = 2.5f;
    private float mMinValueX = -3.75f;           //  x value of the center of the left-most square
    private float mMinValueY = -3.75f;           //  y value of the center of the bottom-most square
    private float mSpawnRate = 2.0f;
    private bool mIsGameOver = false;
    private int mScore = 0;
    
    // Start the game, remove title screen, reset score, and adjust spawnRate based on difficulty button clicked
    public void StartGame(int _difficulty)
    {
        this.mSpawnRate /= _difficulty;
        this.mScore = 0;
        UpdateScore(mScore);
        this.mIsGameOver = false;
        mTitleScreen.SetActive(false);
        StartCoroutine(SpawnTarget());
    }

    // While game is active spawn a random target
    IEnumerator SpawnTarget()
    {
        while (!mIsGameOver)
        {
            yield return new WaitForSeconds(mSpawnRate);
            int index = Random.Range(0, mTargetPrefabs.Count);
            Instantiate(mTargetPrefabs[index], RandomSpawnPosition(), mTargetPrefabs[index].transform.rotation);
        }
    }

    // Generate a random spawn position based on a random index from 0 to 3
    private Vector3 RandomSpawnPosition()
    {
        float spawnPosX = mMinValueX + (RandomSquareIndex() * mSpaceBetweenSquares);
        float spawnPosY = mMinValueY + (RandomSquareIndex() * mSpaceBetweenSquares);
        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
        return spawnPosition;
    }

    // Generates random square index from 0 to 3, which determines which square the target will appear in
    private int RandomSquareIndex()
    {
        return Random.Range(0, 4);
    }

    // Update score with value from target clicked
    public void UpdateScore(int _score)
    {
        this.mScore += _score;
        mScoreText.text = "SCORE: " + mScore;
        return;
    }

    public bool isGameOver()
    {
        return this.mIsGameOver;
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        mGameOverText.gameObject.SetActive(true);
        mRestartButton.gameObject.SetActive(true);
        mIsGameOver = true;
        return;
    }

    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        return;
    }
}

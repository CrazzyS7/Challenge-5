using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TargetX : MonoBehaviour
{
    private float mSpaceBetweenSquares = 2.5f;   // the distance between the centers of squares on the game board
    private GameManagerX mGameManagerX;
    private float mMinValueX = -3.75f;           // the x value of the center of the left-most square
    private float mMinValueY = -3.75f;           // the y value of the center of the bottom-most square
    private Rigidbody mTargetRB;
    
    public ParticleSystem mExplosionPtcl;
    public float mTimeOnScreen = 2f;
    public int mPointValue;

    void Start()
    {
        this.mTargetRB = GetComponent<Rigidbody>();
        this.mGameManagerX = GameObject.Find("GameManager").GetComponent<GameManagerX>();

        transform.position = RandomSpawnPosition(); 
        StartCoroutine(RemoveObjectRoutine());  // begin timer before target leaves screen
        return;
    }

    // When target is clicked, destroy it, update score, and generate explosion
    private void OnMouseDown()
    {
        if (!mGameManagerX.isGameOver())
        {
            Destroy(gameObject);
            mGameManagerX.UpdateScore(mPointValue);
            Explosion();
        }
        return;       
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
    int RandomSquareIndex ()
    {
        int targetRange = 4;
        return Random.Range(0, targetRange);
    }


    // If target that is NOT the bad object collides with sensor, trigger game over
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sensor") && !gameObject.CompareTag("Bad"))
        {
            mGameManagerX.GameOver();
        }
        Destroy(gameObject);
        return;
    }

    // Display explosion particle at object's position
    private void Explosion()
    {
        Instantiate(mExplosionPtcl, transform.position, mExplosionPtcl.transform.rotation);
        return;
    }

    // After a delay, Moves the object behind background so it collides with the Sensor object
    IEnumerator RemoveObjectRoutine ()
    {
        if (!mGameManagerX.isGameOver())
        {
            float speed = 5.0f;
            yield return new WaitForSeconds(mTimeOnScreen);
            transform.Translate(Vector3.forward * speed, Space.World);
        }
    }
}

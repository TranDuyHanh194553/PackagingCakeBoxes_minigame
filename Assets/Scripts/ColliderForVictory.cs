using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderForVictory : MonoBehaviour
{
    private GameObject ballUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ballUI = GameObject.FindWithTag("Ball");
        if (other.CompareTag("BallCollider"))
        {
            Debug.Log("Arigatou!");
            ballUI.SetActive(false);
        }
    }
}

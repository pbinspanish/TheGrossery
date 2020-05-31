using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisionController : MonoBehaviour
{
    public EnemyController enemyController;

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            enemyController.isEnemyVisible = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            enemyController.isEnemyVisible = false;
        }
    }
}

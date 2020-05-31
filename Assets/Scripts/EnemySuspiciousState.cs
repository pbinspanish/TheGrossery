using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuspiciousState : EnemyAbstractState
{
    Vector2 lastKnownPosition;
    public override void EnterState(EnemyController enemy) {
        lastKnownPosition = enemy.Player.transform.position;
        enemy.agent.SetDestination(lastKnownPosition);
    }

    public override void Update(EnemyController enemy) {
        float distanceToTarget = Vector2.Distance(enemy.transform.position, lastKnownPosition);

        if (distanceToTarget < 0.5) {
            // Destination Reached
            // TODO: Looking around animation
        }

        if (enemy.isEnemyVisible) {
            enemy.suspicion += enemy.suspicionSuspiciousIncrement * Time.deltaTime;

            if (enemy.suspicion >= 100) {
                Debug.Log("Alert!");
                enemy.TransitionToState(enemy.AlertState);
            }

            // Move towards them
            lastKnownPosition = enemy.Player.transform.position;
            enemy.agent.SetDestination(lastKnownPosition);
        }
        else {
            if (enemy.suspicion <= 50) {
                // Begin searching if not suspicious enough
                enemy.TransitionToState(enemy.SearchState);
            }
            else {
                enemy.suspicion -= enemy.suspicionDecrement * Time.deltaTime;
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D other, EnemyController enemy) {
        
    }
}

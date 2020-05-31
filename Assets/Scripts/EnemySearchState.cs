using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearchState : EnemyAbstractState
{
    Vector2 randomSearchSpot;
    public override void EnterState(EnemyController enemy) {
        enemy.suspicion -= 30;
        SetRandomSearchSpot(enemy);
        enemy.agent.SetDestination(randomSearchSpot);
    }

    public override void Update(EnemyController enemy) {
        float distanceToTarget = Vector2.Distance(enemy.transform.position, randomSearchSpot);

        if (distanceToTarget < 0.5) {
            // Destination Reached
            SetRandomSearchSpot(enemy);
        }

        if (enemy.isEnemyVisible) {
            if (enemy.suspicion >= 100) {
                enemy.TransitionToState(enemy.AlertState);
            }
            else {
                enemy.suspicion += enemy.suspicionSuspiciousIncrement * Time.deltaTime * 2;
            }
        }
        else {
            if (enemy.suspicion <= 20) {
                // Begin searching if not suspicious enough
                enemy.TransitionToState(enemy.IdleState);
            }
            else {
                enemy.suspicion -= enemy.suspicionDecrement * Time.deltaTime;
            }
        }
    }

    private void SetRandomSearchSpot(EnemyController enemy) {
        if (Random.Range(0, 10) > 5) {
            if (Random.Range(0, 10) > 5) {
                randomSearchSpot = new Vector2(enemy.transform.position.x + Random.Range(2, 5), enemy.transform.position.y + Random.Range(2, 5));
            }
            else {
                randomSearchSpot = new Vector2(enemy.transform.position.x + Random.Range(2, 5), enemy.transform.position.y - Random.Range(2, 5));
            }
        }
        else {
            if (Random.Range(0, 10) > 5) {
                randomSearchSpot = new Vector2(enemy.transform.position.x - Random.Range(2, 5), enemy.transform.position.y + Random.Range(2, 5));
            }
            else {
                randomSearchSpot = new Vector2(enemy.transform.position.x - Random.Range(2, 5), enemy.transform.position.y - Random.Range(2, 5));
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D other, EnemyController enemy) {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyAbstractState
{

    public override void EnterState(EnemyController enemy) {
        enemy.CurrentPath = 0;
        enemy.MaxPath = enemy.Path.Count - 1;
        enemy.agent.SetDestination(enemy.Path[0]);
    }

    public override void Update(EnemyController enemy) {
        #region Walk Cycle

        float distanceToTarget = Vector2.Distance(enemy.transform.position, enemy.Path[enemy.CurrentPath]);

        if (distanceToTarget < 0.5) {
            // Reached its destination, now it can move on
            if (enemy.CurrentPath == enemy.MaxPath) {
                enemy.CurrentPath = 0;
            }
            else {
                enemy.CurrentPath += 1;
            }
            enemy.agent.SetDestination(enemy.Path[enemy.CurrentPath]);
        }

        #endregion

        #region Suspicion & Suspicious State Change

        if (enemy.isEnemyVisible) {
            enemy.suspicion += enemy.suspicionIdleIncrement * Time.deltaTime;
            if (enemy.suspicion >= 50) {
                switch (enemy.level) {
                    case 1:
                        enemy.source.PlayOneShot(enemy.alert1);
                        break;

                    case 2:
                        enemy.source.PlayOneShot(enemy.alert2);
                        break;

                    case 3:
                        enemy.source.PlayOneShot(enemy.alert3);
                        break;

                    default:
                    break;
                }
                enemy.TransitionToState(enemy.SuspiciousState);
            }
        }
        else {
            if (enemy.suspicion <= 0) {
                // don't do anything, don't want it dipping below 0
            }
            else {
                enemy.suspicion -= enemy.suspicionDecrement * Time.deltaTime;
            }
        }

        #endregion
    }

    public override void OnTriggerEnter2D(Collider2D other, EnemyController enemy) {
        if (other.tag == "vaccine") {
            enemy.TransitionToState(enemy.CuredState);
        }
    }
}
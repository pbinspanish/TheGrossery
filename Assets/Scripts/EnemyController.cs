using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class EnemyController : MonoBehaviour
{
    #region Properties

    // Path, array of coordinates, walks between them in order
    public List<Vector2> Path = new List<Vector2>();
    public int CurrentPath;
    public int MaxPath;


    // Level (1,2,3)
    public int level = 1;
    public float basicMovementSpeed = 1.0f;
    public float mediumMovementSpeed = 1.5f;
    public float advancedMovementSpeed = 2.0f;


    // Detection
    public GameObject vision;
    public bool isEnemyVisible = false;

    public float suspicion = 0;
    public float suspicionIdleIncrement = 15;
    public float suspicionSuspiciousIncrement = 30;
    public float suspicionDecrement = 4;


    // Miscellaneous
    public GameObject Player;
    public NavMeshAgent agent;
    private EnemyAbstractState currentState;
    public Tilemap map;
    Vector3 pos, velocity;
    int dir = 0;

    // States
    public readonly EnemyIdleState IdleState = new EnemyIdleState();
    public readonly EnemySuspiciousState SuspiciousState = new EnemySuspiciousState();
    public readonly EnemyAlertState AlertState = new EnemyAlertState();
    public readonly EnemySearchState SearchState = new EnemySearchState();
    public readonly EnemyCuredState CuredState = new EnemyCuredState();

    // Sounds
    public AudioClip alert1;
    public AudioClip alert2;
    public AudioClip alert3;

    public AudioSource source;

    #endregion
    
    public void TransitionToState(EnemyAbstractState state) {
        currentState = state;
        currentState.EnterState(this);
    }

    // sets the state to idle by default
    void Start()
    {
        GetComponent<Animator>().SetInteger("diff", level);

        switch (level) {
            case 1:
                GetComponent<NavMeshAgent>().speed = basicMovementSpeed;
                this.tag = "covid1";
                break;

            case 2:
                GetComponent<NavMeshAgent>().speed = mediumMovementSpeed;
                this.tag = "covid2";
                break;
                
            case 3:
                GetComponent<NavMeshAgent>().speed = advancedMovementSpeed;
                this.tag = "covid3";
                break;
                
            default:
                break;
        }

		agent.updateRotation = false;
		agent.updateUpAxis = false;

        CurrentPath = 0;
        MaxPath = Path.Count - 1;
        TransitionToState(IdleState);
    }

    // Passes Update to the state
    void Update()
    {
        #region Determining Enemy Direction

        Vector2 newPos = transform.position;
        velocity = (transform.position - pos) / Time.deltaTime;
        pos = transform.position;

        // Angle the enemy is facing in degrees
        float angle = (Mathf.Atan2(velocity.normalized.y, velocity.normalized.x)) * (180 / Mathf.PI);

        // 0 = UP, 1 = DOWN, 2 = LEFT, 3 = RIGHT

        if (45 <= angle && angle <= 135) {
            // UP
            dir = 0;
            vision.GetComponent<Transform>().transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (-135 <= angle && angle <= -45) {
            // DOWN
            dir = 1;
            vision.GetComponent<Transform>().transform.eulerAngles = new Vector3(0, 0, 270);
        }
        else if ((-180 <= angle && angle <= -135) || (135 <= angle && angle <= 180)) {
            // LEFT
            dir = 2;
            vision.GetComponent<Transform>().transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (-45 <= angle && angle <= 45) {
            // RIGHT
            dir = 3;
            vision.GetComponent<Transform>().transform.eulerAngles = new Vector3(0, 0, 0);
        }

        #endregion

        currentState.Update(this);
    }

    private void FixedUpdate() {
        GetComponent<Animator>().SetInteger("dir", dir);
    }

    void Awake()
    {
        pos = transform.position;

        source = this.GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        currentState.OnTriggerEnter2D(other, this);
    }
}

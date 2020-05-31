using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterTest : MonoBehaviour
{
    public NavMeshAgent agent;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        var agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(new Vector3(6.0f, 7.0f));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;

    NavMeshAgent navMeshAgent;

	private void Start()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	void Update()
    {
        navMeshAgent.SetDestination(target.position);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
	Vector3 targetPos;

	private void Start()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
		targetPos = transform.position;
	}

	void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			MoveToCursor();
		}

		Debug.DrawLine(transform.position, targetPos, Color.yellow);
	}

	void MoveToCursor()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;
		bool hasHit = Physics.Raycast(ray, out hit);

		if (hasHit)
		{
			targetPos = hit.point;
			navMeshAgent.SetDestination(targetPos);
		}
	}
}

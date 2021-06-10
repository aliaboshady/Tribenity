using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
	Animator animator;
	Vector3 targetPos;

	private void Start()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		targetPos = transform.position;
	}

	void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			MoveToCursor();
		}

		UpdateAnimator();

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

	private void UpdateAnimator()
	{
		Vector3 navMeshLocalVelocity = transform.InverseTransformDirection(navMeshAgent.velocity);
		animator.SetFloat("forwardSpeed", navMeshLocalVelocity.z);
	}
}

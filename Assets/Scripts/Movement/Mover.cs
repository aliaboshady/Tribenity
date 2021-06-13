using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
	public class Mover : MonoBehaviour, IAction
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
			UpdateAnimator();
			Debug.DrawLine(transform.position, targetPos, Color.yellow);
		}

		public void StartMoveAction(Vector3 destination)
		{
			GetComponent<ActionScheduler>().StartAction(this);
			MoveTo(destination);
		}

		public void MoveTo(Vector3 destination)
		{
			targetPos = destination;
			navMeshAgent.SetDestination(destination);
			navMeshAgent.isStopped = false;
		}
		
		public void Cancel()
		{
			navMeshAgent.isStopped = true;
		}

		private void UpdateAnimator()
		{
			Vector3 navMeshLocalVelocity = transform.InverseTransformDirection(navMeshAgent.velocity);
			animator.SetFloat("forwardSpeed", navMeshLocalVelocity.z);
		}
	}
}
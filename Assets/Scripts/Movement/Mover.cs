using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
	public class Mover : MonoBehaviour, IAction
	{
		[SerializeField] float maxSpeed = 6f;
		NavMeshAgent navMeshAgent;
		Animator animator;
		Vector3 targetPos;
		Health health;

		private void Start()
		{
			navMeshAgent = GetComponent<NavMeshAgent>();
			animator = GetComponent<Animator>();
			targetPos = transform.position;
			health = GetComponent<Health>();
		}

		void Update()
		{
			navMeshAgent.enabled = !health.IsDead();

			UpdateAnimator();
			Debug.DrawLine(transform.position, targetPos, Color.yellow);
		}

		public void StartMoveAction(Vector3 destination, float speedFraction)
		{
			GetComponent<ActionScheduler>().StartAction(this);
			MoveTo(destination, speedFraction);
		}

		public void MoveTo(Vector3 destination, float speedFraction)
		{
			targetPos = destination;
			navMeshAgent.SetDestination(destination);
			navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
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
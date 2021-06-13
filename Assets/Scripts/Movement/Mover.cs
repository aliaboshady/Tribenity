using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
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
			UpdateAnimator();
			Debug.DrawLine(transform.position, targetPos, Color.yellow);
		}

		public void MoveTo(Vector3 destination)
		{
			targetPos = destination;
			navMeshAgent.SetDestination(destination);
		}

		private void UpdateAnimator()
		{
			Vector3 navMeshLocalVelocity = transform.InverseTransformDirection(navMeshAgent.velocity);
			animator.SetFloat("forwardSpeed", navMeshLocalVelocity.z);
		}
	}
}
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using System;

namespace RPG.Control
{
	public class AIController : MonoBehaviour
	{
		[SerializeField] float chaseDistance = 5f;
		[SerializeField] float suspicionTime = 2f;
		[SerializeField] float patrolWaitTime = 2f;
		[SerializeField] float waypointTolerence = 1f;
		[SerializeField, Range(0, 1)] float patrolSpeedFraction = 0.2f;
		[SerializeField] PatrolPath patrolPath;

		Fighter fighter;
		GameObject player;
		Health health;
		Mover mover;
		ActionScheduler actionScheduler;

		Vector3 guardPosition;
		int currentWaypointIndex = 0;
		float timeSinceLastSawPlayer = Mathf.Infinity;
		float timeSinceArrivedAtWaypoint = Mathf.Infinity;

		private void Start()
		{
			fighter = GetComponent<Fighter>();
			player = GameObject.FindWithTag("Player");
			health = GetComponent<Health>();
			mover = GetComponent<Mover>();
			actionScheduler = GetComponent<ActionScheduler>();

			guardPosition = transform.position;
		}

		private void Update()
		{
			if (health.IsDead()) return;

			if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
			{
				AttackBehavior();
			}
			else if (timeSinceLastSawPlayer <= suspicionTime)
			{
				SuspicionBehavior();
			}
			else
			{
				PatrolBehavior();
			}

			UpdateTimers();
		}

		private void UpdateTimers()
		{
			timeSinceLastSawPlayer += Time.deltaTime;
			timeSinceArrivedAtWaypoint += Time.deltaTime;
		}

		private void PatrolBehavior()
		{
			Vector3 nextPosition = guardPosition;
			if(patrolPath != null)
			{
				if (AtWaypoint())
				{
					timeSinceArrivedAtWaypoint = 0;
					CycleWaypoint();
				}
				nextPosition = GetCurrentWaypoint();
			}

			if(timeSinceArrivedAtWaypoint >= patrolWaitTime)
			{
				mover.StartMoveAction(nextPosition, patrolSpeedFraction);
			}
		}

		private bool AtWaypoint()
		{
			return Vector3.SqrMagnitude(transform.position - GetCurrentWaypoint()) <= waypointTolerence * waypointTolerence;
		}

		private Vector3 GetCurrentWaypoint()
		{
			return patrolPath.GetWaypoint(currentWaypointIndex);
		}

		private void CycleWaypoint()
		{
			currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
		}

		private void SuspicionBehavior()
		{
			actionScheduler.CancelCurrentAction();
		}

		private void AttackBehavior()
		{
			fighter.Attack(player);
			timeSinceLastSawPlayer = 0;
		}

		private bool InAttackRangeOfPlayer()
		{
			if (player == null) return false;
			return Vector3.SqrMagnitude(player.transform.position - transform.position) <= chaseDistance * chaseDistance;
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(transform.position, chaseDistance);
		}
	}
}

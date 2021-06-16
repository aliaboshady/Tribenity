using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
	public class AIController : MonoBehaviour
	{
		[SerializeField] float chaseDistance = 5f;
		[SerializeField] float suspicionTime = 2f;

		Fighter fighter;
		GameObject player;
		Health health;
		Mover mover;
		ActionScheduler actionScheduler;

		Vector3 guardLocation;
		float timeSinceLastSawPlayer = Mathf.Infinity;

		private void Start()
		{
			fighter = GetComponent<Fighter>();
			player = GameObject.FindWithTag("Player");
			health = GetComponent<Health>();
			mover = GetComponent<Mover>();
			actionScheduler = GetComponent<ActionScheduler>();

			guardLocation = transform.position;
		}

		private void Update()
		{
			if (health.IsDead()) return;

			if (InAttackRangeOfPlayer()  && fighter.CanAttack(player))
			{
				AttackBehavior();
				timeSinceLastSawPlayer = 0;
			}
			else if (timeSinceLastSawPlayer <= suspicionTime)
			{
				SuspicionBehavior();
			}
			else
			{
				GuardBehavior();
			}

			timeSinceLastSawPlayer += Time.deltaTime;
		}

		private void GuardBehavior()
		{
			mover.StartMoveAction(guardLocation);
		}

		private void SuspicionBehavior()
		{
			actionScheduler.CancelCurrentAction();
		}

		private void AttackBehavior()
		{
			fighter.Attack(player);
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

using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
	{
		[SerializeField] float weaponDamage = 5f;
		[SerializeField] float weaponRange = 2f;
		[SerializeField] float timeBetweenAttacks = 1f;

        Transform target;
		Mover mover;
		Animator animator;
		float timeSinceLastAttack;

		private void Start()
		{
			mover = GetComponent<Mover>();
			animator = GetComponent<Animator>();
			timeSinceLastAttack = timeBetweenAttacks;
		}

		private void Update()
		{
			timeSinceLastAttack += Time.deltaTime;

			if (target == null) return;

			if(!GetIsInRange())
			{
				mover.MoveTo(target.position);
			}
			else
			{
				AttackBehavior();
				mover.Cancel();
			}
		}

		void AttackBehavior()
		{
			if (timeSinceLastAttack >= timeBetweenAttacks)
			{
				//This will trigger Hit() event
				animator.SetTrigger("attack");
				timeSinceLastAttack = 0;
			}
		}

		private bool GetIsInRange()
		{
			return Vector3.SqrMagnitude(target.position - transform.position) <= weaponRange * weaponRange;
		}

		public void Attack(CombatTarget combatTarget)
		{
			GetComponent<ActionScheduler>().StartAction(this);
			target = combatTarget.transform;
		}

		public void Cancel()
		{
			target = null;
		}

		//Animation Event
		void Hit()
		{
			Health targetHealth = target.GetComponent<Health>();
			targetHealth.TakeDamage(weaponDamage);
		}
	}
}
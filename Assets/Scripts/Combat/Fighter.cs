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

        Health target;
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
			if (target.IsDead()) return;

			if(!GetIsInRange())
			{
				mover.MoveTo(target.transform.position);
			}
			else
			{
				AttackBehavior();
				mover.Cancel();
			}
		}

		void AttackBehavior()
		{
			transform.LookAt(target.transform);
			if (timeSinceLastAttack >= timeBetweenAttacks)
			{
				//This will trigger Hit() event
				animator.SetTrigger("attack");
				timeSinceLastAttack = 0;
			}
		}

		private bool GetIsInRange()
		{
			return Vector3.SqrMagnitude(target.transform.position - transform.position) <= weaponRange * weaponRange;
		}

		public void Attack(CombatTarget combatTarget)
		{
			GetComponent<ActionScheduler>().StartAction(this);
			target = combatTarget.GetComponent<Health>();
		}

		public void Cancel()
		{
			target = null;
			animator.SetTrigger("stopAttack");
		}

		//Animation Event
		void Hit()
		{
			if (target == null) return;
			target.TakeDamage(weaponDamage);
		}
	}
}
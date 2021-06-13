using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
	{
		[SerializeField] float weaponRange = 2f;
        Transform target;
		Mover mover;
		Animator animator;

		private void Start()
		{
			mover = GetComponent<Mover>();
			animator = GetComponent<Animator>();
		}

		private void Update()
		{
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
			animator.SetTrigger("attack");
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

		}
    }
}
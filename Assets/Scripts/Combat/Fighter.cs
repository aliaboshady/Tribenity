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

		private void Start()
		{
			mover = GetComponent<Mover>();
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
				mover.Cancel();
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
    }
}
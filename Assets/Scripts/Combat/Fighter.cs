using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
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
				Cancel();
				mover.Stop();
			}
		}

		private bool GetIsInRange()
		{
			return Vector3.SqrMagnitude(target.position - transform.position) <= weaponRange * weaponRange;
		}

		public void Attack(CombatTarget combatTarget)
		{
			target = combatTarget.transform;
		}

		public void Cancel()
		{
			target = null;
		}
    }
}
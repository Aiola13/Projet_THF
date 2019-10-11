using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VolumicVR.Utils
{
	public class TriggerAnimator : MonoBehaviour
	{
		[SerializeField] private string     _playerTag              = "Player";
		[SerializeField] private Animator	_pcScreenAnimator;
		[SerializeField] private string     _computerUiParameter;


		#region UNITY EVENTS

		private void OnTriggerEnter ( Collider other )
		{
			if ( other.tag == _playerTag )
				_pcScreenAnimator.SetBool ( _computerUiParameter, true );
		}

		private void OnTriggerExit ( Collider other )
		{
			if ( other.tag == _playerTag )
				_pcScreenAnimator.SetBool ( _computerUiParameter, false );
		}

		#endregion
	}
}
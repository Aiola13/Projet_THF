using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace VolumicVR
{
	public class Printer : MonoBehaviour
	{
		#region INTERNAL EVENTS

		[HideInInspector] public UnityEvent StartPrintingObject;
		[HideInInspector] public UnityEvent OnPrintEnd;

		#endregion


		#region INSPECTOR FIELDS

		[Header("ANIMATOR PARAMETERS")]
		[SerializeField] private Animator	_animator;
		[SerializeField] private string     _startGlobalAnimParamName;
		[SerializeField] private string     _startPrintingParamName;
		[SerializeField] private string     _stopPrintingParamName;
		[SerializeField] private string     _OnPrintEndedParamName;

		[Header("Vertical Anim Parameters")]
		[SerializeField] private float      _originalAnimTime;
		[SerializeField] private string     _verticalTimeMultiplierParamName;

		[Header("Grabbale System")]
		[SerializeField] private GameObject[]	_notGrabbableTr;
		[SerializeField] private GameObject		_grabbablesTr;

		#endregion


		#region PRIVATE FIELDS

		private bool _isPrinting;

		#endregion


		#region UNITY EVENTS

		private void Start ()
		{
			if ( StartPrintingObject == null )
				StartPrintingObject = new UnityEvent ();

			if ( OnPrintEnd == null )
				OnPrintEnd = new UnityEvent ();
		}

		#endregion


		#region PRINTER BEHAVIOURS

		private float _actuelObjectPrintTime;


		public void SetTimeMultiplier ( float multiplier )
		{
			_animator.SetFloat ( _verticalTimeMultiplierParamName, _originalAnimTime / _actuelObjectPrintTime * multiplier );
		}

		public void StartGlobalAnim ( float printTime )
		{
			_actuelObjectPrintTime = printTime;

			_animator.SetFloat ( _verticalTimeMultiplierParamName, _originalAnimTime / _actuelObjectPrintTime );

			_animator.SetTrigger ( _startGlobalAnimParamName );
		}

		public void StartPrint ()
		{
			if ( _isPrinting )
				return;

			//Activate VERTICAL layer
			//_animator.SetLayerWeight ( _animator.GetLayerIndex ( "Vertical" ), 1 );

			StartPrintingObject.Invoke ();

			_animator.SetTrigger ( _startPrintingParamName );

			_isPrinting = true;
		}

		public void StopPrinting ()
		{
			_animator.SetTrigger ( _stopPrintingParamName );

			//Deactivate VERTICAL layer
			//_animator.SetLayerWeight ( _animator.GetLayerIndex ( "Vertical" ), 0 );
		}

		public void OnPrintEnded ()
		{
			_isPrinting = false;

			_animator.SetTrigger ( _OnPrintEndedParamName );
			OnPrintEnd.Invoke ();
		}

		public void ActivateGrabbables ( bool value )
		{
			foreach ( GameObject go in _notGrabbableTr )
				go.SetActive ( !value );

			_grabbablesTr.SetActive ( value );
		}

		#endregion
	}
}
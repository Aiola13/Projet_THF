using System.Collections;
using UnityEngine;
using VRTK;


namespace VolumicVR
{
	public class STLBehaviours : MonoBehaviour
	{
		#region INSPECTOR FIELDS

		[Header("Stand Gameobject parameters")]
		[SerializeField] private Rigidbody      _rigidbody;
		[SerializeField] private Collider       _collider;

		[Header ("STL Object")]
		[SerializeField] private GameObject		_printedObject;
		[SerializeField] private float          _printedObjectWorldHeigth;

		[Header("Materials")]
		[SerializeField] private Material       _cuttingMat;
		[SerializeField] private Material       _standardMat;

		[Header ("Printing Parameters")]
		[SerializeField] private string         _shaderWorldHeightValueParamName        = "_Val";
		[Tooltip("Printing time in seconds")]
		[SerializeField] private float          _printTime;
		[Range ( 1, 20 )]
		/*[SerializeField]*/ private float		_timeMultiplier                         = 1;
		[SerializeField] private int            _printingStepsNum                       = 60;

		#endregion


		#region PRIVATE FIELDS

		private float						_minCuttingWorldHeightValue;

		private Coroutine					_printingCoroutine;
		private MeshRenderer				_printedObjRenderer;

		private VRTK_InteractableObject     _interactable;

		private int                         _actualStep;

		#endregion


		#region UNITY EVENTS

		private void Start ()
		{
			Init ();
		}

		#endregion


		#region PUBLIC METHODS

		//To be able to test the print process using the context menu in the inspector
		[ContextMenu ( "Print" )]
		public void Print ()
		{
			_minCuttingWorldHeightValue = _printedObject.transform.position.y;

			_printedObjRenderer.material = _cuttingMat;
			_printedObjRenderer.material.SetFloat ( _shaderWorldHeightValueParamName, _minCuttingWorldHeightValue );

			_rigidbody.isKinematic = true;
			_collider.enabled = false;
			_interactable.isGrabbable = false;

			_printingCoroutine = StartCoroutine ( StartPrinting () );
		}

		public int GetTotalPrintSteps ()
		{
			return _printingStepsNum;
		}

		public int GetActualPrintStep ()
		{
			return _actualStep;
		}

		public float GetPrintTime ()
		{
			return _printTime;
		}

		public void SetTimeMultiplier ( float value = 1 )
		{
			_timeMultiplier = value;
		}

		#endregion


		#region PRIVATE METHODS

		private void Init ()
		{
			_printedObjRenderer = _printedObject.GetComponent<MeshRenderer> ();
			_printedObjRenderer.material = _standardMat;

			_interactable = this.GetComponent<VRTK_InteractableObject> ();

			if ( _interactable == null )
				Debug.LogError ( "Interactable Object is not setup !" );
		}
		

		private IEnumerator StartPrinting ()
		{
			_actualStep = 0;

			do
			{
				_printedObjRenderer.material.SetFloat ( _shaderWorldHeightValueParamName, _minCuttingWorldHeightValue + _printedObjectWorldHeigth / _printingStepsNum * _actualStep );

				yield return new WaitForSeconds ( _printTime / _printingStepsNum / _timeMultiplier );

				_actualStep++;
			}
			while ( _actualStep <= _printingStepsNum );


			_printedObjRenderer.material = _standardMat;
			_rigidbody.isKinematic = false;
			_collider.enabled = true;
			_interactable.isGrabbable = true;
		}

		#endregion
	}
}
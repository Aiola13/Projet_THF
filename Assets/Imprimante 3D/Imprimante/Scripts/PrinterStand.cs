using System;
using System.Collections;
using UnityEngine;


namespace VolumicVR
{
	public enum PrintedObject
	{
		None		= 0,
		Chat		= 1,
		Maoi		= 2,
		OctoStylo	= 3,
		StarVase	= 4,
		Vase		= 5
	}

	public class PrinterStand : MonoBehaviour, IPrinterStand
	{
		#region EVENTS

		public event EventHandler PrintEndedEvent;

		#endregion


		#region PROPERTIES

		public PrintedObject	ObjectToPrint			{ get { return objectToPrint; } set { objectToPrint = value; } }
		public GameObject		ActualPrintedSTL		{ get { return _printedObject; } }

		public float			ActualSTLPrintTime		{ get { return _printedObjectBehaviour.GetPrintTime (); } }

		public int				ActualSTLTotalSteps		{ get { return _printedObjectBehaviour.GetTotalPrintSteps (); } }
		public int				ActualSTLCurrentStep	{ get { return _printedObjectBehaviour.GetActualPrintStep (); } }

		public float			TimeMultiplier			{ get { return _timeMultiplier; } set { _timeMultiplier = value; } }

		#endregion


		#region INSPECTOR FIELDS

		[Header("Printer")]
		[SerializeField] private Printer		_printer;
		[SerializeField] private Transform      _printSpot;

		[Header("STL to print")]
		public PrintedObject                    objectToPrint;
		[Range(1,100)]
		[SerializeField] private float          _timeMultiplier		= 1;

		[Header("STLs")]
		[SerializeField] private GameObject		_chat;
		[SerializeField] private GameObject		_moai;
		[SerializeField] private GameObject		_octoStylo;
		[SerializeField] private GameObject		_starVase;
		[SerializeField] private GameObject		_vase;

		[Space()]
		[SerializeField] private Transform      _grabbableTray;

		#endregion


		#region PRIVATE FIELDS

		private GameObject      _printedObject;
		private STLBehaviours   _printedObjectBehaviour;

		private float           _lastTimeMultiplier;

		#endregion


		#region UNITY EVENTS

		private void Start ()
		{
			_printer.ActivateGrabbables ( true );

			_printer.StartPrintingObject.AddListener ( StartObjectPrint );
			_printer.OnPrintEnd.AddListener ( OnPrintEnd );
		}

		private void Update ()
		{
			SetTimeMultiplier ();
		}

		#endregion


		#region IMPLEMENTATION IPrinterStand

		[ContextMenu ( "StartPrinting" )]
		public void StartPrinting ()
		{
			_printer.ActivateGrabbables ( false );

			_printedObject = Instantiate ( GetAccordingSTL ( objectToPrint ), _printSpot.position, Quaternion.identity, _printSpot );
			_printedObjectBehaviour = _printedObject.GetComponent<STLBehaviours> ();

			//Récupérer temps d'impression de la pièce et le recalquer sur l'anim de hauteur
			_printer.StartGlobalAnim ( _printedObjectBehaviour.GetPrintTime () );

			_printedObject.SetActive ( false );
		}

		public void SetSTLToPrint ( int printedObjectNum )
		{
			objectToPrint = (PrintedObject)printedObjectNum;
		}

		public void SetSTLToPrint ( PrintedObject printedObject )
		{
			objectToPrint = printedObject;
		}

		public void PausePrint ()
		{
			Debug.Log ( "Unimplemented funcionnalitie : PausePrint ()" );
		}

		public void UnpausePrint ()
		{
			Debug.Log ( "Unimplemented funcionnalitie : UnpausePrint ()" );
		}

		#endregion


		#region PRINTER BEHAVIOURS


		public void StartObjectPrint ()
		{
			StartCoroutine ( StartObjectPrintCoroutine () );
		}

		private IEnumerator StartObjectPrintCoroutine ()
		{
			_printedObject.SetActive ( true );

			yield return new WaitForFixedUpdate ();

			_printedObjectBehaviour.Print ();
		}

		private void OnPrintEnd ()
		{
			_printer.ActivateGrabbables ( true );

			_printedObject.transform.parent = _grabbableTray;

			PrintEndedEvent?.Invoke ( this, EventArgs.Empty );
		}

		private void SetTimeMultiplier ()
		{
			if ( _timeMultiplier == _lastTimeMultiplier )
				return;

			if ( _printer != null )
				_printer.SetTimeMultiplier ( _timeMultiplier );

			if ( _printedObjectBehaviour != null )
				_printedObjectBehaviour.SetTimeMultiplier ( _timeMultiplier );

			_lastTimeMultiplier = _timeMultiplier;
		}

		private GameObject GetAccordingSTL ( PrintedObject pObj )
		{
			switch ( pObj )
			{
				case PrintedObject.Chat:
					return _chat;

				case PrintedObject.Maoi:
					return _moai;

				case PrintedObject.OctoStylo:
					return _octoStylo;

				case PrintedObject.StarVase:
					return _starVase;

				case PrintedObject.Vase:
					return _vase;

				case PrintedObject.None:
				default:
					Debug.LogError ( pObj.ToString () + " doe not exists !" );
					return null;
			}
		}

		#endregion
	}
}
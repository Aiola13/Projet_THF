using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;


namespace VolumicVR
{
	public class GameManager : MonoBehaviour
	{
		#region INSPECTOR FIELDS

		[Header("DEBUG")]
		[Tooltip("Active le player VR dans tous les cas")]
		[SerializeField] private bool _debugVR;

		[Header("Player Gameobjects")]
		[SerializeField] private GameObject _playerDT;
		[SerializeField] private GameObject _playerVR;

		#endregion


		#region UNITY EVENTS

		private void Start ()
		{
			Init ();
		}

		#endregion

		private void Init ()
		{
			if ( XRDevice.isPresent || _debugVR )
				_playerDT.gameObject.SetActive ( false );
			else
				_playerVR.gameObject.SetActive ( false );
		}
	}
}
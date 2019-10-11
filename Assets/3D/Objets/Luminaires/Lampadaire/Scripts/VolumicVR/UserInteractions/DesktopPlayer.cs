using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VolumicVR.UserInteractions
{
	class DesktopPlayer : Player
	{
		[Header( "DEBUG" )]
		[SerializeField] private bool       _debug			= false;

		[Header( "Setup" )]
		[SerializeField] private Camera		_camera;


		// Start is called before the first frame update
		void Start ()
		{

		}

		void Update ()
		{
			FrontRaycast ();
		}


		private void FrontRaycast ()
		{
			Ray ray = _camera.ScreenPointToRay ( new Vector3 ( Screen.width * .5f, Screen.height * .5f, 100 ) );

			if ( _debug )
				Debug.DrawRay ( ray.origin, ray.direction, Color.red );

			if ( Physics.Raycast ( ray, out RaycastHit hitInfos ) )
			{
				//Debug.Log ( hitInfos.collider.name );
			}
		}
	}
}
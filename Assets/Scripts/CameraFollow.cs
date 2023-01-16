using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform carTransform;
	[Range(1, 10)]
	public float followSpeed = 2;
	[Range(1, 10)]
	public float lookSpeed = 5;
	Vector3 initialCameraPosition;
	Vector3 initialCarPosition;
	Vector3 absoluteInitCameraPosition;
	GameObject[] playerslist=null;
	void Start(){

		
		initialCameraPosition = gameObject.transform.position;
		initialCarPosition = carTransform.position;
		absoluteInitCameraPosition = initialCameraPosition - initialCarPosition;
		StartCoroutine(check());
	}
	IEnumerator check()
	{
		yield return new WaitForSeconds(5);

		playerslist = GameObject.FindGameObjectsWithTag("Player");
		//Debug.Log("tombstone: " + playerslist.Length);
	}
	public void SetPlayerBot(GameObject player)
	{

		carTransform = player.transform;
		//GetPlayerCar();

	}
	public void SetPlayerBotTransform(Transform player)
	{

		carTransform = player;
		//GetPlayerCar();

	}
	void FixedUpdate()
	{
		if (playerslist != null)
		{
			if (playerslist.Length == 2)
			{
				var centerX = 0f;
				var centerZ = 0f;
				centerX = (playerslist[0].transform.position.x + playerslist[1].transform.position.x) / 2;
				centerZ = (playerslist[0].transform.position.z + playerslist[1].transform.position.z) / 2;

				carTransform.position = new Vector3(centerX, 0, centerZ);
			}
		}
		//Look at car
		Vector3 _lookDirection = (new Vector3(carTransform.position.x, carTransform.position.y, carTransform.position.z)) - transform.position;
		Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);

		//Move to car
		Vector3 _targetPos = absoluteInitCameraPosition + carTransform.transform.position;
		transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);

	}

}

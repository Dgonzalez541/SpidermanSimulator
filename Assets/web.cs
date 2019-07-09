using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class web : MonoBehaviour {
	public SteamVR_TrackedObject controller;
	public LineRenderer line;
	public Rigidbody player;
	public GameObject fakeBody;
	public GameObject otherHand;
	private Vector3 target; 
	private bool isSlinging;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var device = SteamVR_Controller.Input ((int)controller.index);
		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) 
		{
			
			RaycastHit hit; 
			if (Physics.Raycast (controller.transform.position, controller.transform.forward, out hit)) 
			{
				line.enabled = true;
				line.SetPosition (0, controller.transform.position);
				target = hit.point;
				line.SetPosition (1, target);
				line.material.mainTextureOffset = Vector2.zero;
			} 
		}
		else if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) 
		{
			isSlinging = true;
			fakeBody.SetActive(false);
			line.SetPosition (0, controller.transform.position);
			line.material.mainTextureOffset = new Vector2 (line.material.mainTextureOffset.x + Random.Range (0.01f, -0.5f), 0f);
			player.AddForce ((target - controller.transform.position).normalized * 20f);//,ForceMode.Acceleration);					
		} 
		//end if
		else 
		{
		//	Debug.Log ("else");
			isSlinging = false;
			if (!otherHand.GetComponent<web> ().isSlinging && !isSlinging && player.velocity.magnitude < 3f) 
			{
				fakeBody.SetActive(true);
			}
			line.enabled = false;
		}
	}//end update

}

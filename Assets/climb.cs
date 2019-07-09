using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climb : MonoBehaviour {

	public Rigidbody Body;
	public Vector3 prevPos;
	public bool canGrip;
    public GameObject hand;
    public GameObject Otherhand;
	public GameObject fakeBody;

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    public bool gripButtonDown = false;
    public bool gripButtonUp = false;
    public bool gripButtonPressed = false;
    private Valve.VR.EVRButtonId dPadLeft = Valve.VR.EVRButtonId.k_EButton_DPad_Left;
    public bool dPadLeftDown = false;
    public bool dPadLeftUp = false;
    public bool dPadLeftPressed = false;
    private Valve.VR.EVRButtonId dPadRight = Valve.VR.EVRButtonId.k_EButton_DPad_Right;
    public bool dPadRightDown = false;
    public bool dPadRightUp = false;
    public bool dPadRightPressed = false;
    private Valve.VR.EVRButtonId dPadUp = Valve.VR.EVRButtonId.k_EButton_DPad_Up;
    public bool dPadUpDown = false;
    public bool dPadUpUp = false;
    public bool dPadUpPressed = false;
    private Valve.VR.EVRButtonId dPadDown = Valve.VR.EVRButtonId.k_EButton_DPad_Down;
    public bool dPadDownDown = false;
    public bool dPadDownUp = false;
    public bool dPadDownPressed = false;
    private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    public bool touchpadDown = false;
    public bool touchpadUp = false;
    public bool touchpadPressed = false;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerButtonDown = false;
    public bool triggerButtonUp = false;
    public bool triggerButtonPressed = false;
    private Valve.VR.EVRButtonId menu = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
    public bool menuDown = false;
    public bool menuUp = false;
    public bool menuPressed = false;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        prevPos = hand.transform.localPosition;
	}//end start

	void FixedUpdate () {
        gripButtonDown = controller.GetPressDown(gripButton);
        gripButtonUp = controller.GetPressUp(gripButton);
        gripButtonPressed = controller.GetPress(gripButton);

        var device = SteamVR_Controller.Input ((int)controller.index);
		if ((canGrip && gripButtonPressed)) {
			Body.useGravity = false;
			Body.isKinematic = true;
			Body.transform.position += (prevPos - hand.transform.position);
		}
		else
        {
            Body.useGravity = true;
			Body.isKinematic = false;
		}

		prevPos = hand.transform.position;
	}//end update

	void OnTriggerStay(Collider other)
    {
		canGrip = true;
	}//end onTriggerEnger

    void OnCollisionStay(Collider other)
    {
        if (other.tag == "wall")
        {
            canGrip = true;
        }
    }
    void OnCollisionEnter(Collider other)
    {

        canGrip = true;

    }
    void OnCollisionExit(Collider other)
    {

        canGrip = false;

    }//end onTriggerEnger
    void OnTriggerExit(Collider other)
    {
		canGrip = false;
	}//end onTriggerExit


}//end climb

/*
 * 
 * I would use the colliders instead:
1) Add sphere colliders to the controllers and head, scale as necessary
2) Create a tag and assign it to the controllers and head gameobjects that the sphere colliders are attached to.
3) Add a script to the moving wall(s) that checks OnCollisionEnter(Collider collider) and in that checks for that which collided's tag.
a) if (collider.tag == "controllerorhead") { // head or collider touched the moving wall, player loses }
 */

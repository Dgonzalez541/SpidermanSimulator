using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;


public class LabLoco : MonoBehaviour
{
    public GameObject Rig;
    public Camera HMDCam;
    public GameObject blindfold;
    public GameObject body;
    public GameObject controllerOther;

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
    private int turnCount;


    void Start()
    {
        //body = GameObject.Find("Body");
        //var go = GameObject.Find("SomeGuy");
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        //cachedTransform = GetComponent<Transform>();
        //cachedPosition = track.position;
        //cachedRotation = track.rotation.eulerAngles;
    }

    void Update()
    {
        if (controller == null)
        {
            Debug.Log("Controller not initialized");

        }
        /*

        if (controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0)[0] < -0.8f
        && controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
        {
            dPadLeftUp = true;
            //Debug.Log("dPadLeftUp");
        }
        else dPadLeftUp = false;

        if (controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0)[0] > 0.8f
        && controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
        {
            dPadRightUp = true;
            // Debug.Log("dPadRightUp");
        }
        else dPadRightUp = false;

        if (controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0)[1] > 0.8f
        && controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
        {
            dPadUpUp = true;
            // Debug.Log("dPadUpUp");
        }
        else dPadUpUp = false;

        if (controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0)[1] < -0.8f
        && controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
        {
            dPadDownUp = true;
            // Debug.Log("dPadDownUp");
        }
        else dPadDownUp = false;
        /*
        dPadLeftDown = controller.GetPressDown(dPadLeft);
        dPadLeftUp = controller.GetPressUp(dPadLeft);
        dPadLeftPressed = controller.GetPress(dPadLeft);

        dPadRightDown = controller.GetPressDown(dPadRight);
        dPadRightUp = controller.GetPressUp(dPadRight);
        dPadRightPressed = controller.GetPress(dPadRight);

        dPadUpDown = controller.GetPressDown(dPadUp);
        dPadUpUp = controller.GetPressUp(dPadUp);
        dPadUpPressed = controller.GetPress(dPadUp);

        dPadDownDown = controller.GetPressDown(dPadDown);
        dPadDownUp = controller.GetPressUp(dPadDown);
        dPadDownPressed = controller.GetPress(dPadDown);
        */
        menuDown = controller.GetPressDown(menu);
        menuUp = controller.GetPressUp(menu);
        menuPressed = controller.GetPress(menu);

        touchpadDown = controller.GetPressDown(touchpad);
        touchpadUp = controller.GetPressUp(touchpad);
        touchpadPressed = controller.GetPress(touchpad);

        gripButtonDown = controller.GetPressDown(gripButton);
        gripButtonUp = controller.GetPressUp(gripButton);
        gripButtonPressed = controller.GetPress(gripButton);

        triggerButtonDown = controller.GetPressDown(triggerButton);
        triggerButtonUp = controller.GetPressUp(triggerButton);
        triggerButtonPressed = controller.GetPress(triggerButton);

        var forward = HMDCam.transform.forward;
        var right = HMDCam.transform.right;

        if (touchpadPressed /*&& !body.GetComponent<BodyCollide>().collide*/)
        {

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();
            forward = Quaternion.AngleAxis(-90 * turnCount, Vector3.up) * forward;
            right = Quaternion.AngleAxis(-90 * turnCount, Vector3.up) * right;
            var desiredMoveDirection = forward * controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0)[1] + right * controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0)[0];

            Rig.transform.Translate(desiredMoveDirection * 2 * Time.deltaTime);
        }
        if (gripButtonDown)
        {
            blindfold.SetActive(!blindfold.activeSelf);
            Rig.transform.localEulerAngles = new Vector3(Rig.transform.localEulerAngles.x, Rig.transform.localEulerAngles.y + 90, Rig.transform.localEulerAngles.z);
            turnCount++;
            controllerOther.GetComponent<LabLoco>().turnCount = turnCount;
            StartCoroutine(Reset());
            //blindfold.SetActive(!blindfold.activeSelf);
            //forward = Quaternion.AngleAxis(-90, Vector3.up) * forward;
        }

    }
    public IEnumerator Reset()
    {
        blindfold.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        blindfold.SetActive(false);
    }
}

/*  The control scheme for the controler goes as follows:
    To disable/enable the blindfold - Menu button
    To record the judged distance for a run - Down on the DPad
    To dump the subect recorded data to a file - Up on the DPad
    To move the pryamid to the next location in space - Right on the DPad
    To calibrate the virtual word to the real world with the position of a controller - Left on the DPad
    To toggle betwen recording all tracked data for a run and dumping it to a file - Trigger button

    remaining possible button mappings:
        grip button
        touchpad axis
        touchpad down (no matter the direction)

 * 
 * 
 * 
 * 
 */

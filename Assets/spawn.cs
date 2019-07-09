using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{

    public Transform spawnPos;
    public GameObject spawnee;
    private Valve.VR.EVRButtonId menu = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
    public bool menuDown = false;
    public bool menuUp = false;
    public bool menuPressed = false;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }//end start

    void FixedUpdate()
    {
        menuDown = controller.GetPressDown(menu);
        menuUp = controller.GetPressUp(menu);
        menuPressed = controller.GetPress(menu);

        var device = SteamVR_Controller.Input((int)controller.index);

    }//end update

    // Update is called once per frame
    void Update()
    {
        if (menuPressed == true)
        {
          if (GameObject.Find("Anit-Motion Sickness Aparatus") != null)
           {
             Destruction();
            }
         }
        else
            Instantiate(spawnee, spawnPos.position, spawnPos.rotation);
    }

    void Destruction()
    {
        Destroy(this.gameObject);
    }//Destroys cage
}
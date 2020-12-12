using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag : MonoBehaviour
{

    private Game _game;
    
    private OVRGrabbable grabbedObjectInTrigger;

    // Start is called before the first frame update
    void Start()
    {
        _game = gameObject.transform.parent.GetComponent<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyObject(OVRGrabbable toDestroy)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(toDestroy.gameObject);
    }
    
    public void OnGrabEnd()
    {
        
        if (grabbedObjectInTrigger != null)
        {
            if (_game.player.inventory.Count < 10)
            {
                _game.player.addToInventory(grabbedObjectInTrigger.GetComponent<ItemBody>().item);
                DebugUtils.message2 = "Is K : " + grabbedObjectInTrigger.GetComponent<Rigidbody>().isKinematic.ToString();
                StartCoroutine(PlayerUtils.MessageCoroutine("Vous avez lâché l'objet dans le sac !", 3,_game));
                GameObject toDestroy = grabbedObjectInTrigger.gameObject;
                StartCoroutine(DestroyObject(grabbedObjectInTrigger));
                //Destroy(grabbedObjectInTrigger.gameObject);
                //Destroy(toDestroy);
            }
            else
            {
                StartCoroutine(PlayerUtils.MessageCoroutine("Vous n'avez plus de place !", 3,_game));
            }
        }
        grabbedObjectInTrigger = null;
        
    }

    
    private bool GrabbableIsGrabbed(OVRGrabbable grabbable)
    {
        bool isGrabbed = false;
        OVRGrabber[] handGrabbers = _game.gameObject.GetComponentsInChildren<OVRGrabber>();
        foreach (OVRGrabber grabber in handGrabbers)
        {
            if (grabber.grabbedObject == grabbable) isGrabbed = true;
        }

        return isGrabbed;

    }

    
    
    void OnTriggerEnter(Collider collider)
    {

        OVRGrabbable grabbable = collider.gameObject.GetComponent<OVRGrabbable>();
        //A grabbable object is in the bag location
        if (grabbable != null)
        {
            bool isGrabbed = GrabbableIsGrabbed(grabbable);

            if (isGrabbed)
            {
                grabbedObjectInTrigger = grabbable;
                _game.messageCanvas.SetActive(true);
                _game.messageCanvas.GetComponentInChildren<Text>().text = "Lâcher pour mettre l'objet dans le sac";
                DebugUtils.message2 = "OK Canvas !";
            }
        }
        
        
        /*
        if (collider.tag == "Hand")
        {
            OVRGrabber handGrabber = collider.GetComponent<OVRGrabber>();
            if (handGrabber.grabbedObject != null)
            {
                message2 = "OK !";
                _messageCanvas.SetActive(true);
            }
            else
            {
                message = "Collider! No grabbed object " + Time.time;
            }
        }*/

    }
    
    void OnTriggerExit(Collider collider)
    {
        
        OVRGrabbable grabbable = collider.gameObject.GetComponent<OVRGrabbable>();

        if (grabbable == grabbedObjectInTrigger)
        {
            grabbedObjectInTrigger = null;
            _game.messageCanvas.SetActive(false);
            DebugUtils.message2 = "Out Canvas !";
        }
        
        /*
        //A grabbable object is in the bag location
        if (grabbable != null)
        {
            bool isGrabbed = GrabbableIsGrabbed(grabbable);

            if (isGrabbed)
            {
                message2 = "Out !";
                _messageCanvas.SetActive(false);
                message2 = "Out Canvas !";

            } 
        }*/
        
        /*
        if (collider.tag == "Hand")
        {
            OVRGrabber handGrabber = collider.GetComponent<OVRGrabber>();
            //if (handGrabber.grabbedObject != null)
            {
                message2 = "Out";
                _messageCanvas.SetActive(false);
            }
        }*/
    }
}

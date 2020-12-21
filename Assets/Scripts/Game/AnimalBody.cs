using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AnimalBody : MonoBehaviour
{
    private Animal _animal;

    public Animal animal => _animal;

    [SerializeField]
    private AnimalType _type;

    private StateMachine _behavior;

    private NavMeshAgent _navMeshAgent;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;

    private Vector3 _initialPosition;

    public Vector3 InitialPosition => _initialPosition;
    
    private ItemBody _foundFood = null;

    private BodyDetection _detection;

    public ItemBody foundFood
    {
        get => _foundFood;
        set => _foundFood = value;
    }


    // Start is called before the first frame update
    void Start()
    {
        _detection = GetComponentInChildren<BodyDetection>(); 
        _initialPosition = transform.position;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        Game game = GameObject.FindObjectOfType<Game>();
        switch (_type)
        {
            case AnimalType.Rabbit:
                _animal =new Animal("Rabbit",100,game.GetItem("Carrot") as Food, game.GetItem("Ribbon") as RareItem);
                break;
            case AnimalType.Deer:
                _animal =new Animal("Deer",100,game.GetItem("Nut") as Food, game.GetItem("Garland") as RareItem);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        _behavior = new AnimalStateMachine(this);
        
    }

    private bool inProgress = false;
    IEnumerator TouchAnimal()
    {
        inProgress = true;
        yield return new WaitForSeconds(0.1f);
        _animal.IncreaseHappiness();
        inProgress = false;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        _behavior.Action();
        transform.Find("Canvas").Find("ProgressBarHungry").GetComponent<Image>().fillAmount = (_animal.hunger / 100.0f);
        transform.Find("Canvas").Find("ProgressBarSad").GetComponent<Image>().fillAmount = (_animal.happiness / 100.0f);
        transform.Find("Canvas").Find("TextState").GetComponent<Text>().text = _behavior.ToString();
        if (_detection.touchAnimal && !inProgress)
        {
            StartCoroutine(TouchAnimal());
        }
    }
    
    public void StartChildCoroutine(IEnumerator coroutineMethod)
    {
        StartCoroutine(coroutineMethod);
    }
    
    public void MoveToDestination(Vector3 destination)
    {
        NavMeshAgent.destination = destination;
        
        transform.rotation = Quaternion.LookRotation(destination - transform.position);
    }

    public bool isHungry()
    {
        return _animal.hunger > 70;
    }

    public bool isSad()
    {
        return _animal.happiness < 30;
    }

    public void SetSlowSpeed()
    {
        _navMeshAgent.speed = 0.75f;
    }

    public void SetNormalSpeed()
    {
        _navMeshAgent.speed = 1.5f;
    }
    
    public void OnTriggerStay(Collider collider)
    {
        ItemBody body = collider.gameObject.GetComponent<ItemBody>();
        //if (body != null)
        //{
        //}
        if (foundFood == null && body != null && body.item == _animal.eat && (body.transform.parent == null && !body.gameObject.GetComponent<OVRGrabbable>().isGrabbed) /* body.gameObject.GetComponent<Rigidbody>().useGravity*/)
        {
            foundFood = body;
        }

        if (!animal.WearItem() && body != null && body.item == _animal.canWear && (body.transform.parent == null && !body.gameObject.GetComponent<OVRGrabbable>().isGrabbed) /*body.gameObject.GetComponent<Rigidbody>().useGravity*/)
        {
            RareItem item = body.item as RareItem;
            DebugUtils.message = animal.name + " - " + body.item.name;
            body.gameObject.transform.parent = transform;
            animal.SetWearItem(item);
            Destroy(body.gameObject.GetComponent<OVRGrabbable>());
            Collider[] colliders = body.gameObject.GetComponents<Collider>();
            foreach (Collider co in colliders)
            {
                Destroy(co);
            }
            body.gameObject.GetComponent<Rigidbody>().useGravity = false;
            body.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}

public enum AnimalType
{
    Rabbit,
    Deer
}
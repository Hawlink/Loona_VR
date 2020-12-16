using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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

    private TextMesh _textMesh;
    public TextMesh textMesh => _textMesh;

    private ItemBody _foundFood = null;

    public ItemBody foundFood
    {
        get => _foundFood;
        set => _foundFood = value;
    }


    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = transform.position;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        Game game = GameObject.FindObjectOfType<Game>();
        switch (_type)
        {
            case AnimalType.Rabbit:
                _animal =new Animal("Rabbit",100,game.GetItem("Carrot") as Food);
                break;
            case AnimalType.Deer:
                _animal =new Animal("Deer",100,game.GetItem("Carrot") as Food);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        _behavior = new AnimalStateMachine(this);

        GameObject text = new GameObject();
        _textMesh = text.AddComponent<TextMesh>();
        text.AddComponent<FacingCamera>();
        text.transform.parent = transform;
        text.transform.localPosition = new Vector3(0, 20, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _behavior.Action();
        _textMesh.text = _behavior.ToString() + "\n" + _animal.happiness + "\n" + _animal.hunger;
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
        _navMeshAgent.speed = 1;
    }

    public void SetNormalSpeed()
    {
        _navMeshAgent.speed = 2;
    }
    
    public void OnTriggerEnter(Collider collider)
    {
        ItemBody body = collider.gameObject.GetComponent<ItemBody>();
        if (body != null && body.item == _animal.eat)
        {
            foundFood = body;
        }
    }
}

public enum AnimalType
{
    Rabbit,
    Deer
}
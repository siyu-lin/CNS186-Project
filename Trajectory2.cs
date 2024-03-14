using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory2 : MonoBehaviour
{
    // Start is called before the first frame update
    private float delay = 0.02f;

    // Number of delays before the first contact
    private int timeBeforeBounce = 20;

    // Line at x = 0.40 along the z direction 
    private float lineEdge = 0.40f; 

    private float ballRadius = 0.5f;

    [SerializeField] private Vector3 bouncePosition;
    [SerializeField] private Vector3 bounceVelocity;
    [SerializeField] private Vector3 initPosition = new Vector3(-3,2,-3);
    [SerializeField] private Vector3 velocity = new Vector3(0.1f, 0f, 0.1f);
    [SerializeField] private Vector3 acceleration = new Vector3(0f, -0.01f, 0f);
    [SerializeField] private int sceneNumber = 0;
    float timer;

    private SceneObject sceneObject = new SceneObject();
    private bool firstBounce = true;

    public static Vector3 perceivedBounce = new Vector3(0,0,0);


    void Start()
    {
        bouncePosition = new Vector3(
                                    randGaussian(lineEdge + ballRadius, 0.6f),
                                    ballRadius,
                                    randGaussian(0, 1.0f));

        bounceVelocity = new Vector3(
                                    randGaussian(0.1f, 0.02f),
                                    -randGaussian(0.2f, 0.02f),
                                    randGaussian(0.1f, 0.02f));

        // Reverse calculate the initial conditions
        initPosition = bouncePosition;
        velocity = bounceVelocity;
        for(int i = 0; i < timeBeforeBounce; i=i+1) {
            velocity = velocity - acceleration;
            initPosition = initPosition - velocity;
            // Debug.Log(velocity);
        }
        


        // Right, Up, Forward
        transform.position = initPosition;
        // Initialize SaveSystem
        SaveSystem.Init();

        sceneObject.initPosition = initPosition;
        sceneObject.initVelocity = velocity;
        sceneObject.acceleration = acceleration;
        sceneObject.contactPosition = bouncePosition;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > delay)
        {
            MoveBall();
            timer -= delay;
            velocity = velocity + acceleration;
            if(transform.position.y <= ballRadius)
            {
                velocity.y = -velocity.y;
                if(firstBounce){
                    // sceneObject.contactPosition = transform.position;
                    firstBounce = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
            Debug.Log("Saved");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    void MoveBall()
    {
        transform.position = transform.position + velocity;
    }

    void Save()
    {
        sceneObject.perceivedContact = perceivedBounce;
        string json = JsonUtility.ToJson(sceneObject);
        SaveSystem.Save(sceneNumber, json);
    }

    void Load()
    {
        string json = SaveSystem.Load(sceneNumber);
        if(json != null)
        {
            
        }
    }


    float randGaussian(float mu, float sigma)
    {
        float rand1 = Random.Range(0.0f, 1.0f);
        float rand2 = Random.Range(0.0f, 1.0f);

        float n = Mathf.Sqrt(-2.0f * Mathf.Log(rand1)) * Mathf.Cos((2.0f * Mathf.PI) * rand2);

        return (mu + sigma * n);
    }

    public class SceneObject 
    {
        public Vector3 initPosition;
        public Vector3 initVelocity;
        public Vector3 acceleration;
        public Vector3 contactPosition;
        public Vector3 perceivedContact;
    }

}

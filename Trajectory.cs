using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    // Start is called before the first frame update
    private float delay = 0.02f;


    [SerializeField] private Vector3 initPosition = new Vector3(-3,2,-3);
    [SerializeField] private Vector3 velocity = new Vector3(0.1f, 0f, 0.1f);
    [SerializeField] private Vector3 acceleration = new Vector3(0f, -0.01f, 0f);
    [SerializeField] private int sceneNumber = 0;
    float timer;

    private SceneObject sceneObject = new SceneObject();
    private bool firstBounce = true;

    [SerializeField] private Vector3 bouncePosition = new Vector3(0f,0f,0f);


    void Start()
    {
        // Right, Up, Forward
        transform.position = initPosition;
        // Initialize SaveSystem
        SaveSystem.Init();

        sceneObject.initPosition = initPosition;
        sceneObject.initVelocity = velocity;
        sceneObject.acceleration = acceleration;
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
            if(transform.position.y <= 0.5)
            {
                velocity.y = -velocity.y;
                if(firstBounce){
                    sceneObject.contactPosition = transform.position;
                    bouncePosition = transform.position;
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

    private class SceneObject 
    {
        public Vector3 initPosition;
        public Vector3 initVelocity;
        public Vector3 acceleration;
        public Vector3 contactPosition;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MousePosition : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 mousePosition;   // 2D mouse position on screen
    public Vector3 worldPosition;   // 3D mouse position in the world on the floor
    private float maxDistance; // Just > distance between camera and floor
    private int layerToHit;    // Layer of the floor 
    void Start()
    {
        maxDistance = 30;
        layerToHit = 7;
    } 

    // Update is called once per frame
    void Update()
    {        
        mousePosition = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if ( Physics.Raycast(ray, out RaycastHit hitData, maxDistance,1<<layerToHit))
        {
            worldPosition = hitData.point;
        }

        transform.position = worldPosition;

        if (Input.GetMouseButtonDown(0))
            Debug.Log(worldPosition);
            Trajectory2.perceivedBounce = worldPosition;
    }
}

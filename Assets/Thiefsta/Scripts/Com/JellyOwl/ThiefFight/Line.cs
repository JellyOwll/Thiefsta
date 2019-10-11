using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour
{

    public GameObject gameObject;          // Reference to the second GameObject
    public Transform positionStart;
    private float WIDTH_HEIGHT = 0.125f;
    private LineRenderer line;              // Line Renderer


    // Use this for initialization
    void Start()
    {
        // Add a Line Renderer to the GameObject
        line = this.GetComponent<LineRenderer>();
        // Set the width of the Line Renderer
        line.SetWidth(WIDTH_HEIGHT, WIDTH_HEIGHT);
        // Set the number of vertex fo the Line Renderer
        line.positionCount=2;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the GameObjects are not null
        if (gameObject != null)
        {
            // Update position of the two vertex of the Line Renderer
            line.SetPosition(0, positionStart.position);
            line.SetPosition(1, gameObject.transform.position);
        }
    }
}
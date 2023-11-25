using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class ChangeColour : MonoBehaviour
{
    public Material redMaterial;
    public Material blueMaterial;
    private Renderer nestedObjectRenderer;
    private bool isRed;
    private RedCar redCarScript;
    void Start()
    {
        // Find the nested object by tag
        GameObject nestedObject = GameObject.FindWithTag("PlayerBody");
        redCarScript = GetComponent<RedCar>();
        if (nestedObject != null)
        {
            // Get the Renderer component from the nested object
            nestedObjectRenderer = nestedObject.GetComponent<Renderer>();

            if (nestedObjectRenderer != null)
            {
                // Initialize the material state based on the current material
                if (nestedObjectRenderer.material == redMaterial)
                {
                    isRed = true;
                }
                else if (nestedObjectRenderer.material == blueMaterial)
                {
                    isRed = false;
                }
                else
                {
                    Debug.Log("Nested object does not have the red or blue material assigned. Defaulting to red.");
                    nestedObjectRenderer.material = redMaterial;
                    isRed = true;
                }
            }
            else
            {
                Debug.LogError("Renderer component not found on nested object!");
            }
        }
        else
        {
            Debug.LogError("Nested object with specified tag not found!");
        }
    }

    public bool isRedColour()
    {
        return isRed;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleMaterial();
        }
        
    }

    void ToggleMaterial()
    {
        if (nestedObjectRenderer != null)
        {
            if (isRed)
            {
                nestedObjectRenderer.material = blueMaterial;
            }
            else
            {
                nestedObjectRenderer.material = redMaterial;
            }
            isRed = !isRed;
        }
    }
    

}



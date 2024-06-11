using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI; // Import the AI namespace

public class NavMeshscript : MonoBehaviour
{
    private NavMeshModifierVolume volume; // Declare a NavMeshModifierVolume
    private NavMeshSurface surface; // Declare a NavMeshSurface

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<NavMeshModifierVolume>(); // Get the NavMeshModifierVolume component
        surface = gameObject.AddComponent<NavMeshSurface>(); // Add a NavMeshSurface component

        // Set the size of the NavMeshSurface to match the NavMeshModifierVolume
        surface.size = volume.size;

        surface.BuildNavMesh(); // Build the NavMesh
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
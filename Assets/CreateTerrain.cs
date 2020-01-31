using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

[ExecuteInEditMode]
public class CreateTerrain : MonoBehaviour
{
    public float depth = 10f;
    public float dim = 1000f;

    public Material material;

    private ProBuilderMesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<MeshCollider>() == null)
            gameObject.AddComponent<MeshCollider>();
        if (gameObject.GetComponent<ProBuilderMesh>() == null)
            gameObject.AddComponent<ProBuilderMesh>();

        mesh = gameObject.GetComponent<ProBuilderMesh>();
        mesh.userCollisions = false;

        // Create a square with centre 0,-depth, 0 and sides of 2*dim length:
        var points = new List<Vector3>() {
            new Vector3(dim, depth * -1, dim),
            new Vector3(dim, depth * -1, dim * -1),
            new Vector3(dim * -1, depth * -1, dim * -1),
            new Vector3(dim * -1, depth * -1, dim)

        };
        
        mesh.CreateShapeFromPolygon(points, depth, false);
        mesh.SetMaterial(mesh.faces, material);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DrawCanal(List<Vector3> corners)
    {
//        mesh.Extrude()   
    }
}

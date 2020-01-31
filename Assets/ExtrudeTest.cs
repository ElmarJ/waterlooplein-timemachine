using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;
using Parabox.CSG;

public class ExtrudeTest : MonoBehaviour
{
    private ProBuilderMesh terrainMesh;
    private MeshFilter terrainMeshFilter;
    // Start is called before the first frame update
    void Start()
    {
        terrainMesh = GetComponent<ProBuilderMesh>();
        terrainMeshFilter = GetComponent<MeshFilter>();

        var canalPolygon = new Vector3[] {
                new Vector3(1f, 0.5f, 1f),
                new Vector3(2f, 0.5f, 1f),
                new Vector3(2f, 0.5f, 2f),
                new Vector3(1f, 0.5f, 2f)
            };

        var depth = 2f;

        DrawCanal(canalPolygon, depth);
    }

    private void DrawCanal(Vector3[] canalPolygon, float depth)
    {
        var canalGameObject = new GameObject("canal");
        canalGameObject.transform.parent = transform.parent;
        var canalMesh = canalGameObject.AddComponent<ProBuilderMesh>();

        canalMesh.CreateShapeFromPolygon(
            canalPolygon,
            depth,
            false);

        canalGameObject.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;

        var originalCenter = terrainMeshFilter.sharedMesh.bounds.center;
        var subtractResult = Boolean.Subtract(this.gameObject, canalGameObject);

        var import = new MeshImporter(terrainMesh);
        import.Import(subtractResult.mesh, null, new MeshImportSettings() { quads = true });

        terrainMesh.ToMesh();
        terrainMesh.Refresh();

        var newCenter = terrainMeshFilter.sharedMesh.bounds.center;
        var offset = originalCenter - newCenter;
        var indices = terrainMeshFilter.sharedMesh.GetIndices(0);

        terrainMesh.TranslateVertices(indices, offset);
        terrainMesh.CenterPivot(indices);

        terrainMesh.ToMesh();
        terrainMesh.Refresh();

        Destroy(canalGameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

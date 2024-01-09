using UnityEngine;

public class TerrainDeformation : MonoBehaviour
{
    public float deformationRadius = 5f;
    public float deformationStrength = 0.5f;

    private Terrain terrain;

    void Start()
    {
        terrain = GetComponent<Terrain>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click");
            HandleTerrainDeformation();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, deformationRadius);
    }

    void HandleTerrainDeformation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (IsWithinTerrainBounds(hit.point))
            {
                Vector3 terrainPoint = GetTerrainPoint(hit.point);
                DeformTerrain(terrainPoint);
            }
        }
    }

    bool IsWithinTerrainBounds(Vector3 point)
    {
        return point.x >= 0 && point.x <= terrain.terrainData.size.x &&
               point.z >= 0 && point.z <= terrain.terrainData.size.z;
    }

    Vector3 GetTerrainPoint(Vector3 hitPoint)
    {
        // Convert the hit point to local terrain coordinates
        Vector3 terrainPoint = hitPoint - terrain.transform.position;

        // Normalize to [0, 1] range
        terrainPoint.x /= terrain.terrainData.size.x;
        terrainPoint.z /= terrain.terrainData.size.z;

        return terrainPoint;
    }

    void DeformTerrain(Vector3 terrainPoint)
    {
        float[,] heights = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution);

        int centerX = Mathf.RoundToInt(terrainPoint.x * (terrain.terrainData.heightmapResolution - 1));
        int centerZ = Mathf.RoundToInt(terrainPoint.z * (terrain.terrainData.heightmapResolution - 1));

        for (int x = centerX - Mathf.RoundToInt(deformationRadius * (terrain.terrainData.heightmapResolution - 1)); x <= centerX + Mathf.RoundToInt(deformationRadius * (terrain.terrainData.heightmapResolution - 1)); x++)
        {
            for (int z = centerZ - Mathf.RoundToInt(deformationRadius * (terrain.terrainData.heightmapResolution - 1)); z <= centerZ + Mathf.RoundToInt(deformationRadius * (terrain.terrainData.heightmapResolution - 1)); z++)
            {
                if (x >= 0 && x < terrain.terrainData.heightmapResolution && z >= 0 && z < terrain.terrainData.heightmapResolution)
                {
                    float distance = Vector2.Distance(new Vector2(x, z), new Vector2(centerX, centerZ)) / (deformationRadius * (terrain.terrainData.heightmapResolution - 1));
                    heights[x, z] -= deformationStrength * Mathf.SmoothStep(1f, 0f, distance);
                }
            }
        }

        terrain.terrainData.SetHeights(0, 0, heights);
    }
}

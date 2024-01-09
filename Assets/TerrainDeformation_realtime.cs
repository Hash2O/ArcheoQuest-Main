using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDeformation_realtime : MonoBehaviour
{
    public Terrain myTerrain;
    int xResolution;
    int zResolution;
    float[,] heights;

    void Start()
    {
        xResolution = myTerrain.terrainData.heightmapResolution;
        zResolution = myTerrain.terrainData.heightmapResolution;
        heights = myTerrain.terrainData.GetHeights(0, 0, xResolution, zResolution);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                raiseTerrain(hit.point);
            }
        }
        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                lowerTerrain(hit.point);
            }
        }
    }

    private void raiseTerrain(Vector3 point)
    {
        int terX = (int)((point.x / myTerrain.terrainData.size.x) * xResolution);
        int terZ = (int)((point.z / myTerrain.terrainData.size.z) * zResolution);
        float[,] height = myTerrain.terrainData.GetHeights(terX - 4, terZ - 4, 9, 9);  //new float[1,1];

        for (int tempY = 0; tempY < 9; tempY++)
            for (int tempX = 0; tempX < 9; tempX++)
            {
                float dist_to_target = Mathf.Abs((float)tempY - 4f) + Mathf.Abs((float)tempX - 4f);
                float maxDist = 8f;
                float proportion = dist_to_target / maxDist;

                height[tempX, tempY] += 0.001f * (1f - proportion);
                heights[terX - 4 + tempX, terZ - 4 + tempY] += 0.01f * (1f - proportion);
            }

        myTerrain.terrainData.SetHeights(terX - 4, terZ - 4, height);
    }

    private void lowerTerrain(Vector3 point)
    {
        int terX = (int)((point.x / myTerrain.terrainData.size.x) * xResolution);
        int terZ = (int)((point.z / myTerrain.terrainData.size.z) * zResolution);
        float y = heights[terX, terZ];
        y -= 0.001f;
        float[,] height = new float[1, 1];
        height[0, 0] = y;
        heights[terX, terZ] = y;
        myTerrain.terrainData.SetHeights(terX, terZ, height);
    }
}

// Builds a Mesh containing a single triangle with uvs.
// Create arrays of vertices, uvs and triangles, and copy them into the mesh.

using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	
	public int mapWidth;
	public int mapHeight;
	public float noiseScale;

	[Range(1,5)]
	public int octaves;
	[Range(0,1)]
	public float persistance;
	[Range(0,2)]
	public float lacunarity;

	public bool autoUpdate;

	public int seed;
	public Vector2 offset;

	public float heightScale;

	private Mesh mesh;

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        mesh = GetComponent<MeshFilter>().mesh;
		GenerateNewMap();
	}

	public void GenerateNewMap(){
        mesh.Clear();

		float[,] heightMap = Noise.GenerateNoiseMap (mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

		int w = heightMap.GetLength(0);
		int h = heightMap.GetLength(1);
		float topLeftX = (w-1)/-2f;
		float topLeftZ = (h-1)/2f;

		if(heightScale <= 0){heightScale = 0.00001f;}

		MeshData meshData = new MeshData(w,h);
		int vertexIndex = 0;
		for(int y = 0; y < h; y++){
			for(int x = 0; x < w; x++){
				meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightMap[x,y] * heightScale, topLeftZ - y); 
				meshData.uvs[vertexIndex] = new Vector2(x/(float) w, y/(float) h);

				if(x<w-1 && y < h-1){
					//create two triangles
					meshData.AddTriangle(vertexIndex, vertexIndex+w+1, vertexIndex+w);
					meshData.AddTriangle(vertexIndex+w+1, vertexIndex, vertexIndex+1);
				}

				vertexIndex++;
			}
		}

		mesh.vertices = meshData.vertices;
		mesh.uv = meshData.uvs;
		mesh.triangles = meshData.triangles;

		mesh.RecalculateNormals();
    }

	void OnValidate() {
		if (mapWidth < 1) {
			mapWidth = 1;
		}
		if (mapHeight < 1) {
			mapHeight = 1;
		}
		if (lacunarity < 1) {
			lacunarity = 1;
		}
		if (octaves < 0) {
			octaves = 0;
		}
	}
}

public class MeshData {
	public Vector3[] vertices;
	public int[] triangles;
	public Vector2[] uvs;

	int triangleIndex;

	public MeshData(int meshWidth, int meshHeight){
		vertices = new Vector3[meshWidth * meshHeight];
		triangles = new int[(meshWidth-1)*(meshHeight-1)*6];
		uvs = new Vector2[meshWidth * meshHeight];
	}

	public void AddTriangle(int a, int b, int c){
		triangles[triangleIndex] = a;
		triangles[triangleIndex+1] = b;
		triangles[triangleIndex+2] = c;
		triangleIndex += 3;
	}

}


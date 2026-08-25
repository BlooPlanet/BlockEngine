using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Chunk : MonoBehaviour {

    public const int Width = 16, Height = 64, Depth = 16;
    BlockType[] blockList;
    MeshFilter meshFilter;

    // Start is called before the first frame update
    void Start() {
        Initialize(BlockType.None, null);
        SetBlock(new Vector3Int(1,1,1),BlockType.Block);
        Debug.Log(GetBlock(new Vector3Int(0,1,0)));
        ConstructMesh();
    }

    public void Initialize(BlockType initBlock, Material mat) {
        blockList = new BlockType[Width * Height * Depth];
        for (int i = 0; i < blockList.Length; i++) {
            blockList[i] = initBlock;
        }

        //GetComponent<MeshRenderer>().material = mat;
        meshFilter = GetComponent<MeshFilter>();
        
        Debug.Log("initialize");
    }

    public void ConstructMesh() {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector3> normals = new List<Vector3>();

        for (int x = 0; x < Width; x++)
        for (int z = 0; z < Depth; z++)
        for (int y = 0; y < Height; y++) {
            Vector3Int blockPos = new Vector3Int(x, y, z);
            if (GetBlock(blockPos) == BlockType.Block) {
                
                if (GetBlock(blockPos + Vector3Int.up) == BlockType.None) {
                    triangles.AddRange(BlockMeshData.FaceTrianglution(vertices.Count));
                    vertices.AddRange(BlockMeshData.UpFaceVertices(blockPos));
                    
                    //Normals
                    for (int i = 0; i < 4; i++) {
                        normals.Add(Vector3.up);
                    }
                }
                
                if (GetBlock(blockPos + Vector3Int.down) == BlockType.None) {
                    triangles.AddRange(BlockMeshData.FaceTrianglution(vertices.Count));
                    vertices.AddRange(BlockMeshData.DownFaceVertices(blockPos));
                    
                    //Normals
                    for (int i = 0; i < 4; i++) {
                        normals.Add(Vector3.down);
                    }
                }
                
                if (GetBlock(blockPos + Vector3Int.forward) == BlockType.None) {
                    triangles.AddRange(BlockMeshData.FaceTrianglution(vertices.Count));
                    vertices.AddRange(BlockMeshData.FrontFaceVertices(blockPos));
                    
                    //Normals
                    for (int i = 0; i < 4; i++) {
                        normals.Add(Vector3.forward);
                    }
                }
                
                if (GetBlock(blockPos + Vector3Int.back) == BlockType.None) {
                    triangles.AddRange(BlockMeshData.FaceTrianglution(vertices.Count));
                    vertices.AddRange(BlockMeshData.BackFaceVertices(blockPos));
                    
                    //Normals
                    for (int i = 0; i < 4; i++) {
                        normals.Add(Vector3.back);
                    }
                }
                
                if (GetBlock(blockPos + Vector3Int.right) == BlockType.None) {
                    triangles.AddRange(BlockMeshData.FaceTrianglution(vertices.Count));
                    vertices.AddRange(BlockMeshData.RightFaceVertices(blockPos));
                    
                    //Normals
                    for (int i = 0; i < 4; i++) {
                        normals.Add(Vector3.right);
                    }
                }
                
                if (GetBlock(blockPos + Vector3Int.left) == BlockType.None) {
                    triangles.AddRange(BlockMeshData.FaceTrianglution(vertices.Count));
                    vertices.AddRange(BlockMeshData.LeftFaceVertices(blockPos));
                    
                    //Normals
                    for (int i = 0; i < 4; i++) {
                        normals.Add(Vector3.left);
                    }
                }
            }
        }

        Mesh mesh = new Mesh();
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.normals = normals.ToArray();
        
        meshFilter.mesh = mesh;
        
        Debug.Log("mesh constructed");
    }

    public BlockType GetBlock(Vector3Int blockPos) {
        int index = blockPos.x + blockPos.y * Width + blockPos.z * Width * Height;
        return blockList[index];
    }

    public void SetBlock(Vector3Int blockPos, BlockType block) {
        int index = blockPos.x + blockPos.y * Width + blockPos.z * Width * Height;
        blockList[index] = block;
    }
}

using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer),typeof(MeshCollider))]
public class Chunk : MonoBehaviour {

    public const int Width = 16, Height = 64, Depth = 16;
    BlockType[] blockList;
    MeshFilter meshFilter;
    MeshCollider meshCollider; 
    [HideInInspector] public Vector3Int coordinate;
    [HideInInspector] public Vector3Int position;
    ChunkGen world;

    public void Initialize(BlockType initBlock, Material mat,Vector3Int chunkPos, Vector3Int chunkCoord,ChunkGen chunkGen) {
        position = chunkPos;
        coordinate = chunkCoord;
        world = chunkGen;
        
        blockList = new BlockType[Width * Height * Depth];
        for (int i = 0; i < blockList.Length; i++) {
            blockList[i] = initBlock;
        }

        GetComponent<MeshRenderer>().material = mat;
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
        
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
                
                //Check each faces of the blocks
                for (int i = 0; i < BlockMeshData.directions.Length; i++) {
                    Vector3Int faceDirection = BlockMeshData.directions[i];
                    
                    //Check is the blocks are in edge of the chunk
                    //Try to get neighbor chunk of block data
                    if (CoordInBound(faceDirection + blockPos)) {
                        
                        if (GetBlock(faceDirection + blockPos) == BlockType.None) {
                            
                            triangles.AddRange(BlockMeshData.FaceTrianglution(vertices.Count));
                            vertices.AddRange(BlockMeshData.GetFaceVertices(i,blockPos));

                            // normals
                            for (int j = 0; j < 4; j++) {
                                normals.Add(faceDirection);
                            }
                        }
                    }
                    else {
                        Vector3Int globalBlockPos = faceDirection + blockPos + position;
                        if (world.GetBlock(globalBlockPos) == BlockType.None) {
                            
                            triangles.AddRange(BlockMeshData.FaceTrianglution(vertices.Count));
                            vertices.AddRange(BlockMeshData.GetFaceVertices(i,blockPos));

                            // normals
                            for (int j = 0; j < 4; j++) {
                                normals.Add(faceDirection);
                            }
                        }
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
        meshCollider.sharedMesh = mesh;
        
    }

    public BlockType GetBlock(Vector3Int blockPos) {
        if (CoordInBound(blockPos)) {
            int index = blockPos.x + blockPos.y * Width + blockPos.z * Width * Height;
            return blockList[index];
        }
        return BlockType.None;
    }

    public void SetBlock(Vector3Int blockPos, BlockType block) {
        int index = blockPos.x + blockPos.y * Width + blockPos.z * Width * Height;
        blockList[index] = block;
    }

    public bool CoordInBound(Vector3Int blockPos) {
        return blockPos.x >= 0 && blockPos.x < Width && blockPos.y >= 0 && blockPos.y < Height && blockPos.z >= 0 &&
               blockPos.z < Depth;
    }

    public void GenerateBlocks() {
        for (int x = 0; x < Width; x++) {
            for (int z = 0; z < Depth; z++) {
                for (int y = 0; y < Height; y++) {
                    if (y < 3) {
                        Vector3Int blockPos = new Vector3Int(x, y, z);
                        SetBlock(blockPos,BlockType.Block);
                    }
                }
            }
        }
    }
}

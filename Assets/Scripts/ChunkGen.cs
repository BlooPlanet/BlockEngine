using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGen : MonoBehaviour {
    
    Dictionary<Vector3Int, Chunk> chunkDicitionary = new Dictionary<Vector3Int, Chunk>();
    List<Chunk> chunkList = new List<Chunk>();

    int width = 16;
    int depth = 16;

    public Material chunkMat;
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
        ConstructAllChunkMesh();
    }


    public void Init() {
        for (int x = 0; x < width; x++) {
            for (int z = 0; z < depth; z++) {
                Vector3Int chunkCoord = new Vector3Int(x, 0, z);
                Vector3Int chunkPosition = new Vector3Int(x * Chunk.Width, 0, z * Chunk.Depth);
                Chunk chunk = new GameObject("Chunk " + chunkPosition).AddComponent<Chunk>();
                chunk.transform.position = chunkPosition;
                chunk.transform.parent = this.transform;

                chunkDicitionary.TryAdd(chunkCoord, chunk);
                chunkList.Add(chunk);

                chunk.Initialize(BlockType.Block, chunkMat, chunkPosition, chunkCoord,this);
            }
        }   
    }

    public void ConstructAllChunkMesh() {
        for (int i = 0; i < chunkList.Count; i++) {
            chunkList[i].ConstructMesh();
        }
    }

    public Chunk GetChunk(Vector3Int globalBlockPos) {
        Vector3Int chunkCoord = new Vector3Int(globalBlockPos.x / Chunk.Width, 0, globalBlockPos.z / Chunk.Depth);
        Chunk chunk;
        chunkDicitionary.TryGetValue(chunkCoord, out chunk);
        return chunk;
    }

    public BlockType GetBlock(Vector3Int globalBlockPosition) {
        Chunk chunk = GetChunk(globalBlockPosition);
        if (chunk != null) {
            Vector3Int localBlockPos = globalBlockPosition - chunk.position;
            return chunk.GetBlock(localBlockPos);
        }

        return BlockType.None;
    }
}
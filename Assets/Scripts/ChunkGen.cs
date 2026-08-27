using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGen : MonoBehaviour {
    
    Dictionary<Vector3Int, Chunk> chunkDicitionary = new Dictionary<Vector3Int, Chunk>();
    List<Chunk> chunkList = new List<Chunk>();

    int width = 5;
    int depth = 5;

    public Material chunkMat;
    public Gradient terrainColorGradient;
    
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

                chunk.Initialize(BlockType.None, chunkMat, chunkPosition, chunkCoord,this);
                chunk.GenerateBlocks();
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

    public void SetBlock(Vector3Int globalBlockPos, BlockType block) {
        Chunk chunk = GetChunk(globalBlockPos);
        if (chunk != null) {
            Vector3Int localPos = globalBlockPos - chunk.position;
            chunk.SetBlock(localPos,block);
        }
    }

    public void PlaceTree(Vector3Int globalblockPos) {
        // generate logs 
        int treeLenght = Random.Range(2, 5);
        for (int i = 0; i < treeLenght; i++) {
            SetBlock(globalblockPos + Vector3Int.up * i,BlockType.Block);
        }

        // generate leafs
        int leafTop = Random.Range(2, 4);
        for (int z = -1; z <= 1; z++) {
            for (int x = -1; x <= 1; x++) {
                for (int y = 0; y < leafTop; y++) {
                    Vector3Int leafBlockPos = new Vector3Int(x, y, z) + (globalblockPos + Vector3Int.up * treeLenght);
                    SetBlock(leafBlockPos,BlockType.Block);
                    if (y == leafTop - 1) {
                        if (z % 2 != 0) {
                            if (x % 2 != 0) {
                                SetBlock(leafBlockPos, BlockType.None);
                            }
                        }
                    }
                }
            } 
        }
        
        SetBlock(globalblockPos + (Vector3Int.up * (int)(leafTop + treeLenght )),BlockType.Block);
        
        
    }
}

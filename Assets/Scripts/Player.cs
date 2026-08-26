using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public ChunkGen world;
    public Transform cameraT;

    public void Start() {
        //world.SetBlock(new Vector3Int(0,0,0),BlockType.Block);
    }

    // Update is called once per frame
    void Update() {
        RaycastHit hit;
        if (Physics.Raycast(cameraT.position, cameraT.forward, out hit)) {
            
            //Destroying block
            if (Input.GetMouseButtonDown(0)) {
                Vector3 hitPoint = hit.point - hit.normal * 0.1f;
                Vector3Int blockPos = Vector3Int.FloorToInt(hitPoint);
                
                Chunk chunk = world.GetChunk(blockPos);
                if (chunk != null) {
                    Vector3Int localBlockPos = blockPos - chunk.position;
                    chunk.SetBlock(localBlockPos,BlockType.None);
                    chunk.ConstructMesh();

                    //Refresh neighbor chunks
                    for (int i = 0; i < BlockMeshData.directions.Length; i++) {
                        Vector3Int faceDir = BlockMeshData.directions[i];
                        if (!chunk.CoordInBound(localBlockPos + faceDir)) {
                            Chunk neighborChunk = world.GetChunk(blockPos + faceDir);
                            if (neighborChunk != null) {
                                if (neighborChunk != chunk) {
                                    neighborChunk.ConstructMesh();
                                    //Debug.Log("current: " + chunk + "neighbor: " + neighborChunk + " BlockPos" + blockPos);
                                }
                            
                            }
                        }
                        
                        
                    }
                }
            }
        }
        
        //Placing block
        if (Input.GetMouseButtonDown(1)) {
            Vector3 hitPoint = hit.point + hit.normal * 0.1f;
            Vector3Int blockPos = Vector3Int.FloorToInt(hitPoint);
                
            Chunk chunk = world.GetChunk(blockPos);
            if (chunk != null) {
                Vector3Int localBlockPos = blockPos - chunk.position;
                chunk.SetBlock(localBlockPos,BlockType.Block);
                chunk.ConstructMesh();
            }
        }

        // placing tree
        if (Input.GetKeyDown(KeyCode.T)) {
            Vector3 hitPoint = hit.point + hit.normal * 0.1f;
            Vector3Int blockPos = Vector3Int.FloorToInt(hitPoint);
            world.PlaceTree(blockPos);
            world.ConstructAllChunkMesh();
        }
    }
}

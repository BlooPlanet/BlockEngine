using System.Collections.Generic;
using UnityEngine;

public static class BlockMeshData {
    
    public static Vector3[] UpFaceVertices(Vector3Int blockPos) {
        return new [] {
            new Vector3(0, 1, 0) + blockPos,
            new Vector3(1, 1, 0) + blockPos,
            new Vector3(0, 1, 1) + blockPos,
            new Vector3(1, 1, 1) + blockPos
        };
    }
    
    public static Vector3[] DownFaceVertices(Vector3Int blockPos) {
        return new [] {
            new Vector3(0, 0, 1) + blockPos,
            new Vector3(1, 0, 1) + blockPos,
            new Vector3(0, 0, 0) + blockPos,
            new Vector3(1, 0, 0) + blockPos,
        };
    }
    
    public static Vector3[] FrontFaceVertices(Vector3Int blockPos) {
        return new [] {
            new Vector3(0, 1, 1) + blockPos,
            new Vector3(1, 1, 1) + blockPos,
            new Vector3(0, 0, 1) + blockPos,
            new Vector3(1, 0, 1) + blockPos,
        };
    }
    
    public static Vector3[] BackFaceVertices(Vector3Int blockPos) {
        return new [] {
            new Vector3(0, 0, 0) + blockPos,
            new Vector3(1, 0, 0) + blockPos,
            new Vector3(0, 1, 0) + blockPos,
            new Vector3(1, 1, 0) + blockPos
        };
    }
    
    public static Vector3[] RightFaceVertices(Vector3Int blockPos) {
        return new [] {
            new Vector3(1, 0, 0) + blockPos,
            new Vector3(1, 0, 1) + blockPos,
            new Vector3(1, 1, 0) + blockPos,
            new Vector3(1, 1, 1) + blockPos
        };
    }
    
    public static Vector3[] LeftFaceVertices(Vector3Int blockPos) {
        return new [] {
            new Vector3(0, 1, 0) + blockPos,
            new Vector3(0, 1, 1) + blockPos,
            new Vector3(0, 0, 0) + blockPos,
            new Vector3(0, 0, 1) + blockPos,
        };
    }

    public static int[] FaceTrianglution(int v) {
        return new[] {
            2 + v, 1 + v, 0 + v,
            1 + v, 2 + v, 3 + v
        };
    }

    public static Vector3Int[] directions = new[] {
        Vector3Int.up,
        Vector3Int.down,
        Vector3Int.forward,
        Vector3Int.back,
        Vector3Int.right,
        Vector3Int.left,
    };

    public static Vector3[] GetFaceVertices(int i, Vector3Int blockPos) {
        List<Vector3[]> vertices = new List<Vector3[]>() {
            UpFaceVertices(blockPos),
            DownFaceVertices(blockPos),
            FrontFaceVertices(blockPos),
            BackFaceVertices(blockPos),
            RightFaceVertices(blockPos),
            LeftFaceVertices(blockPos),
        };
        return vertices[i];
    }

    public static Vector3[] GetNormals(Vector3Int direction) {
        return new[] {
            new Vector3(direction.x, direction.y,direction.z),
            new Vector3(direction.x, direction.y,direction.z),
            new Vector3(direction.x, direction.y,direction.z),
            new Vector3(direction.x, direction.y,direction.z),
            new Vector3(direction.x, direction.y,direction.z),
            new Vector3(direction.x, direction.y,direction.z),
        };
    }

    public static Color[] GetFaceColor(Color col) {
        return new[] {
            col,
            col,
            col,
            col,
        };
    }
}

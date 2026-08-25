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
}

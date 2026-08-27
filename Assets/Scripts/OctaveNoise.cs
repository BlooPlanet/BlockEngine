using UnityEngine;

public static class OctaveNoise {
    public static float Value(float x, float y, int octaves, float scale, float lacunarity, float persistence, Vector2 seedOffset)
    {
        if (scale <= 0) scale = 0.0001f;

        float totalNoise = 0f;
        float amplitude = 1f;
        float frequency = 1f;
        float maxPossibleHeight = 0f; // Tracking to normalize back to a 0-1 range

        for (int i = 0; i < octaves; i++)
        {
            // Apply scale, current frequency, and a pseudo-random shift to prevent artifacts
            float sampleX = (x + seedOffset.x + (i * 1000.123f)) / scale * frequency;
            float sampleY = (y + seedOffset.y + (i * 5000.321f)) / scale * frequency;

            // Mathf.PerlinNoise returns a value between 0 and 1
            float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
            
            totalNoise += perlinValue * amplitude;
            maxPossibleHeight += amplitude;

            // Prepare next layer parameters
            amplitude *= persistence;
            frequency *= lacunarity;
        }

        // Return perfectly normalized 0.0 to 1.0 value
        return totalNoise / maxPossibleHeight;
    }
}
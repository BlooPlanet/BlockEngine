using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunManager : MonoBehaviour {
    
    Light sun;

    public bool cycleStart;
    
    // seconds
    float gameSunTime = 0.05f * 60 * 60;
    float totallTime = 0.1f * 60 * 60;
    
    // Start is called before the first frame update
    void Start() {
        sun = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update() {
        if (cycleStart) {
            gameSunTime += Time.deltaTime;
            if (gameSunTime > totallTime) {
                gameSunTime = 0;
            }
        
            CalculateSunPos(gameSunTime);
        }
    }

    public void CalculateSunPos(float time) {
        float timePercentage = time / totallTime;
        
        // night to day
        if (timePercentage >= 0 && timePercentage <= 0.5f) {
            float shiftPercentage = timePercentage / 0.5f;
            sun.intensity = shiftPercentage;
            transform.rotation =
                Quaternion.Lerp(Quaternion.Euler(-90, 30, 0), Quaternion.Euler(90, 30, 0), shiftPercentage);
        }

        // day to night
        if (timePercentage > 0.5f && timePercentage <= 1) {
            float shiftPercentage = (timePercentage - 0.5f) / 0.5f;
            float sunIntensity = 1 - shiftPercentage;
            sun.intensity = sunIntensity;
            
            transform.rotation =
                Quaternion.Lerp(Quaternion.Euler(90, 30, 0), Quaternion.Euler(270, 30, 0), shiftPercentage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proba : MonoBehaviour
{
    public Dictionary <int, Dictionary<Color, float>> probabilities;

    public Sensor getSensor(int distance){
        int randomValue = UnityEngine.Random.Range(0,100);
        int cumulativeProba = 0;
        if(distance>5) distance = 5;
        foreach(KeyValuePair<Color, float> entry in probabilities[distance]){
            cumulativeProba += (int)Mathf.Round(entry.Value * 100);
            if(randomValue<= cumulativeProba) return new Sensor(entry.Value, entry.Key);
        }
        return null;
    }
    void Start()
    {
        probabilities = new Dictionary<int, Dictionary<Color, float>>();
        probabilities[0] = new Dictionary<Color, float>
        {
            {Color.green, 0.02f},
            {Color.yellow, 0.03f},
            {new Color(1f, 0.5f, 0f, 1f), 0.05f},
            {Color.red, 0.9f}
        };
        probabilities[1] = new Dictionary<Color, float>
        {
            {Color.green, 0.02f},
            {Color.red, 0.03f},
            {Color.yellow, 0.1f},
            {new Color(1f, 0.5f, 0f, 1f), 0.85f}
        };
        probabilities[2] = new Dictionary<Color, float>
        {
            {Color.green, 0.02f},
            {Color.red, 0.03f},
            {Color.yellow, 0.1f},
            {new Color(1f, 0.5f, 0f, 1f), 0.85f}
        };
        probabilities[3] = new Dictionary<Color, float>
        {
            {Color.red, 0.03f},
            {new Color(1f, 0.5f, 0f, 1f), 0.07f},
            {Color.green, 0.1f},
            {Color.yellow, 0.8f}
        };
        probabilities[4] = new Dictionary<Color, float>
        {
            {Color.red, 0.03f},
            {new Color(1f, 0.5f, 0f, 1f), 0.07f},
            {Color.green, 0.1f},
            {Color.yellow, 0.8f}
        };
        probabilities[5] = new Dictionary<Color, float>
        {
            {Color.red, 0.01f},
            {new Color(1f, 0.5f, 0f, 1f), 0.03f},
            {Color.yellow, 0.2f},
            {Color.green, 0.76f}
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

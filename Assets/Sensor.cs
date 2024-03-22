using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor
{
    public float probability;
    public Color color;

    public Sensor(float probability, Color color){
        this.probability = probability;
        this.color = color;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public int posX, posY;
    // Start is called before the first frame update
    void Start()
    {
        this.posX = UnityEngine.Random.Range(0,11);
        this.posY = UnityEngine.Random.Range(0,8);

        //to have an idea where the ghost is (for testing purposes)
        Debug.Log("Ghost was positioned at ("+this.posX+","+this.posY+").");
    }
}

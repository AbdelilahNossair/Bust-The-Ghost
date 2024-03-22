using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Peep : MonoBehaviour
{

    public bool showProba = true;

    public void ChangePeep(bool isOn) {
      GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Proba");
      foreach (GameObject go in gameObjectArray) {
        go.GetComponent<Text>().enabled = !go.GetComponent<Text>().enabled;
      }
      Debug.Log($"Peep toggle is now {(isOn ? "ON" : "OFF")}");
    }

    void Start()
    {
        Toggle peepButton = GameObject.Find("Toggle").GetComponent<Toggle>();
        peepButton.onValueChanged.AddListener(ChangePeep);
    }


    void Update()
    {
        
    }
}

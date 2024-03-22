using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bust : MonoBehaviour
{
    public bool Busted = false;


    public void ChangeBust(){
        Busted = !Busted;

        Button bustButton = GameObject.Find("Bust").GetComponent<Button>();
        Debug.Log("The BUST button was Clicked");
    }
    void Start()
    {
        Button bustButton = GameObject.Find("Bust").GetComponent<Button>();
        bustButton.onClick.AddListener(ChangeBust);
    }

}

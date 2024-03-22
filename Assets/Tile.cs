using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Tile : MonoBehaviour, IPointerDownHandler
{

    private int x, y;
    private GameObject tileObj;
    private GameObject gridObj;
    private float tileSize = 50f;
    public bool sensed = false;
    public float probability;
    public GridManager gridManager;



    public Canvas canvas; // Assign this Canvas in the Unity Editor

    public void OnPointerDown(PointerEventData eventData)
    {
        //... existing code ...
        Button bustButton = GameObject.Find("Bust").GetComponent<Button>();
        Bust bust = GameObject.Find("Bust").GetComponent<Bust>();
        Text statusMessage = GameObject.Find("StatusMsg").GetComponent<Text>();
        Debug.Log($"Tile Clicked ({x},{y})");
        if (gridManager != null)
        {
            gridManager.UpdatePosteriorTiles(this.x, this.y);
        }
        else
        {
            Debug.LogError("GridManager reference is not set in Tile.");
        }

        Ghost ghost = GameObject.Find("Ghost").GetComponent<Ghost>();

        Text score = GameObject.Find("Score").GetComponent<Text>();

        Text bustRemaining = GameObject.Find("RemainingBusts").GetComponent<Text>();

        // Now, update the text values
        int currentBusts = int.Parse(bustRemaining.text.Split(':')[1].Trim());
        
        int currentScore = int.Parse(score.text.Split(':')[1].Trim());
        score.text = "Score: " + (currentScore + (ghost != null && this.x == ghost.posX && this.y == ghost.posY ? 5 : -1));

        //If the Bust Button is Pressed
        if (bust.Busted)
        {
            currentBusts -=1;
            bustRemaining.text = "Busts Remaining: " + (currentBusts); 
            if (ghost != null && this.x == ghost.posX && this.y == ghost.posY)
            {
                score.text = "Score: " + (currentScore + 5);
                statusMessage.text = "Status: YOU WON GHOST BUSTEEEEED";
                statusMessage.color = Color.green;
                Destroy(ghost);
                bustButton.interactable = false;
            }
            bust.ChangeBust();
        }
        else
        {
            score.text = "Score: " + (currentScore - 1);
        }

        //GAME OVER
        if (currentBusts == 0 || currentScore == 1)
        {
            statusMessage.text = "Status: GAME OVER";
            statusMessage.color = Color.red;
            bustButton.interactable = false;
        }
        Debug.Log($"Score : {currentScore} / Busts : {currentBusts}");
    }
     
    public void createTile(int x, int y, GameObject gridObj, GameObject tileObj)
    {
        this.x = x;
        this.y = y;
        this.gridObj = gridObj;
        this.tileObj = tileObj;

        tileObj.AddComponent<UnityEngine.UI.Image>();
        tileObj.AddComponent<Outline>();
        tileObj.AddComponent<Proba>();

        //Add a black Outline to the tiles
        Outline outline = tileObj.GetComponent<Outline>();
        outline.effectColor = new Color(41f / 255f, 153f / 255f, 253f / 255f, 1f);
        outline.effectDistance = new UnityEngine.Vector2(2, -2);
        outline.useGraphicAlpha = true;
        tileObj.transform.SetParent(gridObj.transform);

        //Set the tiles' position and sizes
        UnityEngine.UI.Image tileImg = tileObj.GetComponent<UnityEngine.UI.Image>();
        tileImg.color = new Color(66f / 255f, 20f / 255f, 189f / 255f);
        RectTransform tileRect = tileObj.GetComponent<RectTransform>();
        tileRect.sizeDelta = new UnityEngine.Vector2(tileSize, tileSize);
        tileRect.anchoredPosition = new UnityEngine.Vector2(x * tileSize, y * tileSize);

        GameObject tileProbaObj = new GameObject($"Proba Tile ({x},{y})");
        tileProbaObj.tag = "Proba";
        tileProbaObj.AddComponent<Text>();
        Text tileProba = tileProbaObj.GetComponent<Text>();
        RectTransform tileProbaRect = tileProbaObj.GetComponent<RectTransform>();
        tileProbaObj.transform.SetParent(tileObj.transform);
        this.probability = 1f / 90;
        tileProba.text = this.probability.ToString("0.00");
        tileProbaRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);
        tileProbaRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50);
        tileProba.fontSize = 14;

        //Alignment of the Probabilities into the center of tiles
        tileProba.alignment = TextAnchor.MiddleCenter;
        tileProba.horizontalOverflow = HorizontalWrapMode.Overflow;
        tileProba.verticalOverflow = VerticalWrapMode.Overflow;
        tileProba.color = Color.white;
        tileProba.font = Resources.GetBuiltinResource(typeof(Font), "LegacyRuntime.ttf") as Font;
        tileProba.transform.localPosition = new UnityEngine.Vector3(0, 0, 0);

    }

    public int getDistanceFromGhost()
    {
        Ghost ghost = GameObject.Find("Ghost").GetComponent<Ghost>();
        int distance = 0;
        if (ghost != null)
        {
            distance = Mathf.Abs(this.x - ghost.posX) + Mathf.Abs(this.y - ghost.posY);
        }
        return distance;
    }

    public void updateOutline(Color color)
    {
        Outline tileOutline = this.tileObj.GetComponent<Outline>();
        tileOutline.effectColor = color;
    }


}

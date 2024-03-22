using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public class GridManager : MonoBehaviour
{

    //Let's start by defining the size of the grid
    private int gridSizeX = 12; // Number of tiles in the X direction (number of COLUMNS)
    private int gridSizeY = 9; // Number of tiles in the Y direction (number of ROWS)

    private GameObject Ghost;

    private Tile[,] gridArray;


    void Start()
    {
        //Create a Canvas and Set it to be in Screen Space
        GameObject canvasObj = new GameObject("Canvas");
        canvasObj.AddComponent<Canvas>();
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();

        Canvas canvas = canvasObj.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Create a Panel Object, Set its background color to BLACK, Set its size to fit in Screen
        GameObject panelObj = new GameObject("Panel");
        panelObj.AddComponent<RectTransform>();
        panelObj.AddComponent<Image>();
        panelObj.transform.SetParent(canvasObj.transform);

        Image panelImage = panelObj.GetComponent<Image>();
        panelImage.color = Color.black;

        RectTransform panelRect = panelObj.GetComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0f, 0f);
        panelRect.anchorMax = new Vector2(1f, 1f);
        panelRect.offsetMin = new Vector2(0f, 0f);
        panelRect.offsetMax = new Vector2(0f, 0f);

        // Create the holder of the Grid of tiles, Attach it to the Panel and Set the grid's position in the screen
        GameObject gridObj = new GameObject("Grid");
        gridObj.AddComponent<RectTransform>();
        gridObj.transform.SetParent(panelObj.transform);

        RectTransform gridRect = gridObj.GetComponent<RectTransform>();
        gridRect.anchorMin = new Vector2(0f, 1f);
        gridRect.anchorMax = new Vector2(0f, 1f);
        gridRect.pivot = new Vector2(0f, 1f);
        gridRect.anchoredPosition = new Vector2(0f, 0f);
        gridRect.sizeDelta = new Vector2(125, 900);

        //Generate the grid of tiles
        gridArray = new Tile[gridSizeX, gridSizeY];
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                //Create a tile Object
                GameObject tileObj = new GameObject($"Tile ({x},{y})");
                tileObj.AddComponent<Tile>();
                Tile tile = tileObj.GetComponent<Tile>();
                tile.createTile(x, y, gridObj, tileObj);
                tile.gridManager = this;
                gridArray[x, y] = tile;
            }
        }

        this.Ghost = new GameObject("Ghost");
        Ghost.AddComponent<Ghost>(); 

        // Create Score Text GameObject 
        GameObject scoreGO = new GameObject("Score");
        scoreGO.transform.SetParent(canvas.transform, false); 
        Text scoreText = scoreGO.AddComponent<Text>();
        scoreText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf"); 
        scoreText.color = Color.white;
        scoreText.text = "Score: 20";
        scoreText.fontSize = 32;
        scoreText.alignment = TextAnchor.MiddleLeft;

        RectTransform ScoreRectTransform = scoreText.GetComponent<RectTransform>();
        ScoreRectTransform.anchoredPosition = new Vector2(300, 50); 
        ScoreRectTransform.sizeDelta = new Vector2(300, 50); 

        // Create Busts Remaining Text GameObject
        GameObject bustsGO = new GameObject("RemainingBusts");
        bustsGO.transform.SetParent(canvas.transform, false); 
        Text bustsText = bustsGO.AddComponent<Text>();
        bustsText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf"); 
        bustsText.color = Color.white;
        bustsText.text = "Busts Remaining: 2"; 
        bustsText.fontSize = 30;
        bustsText.alignment = TextAnchor.MiddleLeft;

        RectTransform bustsRectTransform = bustsGO.GetComponent<RectTransform>();
        bustsRectTransform.anchoredPosition = new Vector2(300, 0); 
        bustsRectTransform.sizeDelta = new Vector2(300, 50); 

        //Create a Status Message
        GameObject statusMessage = new GameObject("StatusMsg");
        statusMessage.transform.SetParent(canvas.transform, false); 
        Text statusMsgText = statusMessage.AddComponent<Text>();
        statusMsgText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf"); 
        statusMsgText.color = Color.white;
        statusMsgText.text = "Status: Ongoing";
        statusMsgText.fontSize = 30;
        statusMsgText.alignment = TextAnchor.MiddleCenter;

        RectTransform StatusRectTransform = statusMessage.GetComponent<RectTransform>();
        StatusRectTransform.anchoredPosition = new Vector2(300, 270); 
        StatusRectTransform.sizeDelta = new Vector2(350, 150); 

    }
    //make sure of this !!!!!!!!!!!!
    public int getDistance(int x, int y, int sx, int sy)
    {
        int dist = Math.Abs(x - sx) + Math.Abs(y - sy);
        if (dist > 5) return 5;
        return dist;
    }
    
    public void UpdatePosteriorTiles(int selectedTileX, int selectedTileY)
    {
        Tile stile = gridArray[selectedTileX, selectedTileY];
        Proba sprobability = stile.GetComponent<Proba>();
        Sensor result = sprobability.getSensor(stile.getDistanceFromGhost());
        stile.updateOutline(result.color);

        float sum = 0f;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                int distance = getDistance(x, y, selectedTileX, selectedTileY);
                Tile tile = gridArray[x, y];
                Proba probability = tile.GetComponent<Proba>();
                tile.probability *= probability.probabilities[distance][result.color];
                sum += tile.probability;
            }
        }
        //Let's Normalize to have distributions
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Tile tile = gridArray[x, y];
                Text tileProba = GameObject.Find($"Proba Tile ({x},{y})").GetComponent<Text>();
                tile.probability /= sum;
                tileProba.text = tile.probability < 0.01f ? "<0.01" : tile.probability.ToString("0.00");
            }
        }
    }


}
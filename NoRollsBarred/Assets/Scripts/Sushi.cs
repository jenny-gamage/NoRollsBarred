﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//parent object to a group of SushiCells that make up a game piece
//the shape of the game piece is determined by the position of the child SushiCell objects
public class Sushi : MonoBehaviour
{
    private InvalidPlacementDefaultLocation defaultLocation;
    public bool isStationary = true;
    private bool snapEnabled = true;
    private static float sceneWidth;
    private static float sceneHeight;
    private static bool sceneSizeIsInitialized= false;
    public static Sushi selectedSushi;
    private static Vector3 lastMousePos;
    private SushiCell[] cells;
    [HideInInspector] public bool moving = false; // is true if it's on the conveyor belt, false otherwise

    // Start is called before the first frame update
    void Start()
    {
        cells = GetComponentsInChildren<SushiCell>();
        if(!sceneSizeIsInitialized)
        {
            InitializeSceneSize();
        }
        defaultLocation = FindObjectOfType<InvalidPlacementDefaultLocation>();
    }

    void Update()
    {
        if (selectedSushi == this)
        {
            gameObject.transform.position += new Vector3((Input.mousePosition.x - lastMousePos.x) / Screen.width * sceneWidth, (Input.mousePosition.y - lastMousePos.y) / Screen.height * sceneHeight);
            lastMousePos = Input.mousePosition;
        }
    }

    internal void Deselect()
    {
        moving = true; // TODO: should only be true if it goes back in the conveyor.
        selectedSushi = null;
    }

    internal void Select()
    {
        selectedSushi = this;
        snapEnabled = true;
        foreach(Sushi sushi in FindObjectsOfType<Sushi>())
        {
            sushi.isStationary = true;
        }
        isStationary = false;
        lastMousePos = Input.mousePosition;
    }
    internal void SnapToGrid(GridCell gridCell)
    {
        if (snapEnabled&&gridCell.sushiInCell == cells[0])
        {
            gameObject.transform.position += gridCell.transform.position - cells[0].transform.position;
        }
    }

    internal void RejectMove()
    {
<<<<<<< HEAD
        if (GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition).y < (GameObject.Find("Shutter").transform.position.y)-sceneHeight/2f) {
            // this is a bit hacky, but it works and doesn't pick up a sushi if it's behind the shutter.
            selectedSushi = this;
            moving = false;
            lastMousePos = Input.mousePosition;
        }
=======
        snapEnabled = false;
        gameObject.transform.position = defaultLocation.transform.position;
>>>>>>> grid
    }

    private static void InitializeSceneSize()
    {
        Vector2 topRightCorner = Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f));
        sceneWidth = topRightCorner.x*2;
        sceneHeight = topRightCorner.y*2;
        sceneSizeIsInitialized = true;
    }
}

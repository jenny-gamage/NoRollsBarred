﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // the following lists will be pre-populated with prefabbed GameObjects, for selecting pieces and plates from.
    public GameObject[] masterPlateList;
    public GameObject[] masterPieceList;
    // these two lists start empty, and are populated at the start of each game, or when they need to be re-populated.
    [HideInInspector] public List<List<int>> pieceList = new List<List<int>>();
    [HideInInspector] public List<int> plateList = new List<int>();
    public List<Color> colors;
    //int* nextPiece; // updates as pieces are rendered onto the conveyor belt
    private int piecesToLoad = 100;
    private int platesToLoad = 2;
    public float shutterValue = 0.0f; // ranges from 0 to 100.  0 is at shutter pos'n y=20, 100 is y=0 and game over.

    [HideInInspector] public float level;
    [HideInInspector] public float score;
    [HideInInspector] private float levelTimer;
    public GameObject explosion;

    void makePieces()
    {
        for (int i = 0; i < piecesToLoad; i++)
        {
            List<int> thing = new List<int>(); thing.Add(UnityEngine.Random.Range(0, masterPieceList.Length)); thing.Add(UnityEngine.Random.Range(0, colors.Count));
            pieceList.Add(thing);
        }
    }

    internal GameObject GetNextPlate()
    {
        if(plateList.Count==0)
        {
            PopulatePlateList();
        }
        GameObject platePrefab = masterPlateList[plateList[0]];
        plateList.RemoveAt(0);
        return platePrefab;
    }

    // Start is called before the first frame update
    void Awake()
    {
        colors.Add(Color.red); colors.Add(Color.blue); colors.Add(Color.yellow);
        makePieces();
        PopulatePlateList();
        level = 0f; score = 0f;
    }

    private void PopulatePlateList()
    {
        for (int i = 0; i < platesToLoad; i++)
        {
            int plate = UnityEngine.Random.Range(0, masterPlateList.Length);
            while (plateList.Contains(plate))
            {
                plate = UnityEngine.Random.Range(0, masterPlateList.Length); // this guarantees no repeat plates
            }
            plateList.Add(plate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        shutterValue += Time.deltaTime * (level*0.1f); // if the game is going too fast, adjust this 1 to something lower
        levelTimer += Time.deltaTime;
        if (levelTimer>=10) { level += 1; levelTimer = 0; } // every 10 seconds, level increments
        // TODO: game over state if shutterValue>=100
        if (pieceList.Count == 0) makePieces();

    }
}

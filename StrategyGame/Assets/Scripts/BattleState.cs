﻿using System.Collections.Generic;
using UnityEngine;

public class BattleState : MonoBehaviour {
    private Tile[,] board;
    private GameObject[,] cubes;
    private IList<Character> party;
    private IList<Character> enemies;
    private TurnOrder order;
    private Character current;

    // Use this for initialization
    void Start () {
        board = new Board().getBoard();
        createParty();
        createEnemies();
        order = new TurnOrder(party, enemies);
        nextTurn();
        createBoard();
        
	}
	
	// Update is called once per frame
	void Update () {
        occupyTiles(party);
        occupyTiles(enemies);
        for (int i = 0; i < cubes.GetLength(0); i++)
        {
            for (int j = 0; j < cubes.GetLength(1); j++)
            {
                if (!board[i, j].getOccupied())
                {
                    cubes[i, j].GetComponent<Renderer>().material.color = new Color(255, 255, 255);
                }
                else if(current.getY() == i && current.getX() == j)
                {
                    cubes[i, j].GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                }
                else
                {
                    cubes[i, j].GetComponent<Renderer>().material.color = new Color(0, 0, 0);
                }
            }
        }
    }

    private void createParty()
    {
        party = new List<Character>();
        party.Add(new Character(3, 2));
    }

    private void createEnemies()
    {
        enemies = new List<Character>();
        enemies.Add(new Character(1, 1));
    }

    private void createBoard()
    {
        cubes = new GameObject[board.GetLength(1), board.GetLength(0)];
        
        for (int i = 0; i < cubes.GetLength(0); i++)
        {
            for (int j = 0; j < cubes.GetLength(1); j++)
            {
                cubes[i,j] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubes[i,j].transform.position = new Vector3(i, 0, j);
                cubes[i, j].transform.parent = this.transform;
                
            }
        }
    }

    private void occupyTiles(IList<Character> list)
    {
        foreach (Character c in list)
        {
            if (c.getX() < board.GetLength(1) && c.getY() < board.GetLength(0))
            {
                board[c.getY(), c.getX()].occupy();
            }
        }
    }

    private void nextTurn()
    {
        current = order.getNext();
    }
}

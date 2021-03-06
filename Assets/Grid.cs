﻿using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
   
    // Создадим сетку
    public static int w = 20;
    public static int h = 40;
    public static Transform[,] grid = new Transform[w, h];
    //Округление положения
    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x),
        Mathf.Round(v.y));
    }
    //Определить нахождение внутри границы
    public static bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 &&
        (int)pos.x < w &&
        (int)pos.y >= 0);
    }
    //Удалить строку
    public static void deleteRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }
    //Определить есть ли строка внизу
    public static void decreaseRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] != null)
            {
                // Опустить строку на уровеь
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                // Update Block position
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }
    //Опустить остальные строки
    public static void decreaseRowsAbove(int y)
    {
        for (int i = y; i < h; ++i)
            decreaseRow(i);
    }
    //Проверка заполненности строки
    public static bool isRowFull(int y)
    {
        for (int x = 0; x < w; ++x)
            if (grid[x, y] == null)
                return false;
        return true;
    }

    //Удаление строк, если есть пустая внизу
    public static void deleteFullRows()
    {
        for (int y = 0; y < h; ++y)
        {
            if (isRowFull(y))
            {
                deleteRow(y);
                decreaseRowsAbove(y + 1);
                --y;
            }
        }
    }



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

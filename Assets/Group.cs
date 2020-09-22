using UnityEngine;
using System.Collections;

public class Group : MonoBehaviour {

    // отслеживает время падения 
    float lastFall = 0;
    //Отслживаем нахождение каждого блока
    public bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);

            // Проверка на нахождение между границ
            if (!Grid.insideBorder(v))
                return false;

            // Блок в сетке и не пересекается с другим блоком
            if (Grid.grid[(int)v.x, (int)v.y] != null &&
            Grid.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }
    //Изменение позиции блоков группы при повороте фигуры
    void updateGrid()
    {
        // Удалить старые блоки группы
        for (int y = 0; y < Grid.h; ++y)
            for (int x = 0; x < Grid.w; ++x)
                if (Grid.grid[x, y] != null)
                    if (Grid.grid[x, y].parent == transform)
                        Grid.grid[x, y] = null;

        // Добавить новые блоки групы
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);
            Grid.grid[(int)v.x, (int)v.y] = child;
        }
    }
    // Use this for initialization
    void Start () {

        // Если не хватает места для создания новой фигуры - закончить игру
        if (!isValidGridPos())
        {
            Debug.Log("GAME OVER");
            Destroy(gameObject);
        }
    }
}
	
	// Update is called once per frame
	void Update () {
        // Влево
        if (Input.GetKeyDown (KeyCode . LeftArrow)) {
        // Изменить позицию
            transform . position += new Vector3(-2, 0, 0);

            // Проверка возможности
            if ( isValidGridPos ( ))
                // Если возможно - изменить.
                updateGrid ( );
            else
                // Если не возможно вернуть.
                transform . position += new Vector3(2, 0, 0);
        }
    
    
    //Вправо
    // Получаем сигнал движения вправо

    else if ( Input.GetKeyDown ( KeyCode.RightArrow ) ) {
        // Изменение позиции
        transform . position += new Vector3( 2 , 0 , 0 );

        // Проверка изменения 
        if ( isValidGridPos( ) )
        // Если возможно - обновить сетку
        updateGrid ( );
        else
        // Если нет отменить.
            transform . position += new Vector3( - 2 , 0 , 0 );
} 
     // Поворот
     else if ( Input.GetKeyDown ( KeyCode.UpArrow ) ) {
        transform . Rotate ( 0 , 0 , - 90 ) ;

        // Проверка возможности
        if ( isValidGridPos ( ) )
        // Если возможно - обновить сетку.
        updateGrid ( );
        else
        // Если невозможно - вернуть
        transform . Rotate ( 0 , 0 , 90 ) ;
        }


         // Движение вниз и падение
             else if (Input.GetKeyDown(KeyCode.DownArrow) ||
             Time.time - lastFall >= 1)
        {
            // Изменитьт позицию
            transform . position += new Vector3(0, -1, 0);

            // Проверить возможность
            if (isValidGridPos ( ))
            {
                // Если возможно - изменить.
                updateGrid ( );
            }
            else {
                // Если не возможно - вернуть
                transform . position += new Vector3(0, 1, 0);

                // Удалить заполненные горизонтальные линии
                Grid.deleteFullRows();

                // Создать новую фигуру
                FindObjectOfType<Spawner>().spawnNext();

                // Отключить скрипт
                enabled = false;
            }

            lastFall = Time.time;
        }
    }
}
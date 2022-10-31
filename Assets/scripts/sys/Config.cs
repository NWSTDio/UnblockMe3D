using UnityEngine;

public class Config
    {
    public enum DIRECTION { UP, RIGHT, DOWN, LEFT } // направление свайпов
    public enum AXIS { HORIZONTAL, VERTICAL } // направление свайпов

    public static int Level = 1, MaxLevel = 2;
    public static bool AUDIO = true;
    public static bool RUNNING = true;
    public static bool SPLASH = false;

    public static void NextLevel()
        {
        if (Level >= MaxLevel)
            return;
        Level++;
        }
    
    public static DIRECTION Direction(Vector2 current)
        {
        DIRECTION direction;

        if (Mathf.Abs(current.x) > Mathf.Abs(current.y))
            direction = (current.x > 0 ? DIRECTION.RIGHT : DIRECTION.LEFT);
        else
            direction = (current.y > 0 ? DIRECTION.UP : DIRECTION.DOWN);

        return direction;
        }
    }
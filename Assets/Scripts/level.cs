using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class level : MonoBehaviour
{
 //   public bool IsAutomatic;
 //   public bool HideIcon;
    public string SceneToLoad;

    [Header("Levels")] //
    public level UpLevel;
    public level DownLevel;
    public level LeftLevel;
    public level RightLevel;

    private Dictionary<Direction, level> levelDirections;
    public bool unlockedLevel; //this will determine whether the levels are locked or not 
    public Image lockedImage; //if the levels are locked, this image will appear over the level 


    private void Start()
    {
        // Load the directions into a dictionary for easy access
        levelDirections = new Dictionary<Direction, level>
        {
            { Direction.Up, UpLevel },
            { Direction.Down, DownLevel },
            { Direction.Left, LeftLevel },
            { Direction.Right, RightLevel }
        };

        // Hide the icon if needed
       /* if (HideIcon)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }*/
    }

    public level GetLevelInDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return UpLevel;
            case Direction.Down:
                return DownLevel;
            case Direction.Left:
                return LeftLevel;
            case Direction.Right:
                return RightLevel;
            default:
                throw new ArgumentOutOfRangeException("direction", direction, null);
        }
    }


   
    public level GetNextLevel(level Level)
    {
        return levelDirections.FirstOrDefault(x => x.Value != null && x.Value != Level).Value;
    }

}
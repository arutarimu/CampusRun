using UnityEngine;
using UnityEngine.UI;

public class campusManager : MonoBehaviour
{
    public characterControl Character;
    public level StartLevel;
    public Text SelectedLevelText;


    private void Start()
    {
        // Pass a ref and default the player Starting Level
        Character.Initialise(this, StartLevel);
    }

    private void Update()
    {
        if (Character.isMoving) return;
        CheckForInput();
    }

    private void CheckForInput()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            Character.TrySetDirection(Direction.Up);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            Character.TrySetDirection(Direction.Down);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Character.TrySetDirection(Direction.Left);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            Character.TrySetDirection(Direction.Right);
        }
    }

}
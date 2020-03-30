using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class characterControl : MonoBehaviour
{
    public float Speed = 3f;
    public bool isMoving { get; private set; }
    public Image lockimg;
    public Animator animator;
    public level currentLevel { get; private set; }

    public GameObject Victory;
    public GameObject Credits;

    private level _targetLevel;
    private campusManager _campusManager;
    private bool isRight;
    

    public void Initialise(campusManager campusManager, level startLevel)
    {
        _campusManager = campusManager;
        SetCurrentLevel(startLevel);
    }
    
    
    /// <summary>
    /// This runs once a frame
    /// </summary>
    private void Update()

    {
        
        float horizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        int levelReached = PlayerPrefs.GetInt("levelReached");
        if (levelReached == 2)
        {
            lockimg.enabled = false;
            currentLevel.unlockedLevel = true;
        }

        if (levelReached >= 3)
        {
            lockimg.enabled = false;
            currentLevel.unlockedLevel = true;
            Victory.SetActive(true);
            Credits.SetActive(true);
            
        }

        if (currentLevel.unlockedLevel && Input.GetKey("space"))
        {
            SceneManager.LoadScene(currentLevel.SceneToLoad);

        }

        if (_targetLevel == null) return;

        // Get the characters current position and the targets position
        var currentPosition = transform.position;
        var targetPosition = _targetLevel.transform.position;

        // If the character isn't that close to the target move closer
        if (Vector3.Distance(currentPosition, targetPosition) > .02f)
        {
            transform.position = Vector3.MoveTowards(
                currentPosition,
                targetPosition,
                Time.deltaTime * Speed
            );
        }
        else
        {
            SetCurrentLevel(_targetLevel);

        }
    }

    
    /// <summary>
    /// Check the if the current Level has a reference to another in a direction
    /// If it does the move there
    /// </summary>
    /// <param name="direction"></param>
    public void TrySetDirection(Direction direction)
    {
        // Try get the next Level
        var Level = currentLevel.GetLevelInDirection(direction);
        
        // If there is a Level then move to it
        if (Level == null) return;
        MoveToLevel(Level);
    }

   


    /// <summary>
    /// Move to a new Level
    /// </summary>
    /// <param name="Level"></param>
    private void MoveToLevel(level Level)
    {
        _targetLevel = Level;
        isMoving = true;
    }


    /// <summary>
    /// Set the current Level
    /// </summary>
    /// <param name="Level"></param>
    public void SetCurrentLevel(level Level)

    {
        
        currentLevel = Level;
        _targetLevel = null;
        transform.position = Level.transform.position;
        isMoving = false;

    }
}
using UnityEngine;

/// <summary>
/// Stores calibration data for trial use in a single place.
/// </summary>
public class GlobalControl : MonoBehaviour {
    // participant ID to differentiate data files
    public string participantID = "";

    // The single instance of this class
    public static GlobalControl Instance;

    // The time limit of the game in seconds
    public float timeLimit = 300f;

    // Which difficulty level the game is at
    public int levelNumber = 1;

    public string tryNumber = "";

    /// <summary>
    /// Assign instance to this, or destroy it if Instance already exits and is not this instance.
    /// </summary>
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class resolution
{
    public static readonly Vector2Int res854x480 = new Vector2Int(854,480);
    public static readonly Vector2Int res1366x768 = new Vector2Int(1366,768);
    public static readonly Vector2Int res1600x900 = new Vector2Int(1600,900);
    public static readonly Vector2Int res1920x1080 = new Vector2Int(1920,1080);
    public static readonly Vector2Int res1920x1200 = new Vector2Int(11920,1200);
    public static readonly Vector2Int res2560x1080 = new Vector2Int(2560,1080);
    public static readonly Vector2Int res3440x1440 = new Vector2Int(3440,1440);
}


public class OptionsStatus
{
    private float masterVolume;
    private float xSensitivity;
    private float ySensitivity;
    private float brightness;
}

public class OptionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

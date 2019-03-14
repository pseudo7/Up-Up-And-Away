using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static Level currentLevel = Level.Level1;

    public GameObject sectorPrefab;
    public GameObject finishPrefab;
    public Transform wallTransform;
    public Transform pillarTransform;
    public Vector3 origin;
    public Material wallMat;

    static Transform lastSpawnedSegment;
    static int currentHeightIndex;

    void Start()
    {
        currentHeightIndex = 0;
        lastSpawnedSegment = null;
        UIManager.Instance.UpdateLevelText(currentLevel);
        CreatePlatforms(LevelManager.Instance.GetLevelInfo(currentLevel));
        PlayerPrefManager.IsLevelUnlocked = (int)currentLevel;
    }

    void CreatePlatform(PlatformInfo info)
    {
        float segmentRotation = info.evenlyDistribute ? 360 / info.segmentCount : Constants.SEGMENT_WIDTH;
        GameObject sectorParent = new GameObject("Sector Parent " + currentHeightIndex);
        for (int i = 0; i < info.segmentCount; i++) Instantiate(sectorPrefab, origin + Vector3.up * currentHeightIndex * Constants.PLATFORM_HEIGHT, Quaternion.Euler(0, i * segmentRotation, 0), sectorParent.transform);
        sectorParent.AddComponent<Rotatable>().speed = info.rotationSpeed;
        sectorParent.transform.eulerAngles = lastSpawnedSegment ? new Vector3(0, lastSpawnedSegment.eulerAngles.y + Random.Range(0, 360 / Constants.PLATFORM_ROTATION) * Constants.PLATFORM_ROTATION, 0) : Vector3.zero;
        sectorParent.transform.localScale = new Vector3(2, 1, 2);
        lastSpawnedSegment = sectorParent.transform;
        currentHeightIndex++;
    }

    void LateUpdate()
    {
        wallMat.mainTextureOffset = new Vector2(0, Time.time);
    }

    public void CreatePlatforms(LevelInfo level)
    {
        foreach (PlatformInfo info in level.platforms) CreatePlatform(info);
        Instantiate(finishPrefab, origin + Vector3.up * level.platformCount * Constants.PLATFORM_HEIGHT, Quaternion.identity);
        wallTransform.localScale = new Vector3(1, level.platformCount * Constants.PLATFORM_HEIGHT, 1);
        pillarTransform.localScale = new Vector3(1, level.platformCount * Constants.PLATFORM_HEIGHT / 2, 1);
        wallMat.mainTextureScale = new Vector2(5, level.platformCount * Constants.PLATFORM_HEIGHT / 2);
        PlatformCamera.allowRotation = level.allowCameraRotation;
    }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Pseudo/Capture")]
    static void Capture()
    {
        ScreenCapture.CaptureScreenshot(string.Format("{0}.png", System.DateTime.Now.Ticks.ToString()));
    }
#endif
}

public struct PlatformInfo
{
    public bool evenlyDistribute;
    public int segmentCount;
    public float rotationSpeed;

    public PlatformInfo(bool evenlyDistribute, int segmentCount, float rotationSpeed)
    {
        this.evenlyDistribute = evenlyDistribute;
        this.segmentCount = segmentCount;
        this.rotationSpeed = rotationSpeed;
    }
}
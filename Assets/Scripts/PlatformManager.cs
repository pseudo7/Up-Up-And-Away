using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject sectorPrefab;
    public Vector3 origin;

    static Transform lastSpawnedSegment;
    static int currentHeightIndex;

    private void OnEnable()
    {
        CreatePlatforms(LevelManager.Instance.LoadLevel(LevelName.Level1).platforms);
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

    public void CreatePlatforms(PlatformInfo[] platforms)
    {
        foreach (PlatformInfo info in platforms) CreatePlatform(info);
    }
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
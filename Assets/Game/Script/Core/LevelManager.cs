using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stage { One, Two, Three, Finish };
public class LevelManager : Singleton<LevelManager>
{

    public GridSpawner SpawnerSecondFloor;
    public GridSpawner SpawnerThirdFloor;
    public Transform SecondSpawnerPos;
    public Transform ThirdSpawnerPos;

}

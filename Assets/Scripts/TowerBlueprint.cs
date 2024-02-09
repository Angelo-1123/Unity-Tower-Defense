using System.Collections;
using UnityEngine;

[System.Serializable]
public class TowerBlueprint
{
    public GameObject prefab;
    public float cost;
    public float range; //Same as the tower range, but for drawing before it is built, needs to have the same value.

    public GameObject lvl2TowerPrefab;
    public float lvl2Cost;
    public float lvl2range;
    public GameObject lvl3TowerPrefab;
    public float lvl3Cost;
    public float lvl3range;
    public GameObject lvl4TowerPrefab;
    public float lvl4Cost;
    public float lvl4range;

    public float sellAmount (int level)
    {
        switch (level)
        {
            case 1:
                return (cost*0.9f);
            case 2:
                return (lvl2Cost*0.5f);
            case 3:
                return (lvl3Cost*0.5f);
            case 4:
                return (lvl4Cost*0.5f);
            default:
                Debug.LogError("SellAmount function not working as is should.");
                return 0;
        }
    }
}

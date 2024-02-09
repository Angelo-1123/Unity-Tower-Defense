using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake ()
    {
        if(instance != null)
        {
            Debug.LogError("More than one Build Manager Instance");
            return;
        }
        else
        {
            instance = this;
        }
    }

    public GameObject buildEffect;
    public GameObject sellEffect;

    public TowerBlueprint towerToBuild;
    public Node selectedNode;
    public NodeUI nodeUI;

    public bool CanBuild;

    void Start ()
    {
        towerToBuild = null;
        CanBuild = false;
    }

    public void SelectNode (Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        towerToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        CanBuild = false;
        nodeUI.Hide();
    }

    public void SelectTowerToBuild (TowerBlueprint tower)
    {
        towerToBuild = tower;
        DeselectNode();
    }

    public TowerBlueprint GetTowerToBuild ()
    {
        return towerToBuild;
    }
}
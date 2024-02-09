using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject tower;
    [HideInInspector]
    public TowerBlueprint towerBlueprint;
    [HideInInspector]
    public int towerLevel = 0;
    public LineRenderer circleRenderer;
    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;
    void Start ()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
        towerBlueprint = null;
    }

    public Vector3 GetBuildPosition ()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown ()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (towerBlueprint != null)
        {
            buildManager.SelectNode(this);
            if(buildManager.selectedNode == true)
            {
                circleRenderer.enabled = true;
                switch(towerLevel)
                {
                    case(1):
                        DrawCircle(100, towerBlueprint.range);
                        break;
                    case(2):
                        DrawCircle(100, towerBlueprint.lvl2range);
                        break;
                    case(3):
                        DrawCircle(100, towerBlueprint.lvl3range);
                        break;
                    case(4):
                        DrawCircle(100, towerBlueprint.lvl4range);
                        break;                                
                }
            }
            else
            {
                circleRenderer.enabled = false;
            }
            return;
        }



        if(buildManager.towerToBuild != null && buildManager.CanBuild == true)
        {
            BuildTower(buildManager.GetTowerToBuild());
            //buildManager.CanBuild = false;
        }
        
    }

    void BuildTower (TowerBlueprint blueprint)
    {
        if(PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("More gold is required!");
            return; 
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _tower = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        tower = _tower;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        towerLevel = 1;

        towerBlueprint = blueprint;

        Debug.Log ("Tower Built!");
    }

    public void UpgradeTower ()
    {
        switch(towerLevel)
        {            
            case 1:
                if(PlayerStats.Money < towerBlueprint.lvl2Cost)
                {
                    Debug.Log("More gold is required!");
                    return; 
                }

                Destroy(tower);

                PlayerStats.Money -= towerBlueprint.lvl2Cost;
                GameObject tower2 = (GameObject)Instantiate(towerBlueprint.lvl2TowerPrefab, GetBuildPosition(), Quaternion.identity);
                tower = tower2;
                break;
            case 2:
                if(PlayerStats.Money < towerBlueprint.lvl3Cost)
                {
                    Debug.Log("More gold is required!");
                    return; 
                }

                Destroy(tower);


                PlayerStats.Money -= towerBlueprint.lvl3Cost;
                GameObject tower3 = (GameObject)Instantiate(towerBlueprint.lvl3TowerPrefab, GetBuildPosition(), Quaternion.identity);
                tower = tower3;
                break;
            case 3:
                if(PlayerStats.Money < towerBlueprint.lvl4Cost)
                {
                    Debug.Log("More gold is required!");
                    return; 
                }

                Destroy(tower);


                PlayerStats.Money -= towerBlueprint.lvl4Cost;
                GameObject tower4 = (GameObject)Instantiate(towerBlueprint.lvl4TowerPrefab, GetBuildPosition(), Quaternion.identity);
                tower = tower4;
                break;
            case 4:
                Debug.Log("Tower Already Fully Upgraded!");
                break;
            default:
                Debug.Log("Switch Case in upgrading towers not working as it should.");
                break;
        }

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        towerLevel++;

        Debug.Log ("Tower Upgraded!");
      
    }

    public void SellTower ()
    {
        PlayerStats.Money += towerBlueprint.sellAmount(towerLevel);
        
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        
        Destroy(tower);

        towerBlueprint = null;

        towerLevel = 0;
    }

    public void DrawCircle (int steps, float radius)
    {
        circleRenderer.positionCount = steps;

        for (int currentStep = 0; currentStep < steps; currentStep++)
        {
            float circunferenceProgress = (float)currentStep/steps;

            float currentRadian = circunferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float zScaled = Mathf.Sin(currentRadian);
            
            float x = xScaled * radius;
            float z = zScaled * radius;

            Vector3 currentPosition = new Vector3(x + transform.position.x, 3, z + transform.position.z);

            circleRenderer.SetPosition(currentStep, currentPosition);
        }
    }

    void OnMouseEnter ()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if ( towerBlueprint != null || (buildManager.towerToBuild != null && PlayerStats.Money >= buildManager.towerToBuild.cost))
        {
            rend.material.color = hoverColor;
            if(buildManager.towerToBuild != null && towerLevel == 0)
            {
                circleRenderer.enabled = true;
                DrawCircle(100, buildManager.towerToBuild.range);
            }
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }


        if(buildManager.towerToBuild == null)
        {
            return;
        }
        
    }

    void OnMouseExit ()
    {
        rend.material.color = startColor;
        if(circleRenderer.enabled == true && buildManager.selectedNode == null)
        {
            circleRenderer.enabled = false;
        }

    }
}

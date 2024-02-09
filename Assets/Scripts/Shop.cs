using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public TowerBlueprint arrowTowerPrefab;
    public TextMeshProUGUI arrowTowerCost;
    public TowerBlueprint cannonTowerPrefab;
    public TextMeshProUGUI cannonTowerCost;
    public TowerBlueprint iceTowerPrefab;
    public TextMeshProUGUI iceTowerCost;
    BuildManager buildManager;
    void Start ()
    {
        buildManager = BuildManager.instance;
        arrowTowerCost.text = "$" + arrowTowerPrefab.cost;
        cannonTowerCost.text = "$" + cannonTowerPrefab.cost;
        iceTowerCost.text = "$" + iceTowerPrefab.cost;
    }
    public void SelectArcherTower ()
    {
        Debug.Log("Archer Tower Purchased");
        buildManager.SelectTowerToBuild(arrowTowerPrefab);
        buildManager.CanBuild = true;

    }
    public void SelectCannonTower ()
    {
        Debug.Log("Cannon Tower Purchased");
        buildManager.SelectTowerToBuild(cannonTowerPrefab);
        buildManager.CanBuild = true;
    }
    public void SelectIceTower ()
    {
        Debug.Log("Ice Tower Purchased");
        buildManager.SelectTowerToBuild(iceTowerPrefab);
        buildManager.CanBuild = true;
    }

}

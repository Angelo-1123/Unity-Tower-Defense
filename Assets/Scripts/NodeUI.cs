using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    public TextMeshProUGUI upgradeCost;
    public Button upgradeButton;
    public TextMeshProUGUI sellCost;
    private Node target;
    

    public void SetTarget (Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        switch(target.towerLevel)
        {
            case 1:
                upgradeCost.text = "$" + target.towerBlueprint.lvl2Cost;
                sellCost.text = "$" + target.towerBlueprint.sellAmount(1);
                upgradeButton.interactable = true;
                break;
            case 2:
                upgradeCost.text = "$" + target.towerBlueprint.lvl3Cost;
                sellCost.text = "$" + target.towerBlueprint.sellAmount(2);
                upgradeButton.interactable = true;
                break;
            case 3:
                upgradeCost.text = "$" + target.towerBlueprint.lvl4Cost;
                sellCost.text = "$" + target.towerBlueprint.sellAmount(3);
                upgradeButton.interactable = true;
                break;
            case 4:
                upgradeCost.text = "-";
                sellCost.text = "$" + target.towerBlueprint.sellAmount(4);
                upgradeButton.interactable = false;
                break;
        }
        ui.SetActive(true);
    }
    
    public void Hide ()
    {
        ui.SetActive(false);
    }

    public void Upgrade ()
    {
        target.UpgradeTower();
        BuildManager.instance.DeselectNode();
    }

    public void Sell ()
    {
        target.SellTower();
        BuildManager.instance.DeselectNode();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerStats : MonoBehaviour
{
    public GameObject ui;
    public GameObject shop;
    public TextMeshProUGUI towerName;
    public Image towerIcon;
    public TextMeshProUGUI lvl;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI range;
    private Node target;

    public void SetTarget (Node _target)
    {
        target = _target;

        towerName.text = target.towerBlueprint.name;
        towerIcon.sprite = target.towerBlueprint.icon;

        switch(target.towerLevel)
        {
            case 1:
                lvl.text = "Level 1";
                damage.text = "Damage: " + target.towerBlueprint.damage;
                speed.text = "Speed: " + target.towerBlueprint.speed;
                range.text = "Range: " + target.towerBlueprint.range;
                break;
            case 2:
                lvl.text = "Level 2";
                damage.text = "Damage: " + target.towerBlueprint.lvl2damage;
                speed.text = "Speed: " + target.towerBlueprint.lvl2speed;
                range.text = "Range: " + target.towerBlueprint.lvl2range;
                break;
            case 3:
                lvl.text = "Level 3";
                damage.text = "Damage: " + target.towerBlueprint.lvl3damage;
                speed.text = "Speed: " + target.towerBlueprint.lvl3speed;
                range.text = "Range: " + target.towerBlueprint.lvl3range;
                break;
            case 4:
                lvl.text = "Level 4";
                damage.text = "Damage: " + target.towerBlueprint.lvl4damage;
                speed.text = "Speed: " + target.towerBlueprint.lvl4speed;
                range.text = "Range: " + target.towerBlueprint.lvl4range;
                break;                                
        }
        shop.SetActive(false);
        ui.SetActive(true);
    }

    public void Hide ()
    {
        ui.SetActive(false);
        shop.SetActive(true);
    }
}

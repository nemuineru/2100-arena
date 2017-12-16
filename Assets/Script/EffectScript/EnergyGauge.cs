using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGauge : MonoBehaviour {
    ActorBehavior actor;
    ShooterBehavior Shooter;
    UnityEngine.UI.Image GaugeBase, GaugeExtra;
    UnityEngine.UI.Text text;
	// Use this for initialization
	void Start () {
        text = GetComponent<UnityEngine.UI.Text>();

        actor = GameObject.FindGameObjectWithTag("Player").GetComponent<ActorBehavior>();

        GaugeBase = GameObject.Find("EnergyGauge_1").GetComponent<UnityEngine.UI.Image>();
        GaugeExtra = GameObject.Find("EnergyGauge_2").GetComponent<UnityEngine.UI.Image>();
    }

    int MAX;
    int E_CURRENT_INT;
    int E_CURRENT_FLOAT;
    int Percentage;

    // Update is called once per frame
    void Update() {
        Shooter = GameObject.FindGameObjectWithTag("Player")
        .GetComponent<PlayerStatus>().Primary.GetComponent<ShooterBehavior>();

        MAX = Mathf.CeilToInt(actor.MaxEnergy);
        E_CURRENT_INT = Mathf.FloorToInt(actor.Energy);
        E_CURRENT_FLOAT = Mathf.FloorToInt((actor.Energy - E_CURRENT_INT) * 10);
        Percentage = Mathf.CeilToInt((actor.Energy / actor.MaxEnergy) * 1000);

        text.text = Shooter.Name + "\n" + E_CURRENT_INT + "." + E_CURRENT_FLOAT
            + "/" + MAX + "\n" + "En:"  + Mathf.CeilToInt(Percentage / 10) + "."
            + Mathf.CeilToInt(((actor.Energy / actor.MaxEnergy) * 1000 - Percentage) / 100) + "%";

        if (Percentage > 1000)
        {
            GaugeBase.fillAmount = 1;
            GaugeExtra.fillAmount = (actor.Energy / actor.MaxEnergy) - 1f;
        }
        else {
            GaugeBase.fillAmount = (actor.Energy / actor.MaxEnergy);
            GaugeExtra.fillAmount = 0f;
        }
    }
}

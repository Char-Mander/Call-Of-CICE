using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private float currentBarValue;
    private float maxBarValue;
    private float maxObjectHealth;
    private float currentObjectHealth;

    // Start is called before the first frame update
    void Start()
    {
        maxObjectHealth = GetComponentInParent<Health>().GetMaxHealth();
        currentObjectHealth = GetComponentInParent<Health>().GetCurrentHealth();
        maxBarValue = transform.localScale.x;
        UpdateHealthBar();
    }

    // Update is called once per frame
    public void UpdateHealthBar()
    {
        currentObjectHealth = GetComponentInParent<Health>().GetCurrentHealth();
        currentBarValue = (maxBarValue * currentObjectHealth) / maxObjectHealth;
        transform.localScale = new Vector3(currentBarValue, transform.localScale.y, transform.localScale.z);
        BarColorByHealth();
    }

    void BarColorByHealth()
    {
        float percentHealthValue = currentBarValue * 100/ maxBarValue;
        if(percentHealthValue < 33)
        {
            GetComponentInChildren<Renderer>().material.color = Color.red;
        }
        else if(percentHealthValue < 66)
        {
            GetComponentInChildren<Renderer>().material.color = Color.yellow;
        }
        else
        {
            GetComponentInChildren<Renderer>().material.color = Color.green;
        }
    }
}

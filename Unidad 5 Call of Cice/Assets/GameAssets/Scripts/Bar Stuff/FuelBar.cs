using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBar : MonoBehaviour
{
    private float maxObjectFuel;
    private float currentObjectFuel;
    private float maxBarFuel;
    private float currentBarFuel;
    [SerializeField]
    private Color red, orange, yellow, blue;

    void Start()
    {
        maxObjectFuel = GetComponentInParent<Fuel>().GetMaxFuel();
        currentObjectFuel = GetComponentInParent<Fuel>().GetCurrentFuel();
        maxBarFuel = transform.localScale.x;
        GetComponentInChildren<Renderer>().material.color = red;
    }

    // Update is called once per frame
    public void UpdateFuelBar()
    {
        currentObjectFuel = GetComponentInParent<Fuel>().GetCurrentFuel() ;
        currentBarFuel = (maxBarFuel * currentObjectFuel) / maxObjectFuel;
        transform.localScale = new Vector3(currentBarFuel, transform.localScale.y, transform.localScale.z);
        BarColorByAmount();
    }

    void BarColorByAmount()
    {
        float percentFuelValue = currentBarFuel * 100 / maxBarFuel;
         if (percentFuelValue < 10)
        {
            GetComponentInChildren<Renderer>().material.color = blue;
        }
        else if(percentFuelValue < 33)
        {
            GetComponentInChildren<Renderer>().material.color = yellow;
        }
        else if (percentFuelValue < 66)
        {
            GetComponentInChildren<Renderer>().material.color = orange;
        }
        else
        {
            GetComponentInChildren<Renderer>().material.color = red;
        }
    }
}

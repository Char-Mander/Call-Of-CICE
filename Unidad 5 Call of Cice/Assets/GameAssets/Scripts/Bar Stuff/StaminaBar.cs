using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar : MonoBehaviour
{
    private float maxObjectStamina;
    private float currentObjectStamina;
    private float maxBarStamina;
    private float currentBarStamina;
    [SerializeField]
    private Color color;

    void Start()
    {
        maxObjectStamina = GetComponentInParent<Stamina>().GetMaxStamina();
        currentObjectStamina = GetComponentInParent<Stamina>().GetCurrentStamina();
        maxBarStamina = transform.localScale.x;
        GetComponentInChildren<Renderer>().material.color = color;
    }

    // Update is called once per frame
    public void UpdateStaminaBar()
    {
        currentObjectStamina = GetComponentInParent<Stamina>().GetCurrentStamina();
        currentBarStamina = (maxBarStamina * (float)currentObjectStamina) / (float)maxObjectStamina;
        transform.localScale = new Vector3(currentBarStamina, transform.localScale.y, transform.localScale.z);
    }
}

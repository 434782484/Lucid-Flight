using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    private static Image chargeBarImage;
    public Text text;

    /// <summary>
    /// Initialize the variable
    /// </summary>
    private void Start()
    {
        chargeBarImage = GetComponent<Image>();
    }


    void Update()
    {
        if (chargeBarImage.fillAmount >= 0.98f)
        {
            //SetChargeBarColor(Color.red);
            //text.text = "Ready";
        }
        else if (chargeBarImage.fillAmount > 0.5f)
        {
            //SetChargeBarColor(Color.yellow);
            //text.text = "Charging";
        }
        else if (chargeBarImage.fillAmount > 0.0f)
        {
            //SetChargeBarColor(Color.green);
            //text.text = "Charging";
        }
        else {
            //SetChargeBarColor(Color.white);
            //text.text = "";
        }
    }
    // Start is called before the first frame update
    public static void SetChargeBarValue(float value)
    {
        if(chargeBarImage != null)
        chargeBarImage.fillAmount = value;  
    }

    public static float GetChargeBarValue()
    {
        return chargeBarImage.fillAmount;
    }

    /// <summary>
    /// Sets the health bar color
    /// </summary>
    /// <param name="healthColor">Color </param>
    public static void SetChargeBarColor(Color healthColor)
    {
        chargeBarImage.color = healthColor;
    }


}

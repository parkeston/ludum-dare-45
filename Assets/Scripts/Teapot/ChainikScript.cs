using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChainikScript : MonoBehaviour
{
    public Slider chainikEnergy;
    public bool chainikStatic=true;
    //public Slider numberWater;
    public int minRangeEnergy = 60, maxRangeEnergy = 80;
    public int waterValue = 500;
    public GameObject BackGround;
    public GameObject particleLightSmoke;
    public GameObject water;
    private float minusWater;
    private Animator shakeAnimator;
    public GameObject minGreen, maxRed;
    [SerializeField] float energyIncrease = 1f;

    // Start is called before the first frame update
    void Start()
    {
        shakeAnimator=GetComponent<Animator>();
        BackGround.SetActive(false);
        chainikEnergy.value = 0;
        minusWater = 1 / (float)waterValue;
        minGreen.transform.localPosition = new Vector3((((160 * minRangeEnergy)/100)-90), 0, 0);
        maxRed.transform.localPosition = new Vector3((((160 * maxRangeEnergy)/100)-90), 0, 0);
     }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount>0 && water)
        {
            chainikEnergy.value += energyIncrease;
        }

        if (Input.touchCount<=0 && chainikEnergy.value != 0)
        {
            chainikEnergy.value -=energyIncrease;
        }

        if (chainikEnergy.value == minRangeEnergy)
        {
            chainikStatic = false;
        }

        if (chainikEnergy.value > maxRangeEnergy)
        {
            Destroy(GameObject.Find("LightSmoke"));
            Destroy(this.gameObject);
			LevelsManager.Instance.RestartLevel(1);
        }

        if (water && water.transform.localScale.z <= 0)
        {
            Destroy(water);
			LevelsManager.Instance.ToNextLevel(2);
        }

        if(chainikEnergy.value > minRangeEnergy && chainikEnergy.value < maxRangeEnergy)
        {
            if (!GameObject.Find("LightSmoke"))
            {
                GameObject objParticle = Instantiate(particleLightSmoke) as GameObject;
                objParticle.name = "LightSmoke";

                shakeAnimator.SetBool("ShakeBool", true);
            }
            if (water)
            {
                water.transform.localScale = new Vector3(water.transform.localScale.x, water.transform.localScale.y, water.transform.localScale.z - minusWater);
            }
        }
        else if (chainikEnergy.value < minRangeEnergy)
        {
            if (GameObject.Find("LightSmoke"))
            {
                shakeAnimator.SetBool("ShakeBool", false);
                Destroy(GameObject.Find("LightSmoke"));
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    public Transform player;
    public Transform tr;
    public Slider slider;
    public Slider sliderBack;
    public float maxHP;
    private float currentHP;
    float unitHP = 200;
    public HorizontalLayoutGroup hpLineFoler;
    public Vector3 correctPos;
    

    float lastMaxHP = 0;
    bool backHPSlideAni = false;
    private void Start()
    {
        currentHP = lastMaxHP = maxHP;
        backHPSlideAni = false;
        sliderBack.value =  slider.value = 1;

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 vec = player.position + correctPos;
        tr.position = vec;

        updateHpLine = lastMaxHP != maxHP;

        // 자연스럽게 감소
        slider.value = Mathf.Lerp(slider.value, currentHP / maxHP, Time.deltaTime * 5f);

        if (backHPSlideAni)
        {
            sliderBack.value = Mathf.Lerp(sliderBack.value, slider.value, Time.deltaTime * 5f);
            if (slider.value >= sliderBack.value - 0.01f)
            {
                backHPSlideAni = false;
                sliderBack.value = slider.value;
            }
                
        }
            
    }


    bool updateHpLine = false;
    private void LateUpdate()
    {
        if (updateHpLine)
        {
            UpdateHpLine();
            lastMaxHP = maxHP;
        }
    }

    public void UpdateHpLine()
    {
        Vector3 vec = new Vector3((1000f / unitHP) / (maxHP / unitHP), 1, 1);
        hpLineFoler.enabled = false;
        foreach (Transform child in hpLineFoler.transform)
        {
            child.gameObject.transform.localScale = vec;
        }
        hpLineFoler.enabled = true;
    }

    public void OnDmg(int dmg)
    {
        currentHP -= dmg;
        Invoke("BackHPFun", 0.5f);
    }

    public void BackHPFun()
    {
        backHPSlideAni = true;
    }
}

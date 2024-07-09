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
    public float maxHP;
    public float currentHP;
    float unitHP = 200;
    public HorizontalLayoutGroup hpLineFoler;

    float lastMaxHP = 0;

    private void Start()
    {
        lastMaxHP = maxHP;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 vec = player.position;
        vec.y = 0;
        tr.position = vec;

        slider.value = currentHP / maxHP;
        updateHpLine = lastMaxHP != maxHP;
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
}

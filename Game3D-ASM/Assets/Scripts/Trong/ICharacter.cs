using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ICharacter
{
    float HP { get; set; }
    float MP { get; set; }
    float stamina { get; set; }
    float moveSpeed { get; set; }
    float strength { get; set; }
    float armor {  get; set; }
    float poise { get; set; }
    float critChance {  get; set; }
    float critPower { get; set; }
    float iFrameTime {  get; set; }
    bool isImmune { get; set; }
    Transform popupTextTransform { get; set; }
    Slider healthSlider { get; set; }
    float maxHP { get; set; }
    public void TakeDamage(float damage, bool isCrit);
    public void DealDamage(GameObject target, float power, bool isCrit);
    public bool CheckCritChance(float critChance);
    public void ResetIFrame();
    public void DisplayDamageTaken(float damage, bool isCrit);
    public void UpdateHealthbar();

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    public TextMeshProUGUI starText;
    public TextMeshProUGUI jumpText;
    public TextMeshProUGUI dmgText;
    public Text jumpButtonText;
    public Text dmgButtonText;
    public AudioSource audioSource;

    public static int upgradeStarJump = 5;
    public static int upgradeStarDmg = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        starText.text = "X " + LevelCompleteUI.Starscount;
        jumpText.text = "Jump Force " + Weapon.recoilForce;
        dmgText.text = "Bullet Damage " + Projectile.damage;
        jumpButtonText.text = "Upgrade " + upgradeStarJump + "   ";
        dmgButtonText.text = "Upgrade " + upgradeStarDmg + "   ";
    }

    public void WeaponDamage()
    {
        if (LevelCompleteUI.Starscount >= 5)
        {
            Projectile.damage *= 2;
            LevelCompleteUI.Starscount -= 5;
            upgradeStarDmg += 2;
            audioSource.Play();
        }
    }
    public void GunRecoil()
    {
        if (LevelCompleteUI.Starscount >= 5)
        {
            Weapon.recoilForce += 2;
            LevelCompleteUI.Starscount -= 5;
            upgradeStarJump += 2;
            audioSource.Play();
        }
    }
}

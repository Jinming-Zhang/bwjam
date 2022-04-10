using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfUISystem;
using UnityEngine.UI;

namespace WolfUISystem.Presets
{
    public class HudScreen : ScreenBase
    {
        [Header("Player Health")]
        [SerializeField]
        GameObject healthUITemplate;
        [SerializeField]
        GameObject healthUIRoot;
        [Header("Clue")]
        [SerializeField]
        TMPro.TextMeshProUGUI clueStatusTmp;
        [SerializeField]
        Image clueSlider;
        [Header("Ammo")]
        [SerializeField]
        TMPro.TextMeshProUGUI ammoStatusTmp;
        [Header("Reload")]
        [SerializeField]
        Image reloadImage;

        public override void Initialize()
        {
        }
        public void UpdatePlayerHealth(int newHealth)
        {
            int i = 0;
            for (; i < newHealth; i++)
            {
                if (i < healthUIRoot.transform.childCount)
                {
                    healthUIRoot.transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    Instantiate(healthUITemplate, healthUIRoot.transform);
                }
            }
            for (; i < healthUIRoot.transform.childCount; i++)
            {
                healthUIRoot.transform.GetChild(i).gameObject.SetActive(false);
            }

        }
        public void UpdateClumeter(int current, int max)
        {
            clueStatusTmp.text = $"{current}/{max}";
            clueSlider.fillAmount = (float)current / max;
        }
        public void UpdateAmmo(int current, int max)
        {
            ammoStatusTmp.text = $"{current}/{max}";
        }
        public void UpdateReload(float current, float max)
        {
            reloadImage.fillAmount = current / max;
            if (!reloadImage.gameObject.activeSelf)
            {
                reloadImage.gameObject.SetActive(true);
            }
        }
        public void ReloadDone()
        {
            reloadImage.gameObject.SetActive(false);
        }
        public void OnHelpButtonClicked()
        {
            UIManager.Instance.PushScreen<SettingScreen>();
        }
    }
}
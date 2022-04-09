using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfUISystem;
using WolfUISystem.Presets;

namespace GamePlay.Weapons
{
    [CreateAssetMenu(menuName = "Weapon/Player Gun", fileName = "Player Gun")]
    public class PlayerGun : Gun
    {
        HudScreen hud;
        HudScreen Hud
        {
            get
            {
                if (!hud)
                {
                    hud = UIManager.Instance.GetScreenComponent<HudScreen>();
                }
                return hud;
            }
        }
        public override void Fire(Transform pos, Vector2 direction)
        {
            base.Fire(pos, direction);
            Hud.UpdateAmmo(CurrentAmmo, ClipSize);
        }

        public override void Reload()
        {
            base.Reload();
            GameCore.GameManager.Instance.StartCoroutine(UpdateHudCR());
            Hud.UpdateReload(0, reloadTime);
            IEnumerator UpdateHudCR()
            {
                float timer = 0;
                while (timer <= reloadTime)
                {
                    timer += Time.deltaTime;
                    Hud.UpdateReload(timer, reloadTime);
                    yield return new WaitForEndOfFrame();
                }
                Hud.ReloadDone();
            }
        }
    }
}

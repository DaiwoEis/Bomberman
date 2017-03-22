using UnityEngine;
using UnityEngine.Audio;

namespace CUI
{
    public class SoundSettingView : AnimateView 
    {
        [SerializeField]
        private AudioMixer _masterMixer = null;

        public void MusicSliderCallBack(float value)
        {
            _masterMixer.SetFloat("musicVol", value);
        }

        public void SFXSliderCallBack(float value)
        {
            _masterMixer.SetFloat("sfxVol", value);
        }

        public void ToggleCallBack(bool toggled)
        {
            _masterMixer.SetFloat("masterVol", toggled ? -80f : 0f);
        }

        public void BackCallBack()
        {
            Singleton<ViewManager>.instance.AddCommond(new CloseCommond());
        }

        public override void OnUpdate(UIType uiType)
        {
            base.OnUpdate(uiType);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Singleton<ViewManager>.instance.AddCommond(new CloseCommond());
            }
        }
    }
}
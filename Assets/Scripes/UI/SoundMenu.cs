using UnityEngine;
using UnityEngine.UI;

public class SoundMenu : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
       if(!PlayerPrefs.HasKey("musicVolume")) {
        PlayerPrefs.SetFloat("musicVolume", 1);
        load();
       }
       else
       {
        load();
       }
       if (volumeSlider == null)
       {
        Debug.LogWarning("Volume slider reference is missing in soundMenu script.");
        return;
       }
       // ensure runtime changes on the UI slider apply immediately
       if (volumeSlider != null)
           volumeSlider.onValueChanged.AddListener((v) => changeVolume());
    }

    // Update is called once per frame
   public void changeVolume(){
    if (volumeSlider == null) return;
    AudioListener.volume = volumeSlider.value;
    save();
   }
   private void load(){
    if (volumeSlider == null) return;
    float v = PlayerPrefs.GetFloat("musicVolume");
    volumeSlider.value = v;
    AudioListener.volume = v;
   }
   private void save(){
    if (volumeSlider == null) return;
    PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    PlayerPrefs.Save();
   }

}


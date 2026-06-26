using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class PixelOptions : MonoBehaviour
{
    public Material ShaderMaterial;
     [SerializeField] TMP_Dropdown pixelCountDropdown;
     public int pixelCount = 2048;
     int currentPixelCountIndex = 2; // Default to 1024
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (!PlayerPrefs.HasKey("pixelCount"))
        {
            PlayerPrefs.SetInt("pixelCount", currentPixelCountIndex);
            load();
        }
        else
        {
            load();
        }
        if (pixelCountDropdown != null)
        {
            pixelCountDropdown.onValueChanged.AddListener(OnPixelCountChanged);
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    void OnPixelCountChanged(int index)
    {
        if (ShaderMaterial == null) return;

        float pixelCount = 2048f; // Default value

        switch (index)
        {
            case 0:
                pixelCount = 256f;
                break;
            case 1:
                pixelCount = 512f;
                break;
            case 2:
                pixelCount = 1024f;
                break;
            case 3:
                pixelCount = 2048f;
                break;
            case 4:
                pixelCount = 4096f;
                break;
        }

        ShaderMaterial.SetFloat("_pixelCount", pixelCount);
    }
    private void load(){
    if (pixelCountDropdown == null) return;
    float v = PlayerPrefs.GetFloat("pixelCount");
    pixelCountDropdown.value = (int)v;
   }
   public void save(){
    if (pixelCountDropdown == null) return;
    PlayerPrefs.SetFloat("pixelCount", pixelCountDropdown.value);
    PlayerPrefs.Save();
   }
}

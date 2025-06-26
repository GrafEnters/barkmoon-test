using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BreedsView : MonoBehaviour
{
    public Button[] BreedButtons;
    public TextMeshProUGUI[] BreedNames;
    public LoaderView Loader;
    public System.Action<int> OnBreedClick;

    private void Awake()
    {
        for (int i = 0; i < BreedButtons.Length; i++)
        {
            int idx = i;
            BreedButtons[i].onClick.AddListener(() => OnBreedClick?.Invoke(idx));
        }
    }

    public void ShowLoader(bool show)
    {
        if (Loader != null)
            if (show) Loader.Show(); else Loader.Hide();
    }

    public void UpdateBreeds(string[] names)
    {
        for (int i = 0; i < BreedNames.Length; i++)
        {
            if (i < names.Length)
            {
                BreedNames[i].text = names[i];
                BreedButtons[i].gameObject.SetActive(true);
            }
            else
            {
                BreedNames[i].text = "";
                BreedButtons[i].gameObject.SetActive(false);
            }
        }
    }
} 
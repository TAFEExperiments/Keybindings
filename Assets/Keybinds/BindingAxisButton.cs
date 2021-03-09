using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Keybinds
{
    public class BindingAxisButton : MonoBehaviour
    {
        [SerializeField]
        private string bindingToMap;
        [SerializeField]
        private Button button;
        [SerializeField]
        private TextMeshProUGUI buttonText;
        [SerializeField]
        private TextMeshProUGUI mappingName;

        private bool isRemapping = false;
        private bool remapPositive = true;
        private KeyCode positive;
        private KeyCode negative;

        public void Setup(string _toMap)
        {
            bindingToMap = _toMap;

            button.onClick.AddListener(OnClick);
            mappingName.text = _toMap;
            BindingUtils.UpdateTextWithBindingAxis(bindingToMap, buttonText);
            gameObject.SetActive(true);
        }

        // Start is called before the first frame update
        void Start()
        {
            if (string.IsNullOrEmpty(bindingToMap))
            {
                Debug.LogError($"Binding on {name} was not set, disabling button.");
                gameObject.SetActive(false);
            }

            Setup(bindingToMap);
        }

        // Update is called once per frame
        void Update()
        {
            if (isRemapping)
            {
                if (remapPositive)
                {
                    positive = BindingUtils.GetAnyPressedKey();
                    if (positive != KeyCode.None)
                    {
                        BindingUtils.UpdatePositiveAxisText(bindingToMap, positive, buttonText);
                        remapPositive = false;
                    }
                }
                else
                {
                    negative = BindingUtils.GetAnyPressedKey();

                    if (negative != KeyCode.None)
                    {
                        BindingManager.RebindAxis(bindingToMap, positive, negative);
                        BindingUtils.UpdateTextWithBindingAxis(bindingToMap, buttonText);

                        isRemapping = false;
                        remapPositive = true;
                        BindingManager.IsRemapping = false;
                        positive = KeyCode.None;
                        negative = KeyCode.None;
                    }
                }
            }
        }

        private void OnClick()
        {
            isRemapping = true;
            BindingManager.IsRemapping = true;
        }
    }
}
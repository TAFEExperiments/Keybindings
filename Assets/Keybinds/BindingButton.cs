using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Keybinds
{
    public class BindingButton : MonoBehaviour
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

        public void Setup(string _toMap)
        {
            bindingToMap = _toMap;

            button.onClick.AddListener(OnClick);
            mappingName.text = _toMap;
            BindingUtils.UpdateTextWithBinding(bindingToMap, buttonText);
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
                KeyCode pressed = BindingUtils.GetAnyPressedKey();
                if (pressed != KeyCode.None)
                {
                    BindingManager.Rebind(bindingToMap, pressed);
                    BindingUtils.UpdateTextWithBinding(bindingToMap, buttonText);

                    BindingManager.IsRemapping = false;
                    isRemapping = false;
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
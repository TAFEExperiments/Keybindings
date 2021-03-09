using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Keybinds;

public class BindingButtonSpawner : MonoBehaviour
{
    [SerializeField]
    private BindingButton bindingButton;
    [SerializeField]
    private BindingAxisButton axisButton;
    [SerializeField]
    private Transform holder;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1.5f);

        List<Binding> bindings = BindingManager.GetBindings();

        foreach(Binding binding in bindings)
        {
            BindingButton newButton = Instantiate(bindingButton, holder);
            newButton.Setup(binding.Name);
        }

        List<BindingAxis> axes = BindingManager.GetBindingAxes();

        foreach (BindingAxis axis in axes)
        {
            BindingAxisButton newButton = Instantiate(axisButton, holder);
            newButton.Setup(axis.Name);
        }
    }
}

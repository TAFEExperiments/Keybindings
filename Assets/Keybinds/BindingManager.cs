using UnityEngine;

using System.Collections.Generic;

namespace Keybinds
{
    public class BindingManager : MonoBehaviour
    {
        public static bool IsRemapping { get; set; }

        private Dictionary<string, Binding> bindings = new Dictionary<string, Binding>();
        private Dictionary<string, BindingAxis> axes = new Dictionary<string, BindingAxis>();
        private List<Binding> bindingsList = new List<Binding>();
        private List<BindingAxis> axisList = new List<BindingAxis>();

        [SerializeField, UnityEngine.Serialization.FormerlySerializedAs("defaults")]
        private List<Binding> defaultBindings = new List<Binding>();
        [SerializeField]
        private List<BindingAxis> defaultAxis = new List<BindingAxis>();

        private static BindingManager instance = null;

        public static float BindingAxis(string _name)
        {
            BindingAxis axis = GetBindingAxis(_name);
            if(axis != null)
            {
                return axis.Axis();
            }

            return 0;
        }

        public static bool BindingPressed(string _name)
        {
            Binding binding = GetBinding(_name);

            if (binding != null)
            {
                return binding.Pressed();
            }

            return false;
        }

        public static bool BindingHeld(string _name)
        {
            Binding binding = GetBinding(_name);

            if (binding != null)
            {
                return binding.Held();
            }

            return false;
        }

        public static bool BindingReleased(string _name)
        {
            Binding binding = GetBinding(_name);

            if (binding != null)
            {
                return binding.Released();
            }

            return false;
        }

        public static void Rebind(string _name, KeyCode _value)
        {
            Binding binding = GetBinding(_name);

            if (binding != null)
            {
                binding.Rebind(_value);
            }
        }

        public static void RebindAxis(string _name, KeyCode _positive, KeyCode _negative)
        {
            BindingAxis axis = GetBindingAxis(_name);

            if (axis != null)
            {
                axis.Rebind(_positive, _negative);
            }
        }

        public static List<Binding> GetBindings()
        {
            return instance.bindingsList;
        }

        public static List<BindingAxis> GetBindingAxes()
        {
            return instance.axisList;
        }

        public static Binding GetBinding(string _name)
        {
            if (instance.bindings.ContainsKey(_name))
            {
                return instance.bindings[_name];
            }

            return null;
        }

        public static BindingAxis GetBindingAxis(string _name)
        {
            if (instance.axes.ContainsKey(_name))
            {
                return instance.axes[_name];
            }

            return null;
        }

        // Start is called before the first frame update
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }

            PopulateBindingDictionaries();
            LoadBindings();
        }

        void OnDestroy()
        {
            SaveBindings();
        }

        private void PopulateBindingDictionaries()
        {
            foreach (Binding binding in defaultBindings)
            {
                if (bindings.ContainsKey(binding.Name))
                    continue;

                bindings.Add(binding.Name, binding);
                bindingsList.Add(binding);
            }

            foreach (BindingAxis axis in defaultAxis)
            {
                if (axes.ContainsKey(axis.Name))
                    continue;

                axes.Add(axis.Name, axis);
                axisList.Add(axis);
            }
        }

        private void LoadBindings()
        {
            foreach (Binding binding in bindingsList)
            {
                binding.Load();
            }

            foreach (BindingAxis axis in axisList)
            {
                axis.Load();
            }
        }

        private void SaveBindings()
        {
            foreach (Binding binding in bindingsList)
            {
                binding.Save();
            }

            foreach (BindingAxis axis in axisList)
            {
                axis.Save();
            }
        }
    }
}
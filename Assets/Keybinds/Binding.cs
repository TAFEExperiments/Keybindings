using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Keybinds
{
    [Serializable]
    public class Binding
    {
        public string Name { get { return name; } }
        public KeyCode Value { get { return value; } }
        public string ValueDisplay { get { return BindingUtils.TranslateKeycode(value); } }

        [SerializeField]
        private string name;
        [SerializeField]
        private KeyCode value;

        public Binding(string _name, KeyCode _default)
        {
            name = _name;
            value = _default;
        }

        public void Load()
        {
            value = (KeyCode)PlayerPrefs.GetInt(name, value == KeyCode.None ? (int)KeyCode.Space : (int)value);
        }

        public void Save()
        {
            PlayerPrefs.SetInt(name, (int)value);
            PlayerPrefs.Save();
        }

        public void Rebind(KeyCode _new)
        {
            value = _new;
            Save();
        }

        public bool Pressed()
        {
            return Input.GetKeyDown(value);
        }

        public bool Held()
        {
            return Input.GetKey(value);
        }

        public bool Released()
        {
            return Input.GetKeyUp(value);
        }
    }
}
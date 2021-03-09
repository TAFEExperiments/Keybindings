using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Keybinds;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;

    // Update is called once per frame
    void Update()
    {
        if (BindingManager.IsRemapping)
            return;

        if(BindingManager.BindingHeld("Forward"))
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }

        if(BindingManager.BindingHeld("Right"))
        {
            transform.position += transform.right * Time.deltaTime * speed;
        }

        if(BindingManager.BindingHeld("Backward"))
        {
            transform.position -= transform.forward * Time.deltaTime * speed;
        }

        if(BindingManager.BindingHeld("Left"))
        {
            transform.position -= transform.right * Time.deltaTime * speed;
        }

        transform.position += transform.forward * Time.deltaTime * speed * BindingManager.BindingAxis("Vertical");
        transform.position += transform.right * Time.deltaTime * speed * BindingManager.BindingAxis("Horizontal");
    }
}

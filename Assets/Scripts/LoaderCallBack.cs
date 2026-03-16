using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallBack : MonoBehaviour
{
    private bool isFisrtUpdate = true; 
    private void Update()
    {
        if (isFisrtUpdate)
        {
            isFisrtUpdate = false;
            Loader.LoaderCallBack();
        }
    }
}

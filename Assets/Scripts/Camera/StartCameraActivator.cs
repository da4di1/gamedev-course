using System;
using System.Collections;
using System.Collections.Generic;
using Core.Tools;
using Player;
using UnityEngine;

namespace Camera
{
    public class StartCameraActivator : MonoBehaviour
    {
        [SerializeField] private Cameras _cameras;


        private void OnTriggerEnter2D(Collider2D other)
        {
            _cameras.StartCamera.enabled = true;
            foreach (var cameraPair in _cameras.DirectionalCameras)
                cameraPair.Value.enabled = false;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _cameras.StartCamera.enabled = false;
            other.GetComponent<PlayerHandler>().FlipCameras();
        }
    }
}
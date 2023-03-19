using Core.Tools;
using Player;
using UnityEngine;

namespace Camera
{
    public class FinalCameraActivator : MonoBehaviour
    {
        [SerializeField] private Cameras _cameras;


        private void OnTriggerEnter2D(Collider2D other)
        {
            _cameras.FinalCamera.enabled = true;
            foreach (var cameraPair in _cameras.DirectionalCameras)
                cameraPair.Value.enabled = false;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _cameras.FinalCamera.enabled = false;
            other.GetComponent<PlayerHandler>().FlipCameras();
        }
    }
}
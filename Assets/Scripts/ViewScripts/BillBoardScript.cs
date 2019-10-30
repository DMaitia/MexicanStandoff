using UnityEngine;

namespace ViewScripts
{
    public class BillBoardScript : MonoBehaviour
    {
        private GameObject _mainCamera;
    
        // Start is called before the first frame update
        void Start()
        {
            if (Camera.main != null) _mainCamera = Camera.main.gameObject;
            gameObject.transform.LookAt(_mainCamera.transform.position);
        }
    }
}

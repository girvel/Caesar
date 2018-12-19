using UnityEngine;

namespace Code.Systems.Camera
{
    public class CameraController : MonoBehaviour
    {
        public float ScrollingFactor = 0.005f;
        public float ScalingFactor = 1.2f;

        public float ScrollingSpeedMax = 3f;

        public float ScaleMax = 12;
        public float ScaleMin = 2;

        public float CameraMovingSpeed = 3f;

        public static Vector3 TargetPosition { get; set; }

        protected Vector3 PreviousMousePosition;
        protected UnityEngine.Camera ThisCamera;

        protected void Start()
        {
            TargetPosition = transform.position;

            ThisCamera = GetComponent<UnityEngine.Camera>();
        }

        protected virtual void Update()
        {
            CameraControlUpdate();
            CameraMoving();
        }

        private void CameraMoving()
        {
            if (TargetPosition == transform.position)
                return;

            var path = TargetPosition - transform.position;
            if (path.magnitude <= CameraMovingSpeed * Time.deltaTime * ThisCamera.orthographicSize)
            {
                transform.position = TargetPosition;
            }
        }

        private void CameraControlUpdate()
        {
            if (UnityEngine.Input.GetMouseButton(2) || UnityEngine.Input.GetMouseButton(1))
            {
                var path = PreviousMousePosition - UnityEngine.Input.mousePosition;

                transform.position +=
                    path.magnitude * ScrollingFactor
                    * ThisCamera.orthographicSize > ScrollingSpeedMax
                        ? ScrollingSpeedMax * path.normalized
                        : path * ScrollingFactor * ThisCamera.orthographicSize;
                
                TargetPosition = transform.position;
            }

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (UnityEngine.Input.mouseScrollDelta.y != 0)
            {
                ThisCamera.orthographicSize
                    = Mathf.Clamp(
                        ThisCamera.orthographicSize 
                        * (UnityEngine.Input.mouseScrollDelta.y < 0 ? ScalingFactor : 1 / ScalingFactor),
                        ScaleMin,
                        ScaleMax);
            }

            PreviousMousePosition = UnityEngine.Input.mousePosition;
        }
    }
}
using Unity.Cloud.UserReporting.Plugin;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// Represents a behavior that collects XR information for user reports.
/// </summary>
/// <remarks>If you're using an older version of Unity and don't need XR support, feel free to delete this script.</remarks>
public class UserReportingXRExtensions : MonoBehaviour
{
    #region Methods

    private void Start()
    {
        if (XRSettings.isDeviceActive)
        {
           // UnityUserReporting.CurrentClient.AddDeviceMetadata("XRDeviceModel", XRDevice.);
        }
    }

    private void Update()
    {
        if (XRSettings.isDeviceActive)
        {
            int droppedFrameCount;
            if (XRStats.TryGetDroppedFrameCount(out droppedFrameCount))
            {
                UnityUserReporting.CurrentClient.SampleMetric("XR.DroppedFrameCount", droppedFrameCount);
            }

            int framePresentCount;
            if (XRStats.TryGetFramePresentCount(out framePresentCount))
            {
                UnityUserReporting.CurrentClient.SampleMetric("XR.FramePresentCount", framePresentCount);
            }

            float gpuTimeLastFrame;
            if (XRStats.TryGetGPUTimeLastFrame(out gpuTimeLastFrame))
            {
                UnityUserReporting.CurrentClient.SampleMetric("XR.GPUTimeLastFrame", gpuTimeLastFrame);
            }
        }
    }

    #endregion
}
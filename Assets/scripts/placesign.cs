using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Google.XR.ARCoreExtensions;
using System.Collections;
using UnityEngine.Android;
using TMPro;
using Unity.XR.CoreUtils;

public class PlaceAtGeoLocation : MonoBehaviour
{
    public ARAnchorManager anchorManager;
    public AREarthManager earthManager;

    public GameObject cubePrefab; // Prefab to instantiate
    public double latitude;
    public double longitude;
    public double altitude;
    public float rotation = 0f;
    public double spawnRadius = 10.0; // Meters
    public bool faceplayer = false;
    public string DisplaySignText;
    bool placed = false;
    ARGeospatialAnchor anchorPlaced;

    void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }

    }

  

    void Update()
    {
        var pose = earthManager.CameraGeospatialPose;

        // Calculate distance between device and target anchor
        double distanceMeters = HaversineDistance(
            pose.Latitude, pose.Longitude,
            latitude, longitude,
            pose.Altitude, altitude
        );


        if (!placed && earthManager.EarthTrackingState == TrackingState.Tracking)
        {
            if (anchorManager == null)
            {
                return;
            }
            var anchor = anchorManager.AddAnchor(latitude, longitude, altitude, Quaternion.identity);

            if (anchor != null)
            {
                // Instantiate prefab at local (0,0,0) so it stays locked to the anchor
                var mysign = Instantiate(cubePrefab, Vector3.zero, Quaternion.identity, anchor.transform);
                mysign.transform.Rotate(0, rotation, 0);
                var text = mysign.GetNamedChild("SignText").GetComponent<TextMeshPro>();
                text.text = DisplaySignText;
                anchorPlaced = anchor;
                placed = true;

            }

        }
        if (placed && anchorPlaced != null)
        {
            //anchorPlaced.gameObject.SetActive(distanceMeters <= spawnRadius);
            if (faceplayer)
            {
                anchorPlaced.gameObject.transform.LookAt(Camera.main.transform);
            }
        }
        

    }

    /// <summary>
    /// Haversine formula for distance between two lat/lon points (with altitude).
    /// </summary>
    double HaversineDistance(double lat1, double lon1, double lat2, double lon2, double alt1, double alt2)
    {
        double R = 6371000; // Earth radius in meters
        double dLat = Mathf.Deg2Rad * (float)(lat2 - lat1);
        double dLon = Mathf.Deg2Rad * (float)(lon2 - lon1);

        double a = Mathf.Sin((float)dLat / 2) * Mathf.Sin((float)dLat / 2) +
                   Mathf.Cos(Mathf.Deg2Rad * (float)lat1) * Mathf.Cos(Mathf.Deg2Rad * (float)lat2) *
                   Mathf.Sin((float)dLon / 2) * Mathf.Sin((float)dLon / 2);

        double c = 2 * Mathf.Atan2(Mathf.Sqrt((float)a), Mathf.Sqrt((float)(1 - a)));

        double horizontalDist = R * c;
        double verticalDist = alt2 - alt1;

        return Mathf.Sqrt((float)(horizontalDist * horizontalDist + verticalDist * verticalDist));
    }
}

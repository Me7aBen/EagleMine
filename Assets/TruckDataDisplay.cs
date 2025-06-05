using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class TruckDataDisplay : MonoBehaviour
{
    [Header("Truck ID (e.g. truck_001)")]
    public string truckId = "truck_001";

    [Header("Main Info Panel")]
    public TextMeshProUGUI brandText;
    public TextMeshProUGUI originText;
    public TextMeshProUGUI timestampText;
    public TextMeshProUGUI loadText;
    public TextMeshProUGUI employeeIdText;

    [Header("Floating Label Above Truck")]
    public TextMeshProUGUI truckNumberText;
    public TextMeshProUGUI operatorNameText;
    public TextMeshProUGUI speedText;

    private DatabaseReference dbRef;

    void Start()
    {
        dbRef = FirebaseDatabase.DefaultInstance.GetReference($"trucks/{truckId}");
        ListenToTruckData();
    }

    private void ListenToTruckData()
    {
        dbRef.ValueChanged += OnTruckDataChanged;
    }

    private void OnTruckDataChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError("Firebase error: " + args.DatabaseError.Message);
            return;
        }

        DataSnapshot snapshot = args.Snapshot;

        // Identification
        string brand = snapshot.Child("identification/brand").Value?.ToString();
        string truckNumber = snapshot.Child("identification/truckNumber").Value?.ToString();

        // Route
        string origin = snapshot.Child("route/origin").Value?.ToString();

        // Load
        bool isLoaded = bool.Parse(snapshot.Child("loadStatus/isLoaded").Value?.ToString() ?? "false");
        string material = snapshot.Child("loadStatus/material").Value?.ToString();
        float tons = float.Parse(snapshot.Child("loadStatus/tons").Value?.ToString() ?? "0");

        // Telemetry
        float speed = float.Parse(snapshot.Child("telemetry/speed_kph").Value?.ToString() ?? "0");

        // Operator
        string operatorName = snapshot.Child("operator/name").Value?.ToString();
        string employeeId = snapshot.Child("operator/employeeId").Value?.ToString();

        // Timestamp
        string timestamp = snapshot.Child("timestamp").Value?.ToString();

        // 🟩 Update Main Panel
        brandText.text = $"{brand}";
        originText.text = $"{origin}";
        timestampText.text = $"{timestamp}";
        loadText.text = isLoaded ? $"{material} - {tons} tons" : "Empty";
        employeeIdText.text = $"{employeeId}";

        // 🟦 Update Floating UI
        truckNumberText.text = truckNumber;
        operatorNameText.text = operatorName;
        speedText.text = $"{speed:0.0}";
    }
}
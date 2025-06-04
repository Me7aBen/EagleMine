using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using System.Collections.Generic;

public class FirebaseTruckManager : MonoBehaviour
{
    private DatabaseReference dbRef;

    void Start()
    {
        dbRef = FirebaseDatabase.DefaultInstance.GetReference("trucks");

        dbRef.ValueChanged += OnTruckDataChanged;
    }

    private void OnTruckDataChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError("Firebase error: " + args.DatabaseError.Message);
            return;
        }

        foreach (var truckSnapshot in args.Snapshot.Children)
        {
            string truckId = truckSnapshot.Key;
            string json = truckSnapshot.GetRawJsonValue();

            TruckData truckData = JsonUtility.FromJson<TruckData>(json);
            Debug.Log($"🚛 {truckData.identification.truckNumber} | {truckData.route.origin} -> {truckData.route.destination} | Speed: {truckData.telemetry.speed_kph} km/h");

            // TODO: actualizar visual o mover el camión en la escena aquí
        }
    }
}
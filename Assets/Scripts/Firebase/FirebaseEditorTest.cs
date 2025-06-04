using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseEditorTest : MonoBehaviour
{
    private DatabaseReference dbRef;

    void Start()
    {
        // Verifica dependencias de Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                // Obtiene una instancia de la base con tu URL
                FirebaseDatabase database = FirebaseDatabase.GetInstance("https://gen-lang-client-0125077051-default-rtdb.firebaseio.com/");
                dbRef = database.RootReference;

                // Escribe un valor simple
                WriteTestData();
            }
            else
            {
                Debug.LogError("❌ Firebase not available: " + task.Result);
            }
        });
    }

    private void WriteTestData()
    {
        dbRef.Child("testNode").SetValueAsync("Hello from Unity Editor")
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    Debug.Log("✅ Data sent successfully.");
                }
                else
                {
                    Debug.LogError("❌ Failed to send data: " + task.Exception);
                }
            });
    }
}
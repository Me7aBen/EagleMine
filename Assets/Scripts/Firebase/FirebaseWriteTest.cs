using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseWriteTest : MonoBehaviour
{
    private DatabaseReference dbRef;
    void Start()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;

        dbRef.Child("testNode").SetValueAsync("Hello from Unity Editor")
            .ContinueWithOnMainThread(task => {
                if (task.IsCompleted)
                    Debug.Log("Data sent successfully.");
                else
                    Debug.LogError("Failed to send data.");
            });
    }
}

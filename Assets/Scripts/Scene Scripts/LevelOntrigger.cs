using UnityEngine;

public class LevelOntrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SceneLoader.instance.NextLevel();
            Debug.Log("Trigger scene change.");
        }
    }
}

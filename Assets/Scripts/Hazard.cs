using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{

        void OnTriggerEnter2D(Collision col)
        {
        if (col.gameObject.tag == "Hazard")
        {
            SceneManager.LoadScene("Monastary");
        }
    }
}

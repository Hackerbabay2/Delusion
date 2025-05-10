using System.Collections;
using Storage.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoostrapEntryPoint : MonoBehaviour
{
    private IEnumerator Start()
    {
        WaitForSeconds wait = new WaitForSeconds(3f);
        yield return wait;
        Debug.Log("Load complete");
        
        SceneManager.LoadScene("SampleScene");
    }
}

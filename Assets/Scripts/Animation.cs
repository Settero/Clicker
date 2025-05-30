using UnityEngine;
using System.Collections;

public class Animation : MonoBehaviour
{
    public Renderer targetRenderer;
    public Material material1;
    public Material material2;
    public float duration = 5f;

    public void Play()
    {
        StartCoroutine(SwitchRoutine());
    }

    private IEnumerator SwitchRoutine()
    {
        if (targetRenderer != null && material2 != null && material1 != null)
        {
            targetRenderer.material = material2;
            yield return new WaitForSeconds(duration);
            targetRenderer.material = material1;
        }
    }
}

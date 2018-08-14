using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignettePulse : MonoBehaviour
{
    public PostProcessVolume m_Volume;
    public Vignette m_Vignette;
    public float Value;

    void Start()
    {
        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        m_Vignette.enabled.Override(true);
        m_Vignette.intensity.Override(1f);
        m_Vignette.color = Color.red;

        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
    }

    void Update()
    {
        m_Vignette.intensity.value = .5f + Mathf.Sin(Time.realtimeSinceStartup)/2;
        Value = m_Vignette.intensity.value;
    }

    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(m_Volume, true, true);
    }
}
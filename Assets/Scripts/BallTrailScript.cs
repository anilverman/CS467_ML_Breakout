using UnityEngine;

/// Creates and manages the visual trail behind a Breakout ball.
/// This component only changes the ball's appearance.

public class BallTrailScript : MonoBehaviour
{
    [Header("Trail Appearance")]
    [Tooltip("How long each part of the trail remains visible.")]
    [SerializeField] private float trailDuration = 0.2f;

    [Tooltip("The width of the trail where it meets the ball.")]
    [SerializeField] private float trailWidth = 0.4f;

    [Tooltip("The fixed color used by the trail.")]
    [SerializeField] private Color trailColor = Color.cyan;

    private TrailRenderer trailRenderer;
    private Material trailMaterial;

    /// Finds the required components and configures the trail.
    /// Awake runs once when the ball is created.
    private void Awake()
    {
        FindOrCreateTrailRenderer();
        ConfigureTrailShape();
        ConfigureTrailColor();
        CreateTrailMaterial();
    }

    /// Uses an existing TrailRenderer when one is available.
    /// Otherwise, it adds a new TrailRenderer to the ball.
    private void FindOrCreateTrailRenderer()
    {
        trailRenderer = GetComponent<TrailRenderer>();

        if (trailRenderer == null)
        {
            trailRenderer = gameObject.AddComponent<TrailRenderer>();
        }
    }

    /// Gives the trail a fixed duration and tapered shape.
    /// The trail starts wide and narrows to a point.
    private void ConfigureTrailShape()
    {
        trailRenderer.time = trailDuration;
        trailRenderer.startWidth = trailWidth;
        trailRenderer.endWidth = 0f;
        trailRenderer.minVertexDistance = 0.1f;
        trailRenderer.numCornerVertices = 4;
        trailRenderer.numCapVertices = 4;
    }

    /// Applies one fixed color that fades to transparent.
    /// The color does not change while the ball is moving.
    private void ConfigureTrailColor()
    {
        trailRenderer.startColor = trailColor;

        Color transparentColor = trailColor;
        transparentColor.a = 0f;

        trailRenderer.endColor = transparentColor;
    }

    /// Creates a material that is compatible with the project's
    /// Universal Render Pipeline, with a standard sprite fallback.
    private void CreateTrailMaterial()
    {
        Shader shader = Shader.Find("Universal Render Pipeline/Particles/Unlit");

        if (shader == null)
        {
            shader = Shader.Find("Sprites/Default");
        }

        if (shader == null)
        {
            Debug.LogWarning("A shader could not be found for the ball trail.", this);
            return;
        }

        trailMaterial = new Material(shader);
        trailMaterial.name = "Ball Trail Material";
        trailRenderer.material = trailMaterial;
    }

    /// Allows the trail to appear as the ball moves.
    public void EnableTrail()
    {
        trailRenderer.emitting = true;
    }

    /// Stops the trail from creating new sections.
    /// Existing sections will still fade away normally.
    public void DisableTrail()
    {
        trailRenderer.emitting = false;
    }

    /// <summary>
    /// Immediately removes every visible section of the trail.
    /// </summary>
    public void ClearTrail()
    {
        trailRenderer.Clear();
    }

    /// Clears leftover trail sections when the ball is disabled.
    private void OnDisable()
    {
        if (trailRenderer != null)
        {
            ClearTrail();
        }
    }

    /// Destroys the material created specifically for this ball.
    private void OnDestroy()
    {
        if (trailMaterial != null)
        {
            Destroy(trailMaterial);
        }
    }
}

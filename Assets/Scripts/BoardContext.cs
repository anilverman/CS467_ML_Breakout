using UnityEngine;

/// <summary>
/// Keeps gameplay references scoped to one Breakout board.
/// This prevents side-by-side boards from accidentally controlling or scoring
/// against objects belonging to the other board.
/// </summary>
public class BoardContext : MonoBehaviour
{
    public BallScript ball;
    public BrickSpawnScript brickSpawner;
    public ScoreScript score;
    public PaddleAgent agent;
    public RewardScript rewards;
}

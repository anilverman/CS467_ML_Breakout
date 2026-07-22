using UnityEngine;


/// <summary>
/// To best observer seperation of concerns, there is a dedicated reward script.
/// This allows for the modification of the reward parameters in one central location.
/// Gameplay events call the methods in this script applying the appropriate reward for the behavior.
/// </summary>
public class RewardScript : MonoBehaviour
{

    // variables
    private float BrickReward = 1f;
    private float PaddleReward = 0.1f;
    private float BallLost = -1f;
    private float Alive Reward = 0.001f;


    void Start()
    {
        // create reference to PaddleAgent to access AddReward function
        agent = FindFirstObjectByType<PaddleAgent>(); 
    }

    /// <summary>
    /// Large rewards for agent breaking bricks
    /// </summary>
    public void BrickBrokenReward()
    {
        agent.AddReward(BrickReward);

        Debug.Log("Reward: Brick was broken +1");
    }

    /// <summary>
    /// Small rewards for agent intercepting ball with paddle
    /// </summary>
    public void PaddleBlockReward()
    {
        agent.AddReward(0.1f);

        Debug.Log("Reward: Paddle blocked ball +0.1");
    }

    /// <summary>
    /// Penalty for for agent missing the ball with the paddle
    /// </summary>
    public void LostBallPenalty()
    {
        agent.AddReward(-1f);

        Debug.Log("Penalty: Missed ball -1");
    }

    /// <summary>
    /// Gives agent incredibly small reward for continuing to stay alive and play
    /// </summary>
    public void AliveReward()
    {
        agent.AddReward(.001f);

        Debug.Log("Reward: Continued to stay alive +.001");
    }


    // The following functions below have been initialized but not incorporated yet.
    // The reasoning is that we are still discussing how to proceed after level end and discussing modes.
    // These functions are merely here so that when these features are discussed and implemented, it is easy immediately to set up reward system for it. 

    /// <summary>
    /// Reinforce a larger bonus when all bricks are destroyed and level is cleared.
    /// </summary>
    public void LevelCompleteReward()
    {
        agent.AddReward(3f);

        Debug.Log("Reward: Level cleared +3");
    }

    /// <summary>
    /// Reinforce behaviors to hit ball so that it breaks multiple bricks in quick succession.
    /// </summary>
    public void BrickBreakComboReward()
    {
        agent.AddReward(2f);

        Debug.Log("Reward: Broke multiple bricks with one deflection +2");
    }

    /// <summary>
    /// Reinforce behavior to limit jittering of paddle by agent so it learns to move paddle more smoothly.
    /// </summary>
    public void StableMovementReward()
    {
        agent.AddReward(0.001f);

        Debug.Log("Reward: Smooth paddle movement +.001");
    }

}
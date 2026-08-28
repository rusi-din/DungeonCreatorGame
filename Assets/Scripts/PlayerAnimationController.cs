using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
   private Animator _animator;

    // State hashes
    private static readonly int IdleState = Animator.StringToHash("Idle");
    private static readonly int WalkingState = Animator.StringToHash("Walking");
    private static readonly int RunningState = Animator.StringToHash("Running");



    // Track the currently active state hash
    private int _currentStateHash;

    public static PlayerAnimationController Instance { get; private set; }

    void Awake()
    {
        Instance = this;    
        _animator = GetComponent<Animator>();
        // Start in Idle
        PlayState(IdleState);
    }

    public void runWalking()
    {
        PlayState(WalkingState);
    }

    public void runRunning()
    {
        PlayState(RunningState);
    }

    // Press 'Space' to stop whatever is playing and return to Idle
    public void stopAndReturnToIdle()
    {
        StopAndReturnToIdle();
    }

    /// <summary>
    /// Switches to a new state and stays in it until another command is sent.
    /// </summary>
    public void PlayState(int newStateHash, float transitionDuration = 0.2f)
    {
        // Don't re-trigger if already in this state
        if (_currentStateHash == newStateHash) return;

        _currentStateHash = newStateHash;
        _animator.CrossFade(_currentStateHash, transitionDuration);
    }

    /// <summary>
    /// Explicitly commands the animator to return to Idle.
    /// </summary>
    public void StopAndReturnToIdle()
    {
        PlayState(IdleState);
    }
}

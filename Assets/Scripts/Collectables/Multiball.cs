using System.Linq;

public class Multiball : Collectable
{
    protected override void ApplyEffect()
    {
        foreach (Ball ball in BallManager.Instance.Balls.ToList())
        {
            BallManager.Instance.SpawnBalls(ball.gameObject.transform.position, 1, ball.isLightningBall);
        }
    }
}
public class LightningBall : Collectable
{
    protected override void ApplyEffect()
    {
        foreach (var ball in BallManager.Instance.Balls)
        {
            ball.StartLightningBall();
        }
    }
}

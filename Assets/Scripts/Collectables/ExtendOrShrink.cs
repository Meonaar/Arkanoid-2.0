﻿public class ExtendOrShrink : Collectable
{
    public float newWidth = 2.5f;
    protected override void ApplyEffect()
    {
        if (Paddle.Instance !=null && !Paddle.Instance.paddleIsTransforming)
        {
            Paddle.Instance.startWithAnimation(newWidth);
        }
    }
}

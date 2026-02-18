using UnityEngine;
using Brawler.Core;

public enum ParticleTypes
{
    Jump,
    Hit,
    Magic,
}

public class ParticlePooler : Pooler<ParticleTypes>
{
    public override void Awake()
    {
        base.Awake();
        ServiceLocator.Register(this);
    }
}

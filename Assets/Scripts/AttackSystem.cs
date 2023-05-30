using UnityEngine;

public class EntityComponent : MonoBehaviour
{
    protected virtual int statesCount => 1;
    protected Animator _animator;
    public virtual void Init(Animator animator, params string[] motionKeys)
    {
        _animator = animator;

        if (motionKeys.Length != statesCount)
            throw new System.InvalidOperationException();
    }
}

public class AttackSystem : EntityComponent
{
    protected string _attackKey;
    public override void Init(Animator animator, params string[] motionKeys)
    {
        base.Init(animator, motionKeys);
        _attackKey = motionKeys[0];
    }

    public void Attack()
    {
        _animator.SetTrigger(_attackKey);
    }
}

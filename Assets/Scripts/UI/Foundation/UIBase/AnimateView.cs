using System.Collections;
using UnityEngine;

namespace CUI
{
	public abstract class AnimateView : BaseView 
    {
        protected Animator _animator;

	    protected override void Awake()
	    {
            base.Awake();

	        _animator = GetComponent<Animator>();
	        _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
	    }

        public override IEnumerator _OnEnter(UIType uiType)
        {
            _animator.Play(AnimatorStateConfig.ON_ENTER);
            yield return base._OnEnter(uiType);
        }

        public override IEnumerator _OnExit(UIType uiType)
        {
            _animator.Play(AnimatorStateConfig.ON_EXIT);
            yield return base._OnExit(uiType);
        }

        public override IEnumerator _OnPause(UIType uiType)
        {
            _animator.Play(AnimatorStateConfig.ON_PASUE);
            yield return base._OnPause(uiType);
        }

        public override IEnumerator _OnResume(UIType uiType)
        {
            _animator.Play(AnimatorStateConfig.ON_RESUME);
            yield return base._OnResume(uiType);
        }
    }
}

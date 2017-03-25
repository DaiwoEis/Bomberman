using System.Collections;
using UnityEngine;

namespace CUI
{
	public class AnimateView : BaseView 
    {
        protected Animator _animator;

	    protected override void Awake()
	    {
            base.Awake();

	        _animator = GetComponent<Animator>();
	        _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
	    }

        public override IEnumerator _OnEnter()
        {
            _animator.Play(AnimatorStateConfig.ON_ENTER);
            yield return base._OnEnter();
        }

        public override IEnumerator _OnExit()
        {
            _animator.Play(AnimatorStateConfig.ON_EXIT);
            yield return base._OnExit();
        }

        public override IEnumerator _OnPause()
        {
            _animator.Play(AnimatorStateConfig.ON_PASUE);
            yield return base._OnPause();
        }

        public override IEnumerator _OnResume()
        {
            _animator.Play(AnimatorStateConfig.ON_RESUME);
            yield return base._OnResume();
        }
    }
}

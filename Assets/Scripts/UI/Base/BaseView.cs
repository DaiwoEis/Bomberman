using System;
using System.Collections;
using UnityEngine;

namespace CUI
{
	public abstract class BaseView : MonoBehaviour
	{
	    private int n;

        [SerializeField]
	    protected float _openTime = 0.5f;

        [SerializeField]
	    protected float _closeTime = 0.5f;

        [SerializeField]
	    protected float _resumeTime = 0.5f;

        [SerializeField]
	    protected float _pauseTime = 0.5f;

        [SerializeField]
	    private bool _ifQuitButtonExitView = true;

	    public event Action onEnter = null;
	    public event Action onExit = null;
	    public event Action onPaused = null;
	    public event Action onResumed = null;

	    protected virtual void Awake()
	    {
	        
	    }

        protected virtual void Start()
        {
            
        }

	    protected void Update()
	    {
	        
	    }

	    public virtual void OnUpdate()
	    {
            if (n < 5)
            {
                //Debug.Log(string.Format("{0} Update", uiType.Name));
                n++;
            }

	        if (_ifQuitButtonExitView && Input.GetKeyDown(KeyCode.Escape))
	        {
	            Singleton<ViewController>.instance.AddCommond(new CloseCommond());
	        }
	    }

	    public virtual IEnumerator _OnEnter()
	    {
	        if (onEnter != null) onEnter();

            n = 0;
            //Debug.Log(string.Format("{0} Enter", gameObject.name));
	        yield return new WaitForSecondsRealtime(_openTime);
	    }

        public virtual IEnumerator _OnPause()
        {           
            n = 0;
            //Debug.Log(string.Format("{0} Pause", gameObject.name));
            yield return new WaitForSecondsRealtime(_pauseTime);

            if (onPaused != null) onPaused();
        }

        public virtual IEnumerator _OnResume()
	    {
            if (onResumed != null) onResumed();

            //Debug.Log(string.Format("{0} Resume", gameObject.name));
            yield return new WaitForSecondsRealtime(_resumeTime);	        
	    }

        public virtual IEnumerator _OnExit()
	    {
            //Debug.Log(string.Format("{0} Exit", gameObject.name));
            yield return new WaitForSecondsRealtime(_closeTime);

	        if (onExit != null) onExit();
	    }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
	}
}

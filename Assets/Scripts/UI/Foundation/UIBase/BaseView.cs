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

	    protected virtual void Awake()
	    {
	        
	    }

        protected virtual void Start()
        {
            
        }

	    protected void Update()
	    {
	        
	    }

	    public virtual void OnUpdate(UIType uiType)
	    {
            if (n < 5)
            {
                //Debug.Log(string.Format("{0} Update", uiType.Name));
                n++;
            }
        }

	    public virtual IEnumerator _OnEnter(UIType uiType)
	    {
            n = 0;
            //Debug.Log(string.Format("{0} Enter", uiType.Name));
	        yield return new WaitForSecondsRealtime(_openTime);
	    }

        public virtual IEnumerator _OnPause(UIType uiType)
	    {
            n = 0;
            //Debug.Log(string.Format("{0} Pause", uiType.Name));
            yield return new WaitForSecondsRealtime(_pauseTime);
        }

        public virtual IEnumerator _OnResume(UIType uiType)
	    {
            //Debug.Log(string.Format("{0} Resume", uiType.Name));
            yield return new WaitForSecondsRealtime(_resumeTime);
        }

        public virtual IEnumerator _OnExit(UIType uiType)
	    {
            //Debug.Log(string.Format("{0} Exit", uiType.Name));
            yield return new WaitForSecondsRealtime(_closeTime);
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using CUI;
using UnityEngine;

public abstract class UICommond
{
    public abstract IEnumerator Execute(Stack<BaseView> viewStack);
}

public class OpenCommond : UICommond
{
    private BaseView _nextView = null;

    public OpenCommond(BaseView nextView)
    {
        _nextView = nextView;
    }

    public override IEnumerator Execute(Stack<BaseView> viewStack)
    {
        if (_nextView == null)
        {
            Debug.LogWarning("uitype is not use");
            yield break;
        }

        viewStack.Push(_nextView);
        yield return CoroutineUtility.UStartCoroutine(_nextView._OnEnter());
    }
}

public class CloseAllCommond : UICommond
{
    private Action _onClosed = null;

    public CloseAllCommond(Action onClosed = null)
    {
        _onClosed = onClosed;
    }

    public override IEnumerator Execute(Stack<BaseView> viewStack)
    {
        if (viewStack.Count != 0)
        {
            BaseView curView = viewStack.Peek();
            yield return CoroutineUtility.UStartCoroutine(curView._OnExit());
        }
        if (_onClosed != null) _onClosed();
        viewStack.Clear();
    }
}

public class CloseCommond : UICommond
{
    private Action _onClosed = null;

    public CloseCommond(Action onClosed = null)
    {
        _onClosed = onClosed;
    }

    public override IEnumerator Execute(Stack<BaseView> viewStack)
    {
        if (viewStack.Count != 0)
        {
            BaseView curView = viewStack.Peek();
            yield return CoroutineUtility.UStartCoroutine(curView._OnExit());
            viewStack.Pop();
        }
        if (_onClosed != null) _onClosed();

        if (viewStack.Count != 0)
        {
            BaseView lastView = viewStack.Peek();
            yield return CoroutineUtility.UStartCoroutine(lastView._OnResume());
        }
    }
}

public class PauseCommond : UICommond
{
    public override IEnumerator Execute(Stack<BaseView> viewStack)
    {
        if (viewStack.Count != 0)
        {
            BaseView curView = viewStack.Peek();
            yield return CoroutineUtility.UStartCoroutine(curView._OnPause());
        }
    }
}

public class ViewController : Singleton<ViewController>
{
    private Stack<BaseView> _uiStack = new Stack<BaseView>();

    private Queue<UICommond> _uiCommonds = new Queue<UICommond>();

    private ViewController() { }

    public override void Init()
    {
        base.Init();

        CoroutineUtility.UStartCoroutine(_CheckCommond());
    }

    public void AddCommond(UICommond commond)
    {
        _uiCommonds.Enqueue(commond);
    }

    public BaseView GetCurrentView()
    {
        return _uiStack.Count != 0 ? _uiStack.Peek() : null;
    }

    private IEnumerator _CheckCommond()
    {
        while (true)
        {
            if (_uiCommonds.Count > 0)
            {
                UICommond commond = _uiCommonds.Dequeue();
                yield return
                    CoroutineUtility.UStartCoroutine(commond.Execute(_uiStack));
            }
            else
            {
                UpdateView();
                yield return null;
            }
        }
        // ReSharper disable once IteratorNeverReturns
    }

    private void UpdateView()
    {
        if (_uiStack.Count != 0)
        {
            _uiStack.Peek().OnUpdate();
        }
    }
}
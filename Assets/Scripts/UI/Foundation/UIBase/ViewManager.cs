using System;
using System.Collections;
using System.Collections.Generic;

namespace CUI
{
    public abstract class UICommond
    {
        public abstract IEnumerator Execute(Stack<UIType> contextStack, UIManager uiManager);
    }

    public class OpenCommond : UICommond
    {
        private UIType _nextContext = null;

        public OpenCommond(UIType nextComtext)
        {
            _nextContext = nextComtext;
        }

        public override IEnumerator Execute(Stack<UIType> contextStack, UIManager uiManager)
        {
            contextStack.Push(_nextContext);
            BaseView nextView = uiManager.GetView(_nextContext);
            yield return CoroutineUtility.UStartCoroutine(nextView._OnEnter(_nextContext));
        }
    }

    public class CloseAllCommond : UICommond
    {
        private Action _onClosed = null;

        public CloseAllCommond(Action onClosed = null)
        {
            _onClosed = onClosed;
        }

        public override IEnumerator Execute(Stack<UIType> contextStack, UIManager uiManager)
        {
            if (contextStack.Count != 0)
            {
                UIType curContext = contextStack.Peek();
                BaseView curView = uiManager.GetView(curContext);
                yield return CoroutineUtility.UStartCoroutine(curView._OnExit(curContext));
            }
            if (_onClosed != null) _onClosed();
            contextStack.Clear();
        }
    }

    public class CloseCommond : UICommond
    {
        private Action _onClosed = null;

        public CloseCommond(Action onClosed = null)
        {
            _onClosed = onClosed;
        }

        public override IEnumerator Execute(Stack<UIType> contextStack, UIManager uiManager)
        {
            if (contextStack.Count != 0)
            {
                UIType curContext = contextStack.Peek();
                BaseView curView = uiManager.GetView(curContext);
                yield return CoroutineUtility.UStartCoroutine(curView._OnExit(curContext));                
                contextStack.Pop();
            }
            if (_onClosed != null) _onClosed();

            if (contextStack.Count != 0)
            {
                UIType lastContext = contextStack.Peek();
                BaseView lastView = uiManager.GetView(lastContext);
                yield return CoroutineUtility.UStartCoroutine(lastView._OnResume(lastContext));
            }
        }
    }

    public class PauseCommond : UICommond
    {
        public override IEnumerator Execute(Stack<UIType> contextStack, UIManager uiManager)
        {
            if (contextStack.Count != 0)
            {
                UIType curContext = contextStack.Peek();
                BaseView curView = uiManager.GetView(curContext);
                yield return CoroutineUtility.UStartCoroutine(curView._OnPause(curContext));
            }
        }
    }

    public class ViewManager
    {
        private Stack<UIType> _uiStack = new Stack<UIType>();

        private UIManager _uiManager;

        private Queue<UICommond> _uiCommonds = new Queue<UICommond>();

        private ViewManager()
        {
            _uiManager = new UIManager();

            CoroutineUtility.UStartCoroutine(_CheckCommond());
        }

        public void AddCommond(UICommond commond)
        {
            _uiCommonds.Enqueue(commond);
        }

        public BaseView GetCurrentView()
        {
            if (_uiStack.Count != 0)
            {
                UIType currContext = _uiStack.Peek();
                return _uiManager.GetView(currContext);
            }
            return null;
        }

        private IEnumerator _CheckCommond()
        {
            while (true)
            {
                if (_uiCommonds.Count > 0)
                {
                    UICommond commond = _uiCommonds.Dequeue();
                    yield return
                        CoroutineUtility.UStartCoroutine(commond.Execute(_uiStack, _uiManager));
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
                UIType curContext = _uiStack.Peek();
                BaseView curView = _uiManager.GetView(curContext);
                curView.OnUpdate(curContext);
            }
        }

        public UIType CurrentView()
        {
            return _uiStack.Count != 0 ? _uiStack.Peek() : null;
        }
    }
}

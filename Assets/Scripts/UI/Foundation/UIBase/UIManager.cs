using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace CUI
{
    public class UIManager
    { 
        private Dictionary<UIType, BaseView> _UIViewDict = new Dictionary<UIType, BaseView>();

        private Transform _canvas;

        public UIManager()
        {
            _canvas = GameObject.Find("UICanvas").transform;
            GameObject go = new GameObject("EventSystem");
            go.AddComponent<EventSystem>();
            go.AddComponent<StandaloneInputModule>();
            go.transform.parent = _canvas;
        }

        public BaseView GetView(UIType uiType)
        {
            BaseView result;
            if (!_UIViewDict.TryGetValue(uiType, out result))
            {
                GameObject go = FindViewInChild(uiType) ?? CreateView(uiType);
                go.SetActive(true);
                result = go.GetComponent<BaseView>();
                _UIViewDict[uiType] = result;
            }
            return result;
        }

        private GameObject FindViewInChild(UIType uiType)
        {
            //StringBuilder stringBuilder = new StringBuilder();
            //stringBuilder.Append(_canvas.name);
            //stringBuilder.Append("/");
            //stringBuilder.Append(uiType.Name);
            //Debug.Log(stringBuilder.ToString());
            //return GameObject.Find(stringBuilder.ToString());
            return _canvas.Find(uiType.Name).gameObject;
        }

        private GameObject CreateView(UIType uiType)
        {
            GameObject go = Object.Instantiate(Resources.Load<GameObject>(uiType.Path));
            go.transform.SetParent(_canvas, false);
            go.name = uiType.Name;
            return go;
        }

        public void DestroyView(UIType uiType)
        {
            BaseView destroyView;
            if (_UIViewDict.TryGetValue(uiType, out destroyView))
            {
                if (destroyView != null)
                {
                    Object.Destroy(destroyView.gameObject);
                }
                _UIViewDict.Remove(uiType);
            }
        }
	}
}

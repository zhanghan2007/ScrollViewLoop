using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UI
{
    public abstract class LoopScrollRect : LoopScrollRectBase, LoopScrollPrefabSource
    {
        [HideInInspector]
        [NonSerialized]
        public LoopScrollDataSource dataSource = null;
        
        protected override void ProvideData(Transform transform, int index)
        {
            dataSource.ProvideData(transform, index);
        }
        
        protected override RectTransform GetFromTempPool(int itemIdx)
        {
            RectTransform nextItem = null;
            if (deletedItemTypeStart > 0)
            {
                deletedItemTypeStart--;
                nextItem = m_Content.GetChild(0) as RectTransform;
                nextItem.SetSiblingIndex(itemIdx - itemTypeStart + deletedItemTypeStart);
            }
            else if (deletedItemTypeEnd > 0)
            {
                deletedItemTypeEnd--;
                nextItem = m_Content.GetChild(m_Content.childCount - 1) as RectTransform;
                nextItem.SetSiblingIndex(itemIdx - itemTypeStart + deletedItemTypeStart);
            }
            else
            {
                nextItem = prefabSource.GetObject(itemIdx).transform as RectTransform;
                nextItem.transform.SetParent(m_Content, false);
                nextItem.gameObject.SetActive(true);
            }
            ProvideData(nextItem, itemIdx);
            return nextItem;
        }

        protected override void ReturnToTempPool(bool fromStart, int count)
        {
            if (fromStart)
                deletedItemTypeStart += count;
            else
                deletedItemTypeEnd += count;
        }

        protected override void ClearTempPool()
        {
            Debug.Assert(m_Content.childCount >= deletedItemTypeStart + deletedItemTypeEnd);
            if (deletedItemTypeStart > 0)
            {
                for (int i = deletedItemTypeStart - 1; i >= 0; i--)
                {
                    prefabSource.ReturnObject(m_Content.GetChild(i));
                }
                deletedItemTypeStart = 0;
            }
            if (deletedItemTypeEnd > 0)
            {
                int t = m_Content.childCount - deletedItemTypeEnd;
                for (int i = m_Content.childCount - 1; i >= t; i--)
                {
                    prefabSource.ReturnObject(m_Content.GetChild(i));
                }
                deletedItemTypeEnd = 0;
            }
        }
        
        //==========DefaultLoopScrollPrefabSource==========
        //==========默认的对象池==========
        //如果有自己的资源加载方式，可以重新赋值prefabSource
        public GameObject m_ItemPrefab;
        Stack<Transform> pool = new Stack<Transform>();
        public GameObject GetObject(int index)
        {
            if (pool.Count == 0)
            {
                return Instantiate(m_ItemPrefab);
            }
            var candidate = pool.Pop();
            GameObject o;
            (o = candidate.gameObject).SetActive(true);
            return o;
        }

        public void ReturnObject(Transform trans)
        {
            trans.gameObject.SetActive(false);
            trans.SetParent(transform, false);
            pool.Push(trans);
        }

        protected override void Awake()
        {
            base.Awake();
            prefabSource = this; 
        }
        //==========DefaultLoopScrollDataSource==========
    }
}
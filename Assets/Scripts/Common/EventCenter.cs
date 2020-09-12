using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventCenter
{
    private static Dictionary<EventType, Delegate> m_EventTable = new Dictionary<EventType, Delegate>();
    //添加无参的事件监听
    public static void AddListener(EventType eventType, CallBack callBack)
    {
        if (!m_EventTable.ContainsKey(eventType))
        {
            m_EventTable.Add(eventType, null);
        }
        Delegate d = m_EventTable[eventType];
        if (d != null && d.GetType() != callBack.GetType())
        {
            throw new Exception(string.Format("尝试为事件{0}添加不同类型的委托，当前事件所对应的委托类型为{1}，要添加的委托类型为{2}", eventType, d.GetType(), callBack.GetType()));
        }
        m_EventTable[eventType] = (CallBack)m_EventTable[eventType] + callBack;
    }
    //移除无参的事件监听
    public static void RemoveListener(EventType eventType, CallBack callBack)
    {
        if (m_EventTable.ContainsKey(eventType))
        {
            Delegate d = m_EventTable[eventType];
            if (d == null)
            {
                throw new Exception(string.Format("移除监听错误：事件{0}没有对应的委托", eventType));
            }
            else if (d.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("移除监听错误：尝试为事件{0}移除不同类型的委托，当前事件所对应的委托类型为{1}，要移除的委托类型为{2}", eventType, d.GetType(), callBack.GetType()));
            }
        }
        else
        {
            throw new Exception(string.Format("移除监听错误：没有事件码{0}", eventType));
        }
        m_EventTable[eventType] = (CallBack)m_EventTable[eventType] - callBack;
        if (m_EventTable[eventType]==null)
        {
            m_EventTable.Remove(eventType);
        }
    }
    //广播无参事件
    public static void Broadcast(EventType eventType)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType,out d))
        {
            CallBack callBack = d as CallBack;
            if (callBack != null)
            {
                callBack();
            }
            else
            {
                throw new Exception(string.Format("广播事件错误：事件{0}对应的委托具有不同的类型", eventType));
            }
        }
    }
}
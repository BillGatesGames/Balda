using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Balda
{
    public static class EventBus
    {
        private static Dictionary<Type, List<IHandler>> _handlers = new Dictionary<Type, List<IHandler>>();

        public static void Clear()
        {
            _handlers.Clear();
        }

        public static void Register(IHandler handler)
        {
            var types = GetHandlersTypes(handler);

            foreach (var type in types)
            {
                if (!_handlers.ContainsKey(type))
                {
                    _handlers.Add(type, new List<IHandler>());
                }

                _handlers[type].Add(handler);
            }
        }

        public static void Unregister(IHandler handler)
        {
            var types = GetHandlersTypes(handler);

            foreach (var type in types)
            {
                if (_handlers.ContainsKey(type))
                {
                    _handlers[type].Remove(handler);
                }
            }
        }

        public static void RaiseEvent<THandler>(Action<THandler> action) where THandler : IHandler
        {
            var type = typeof(THandler);

            if (!_handlers.ContainsKey(type))
            {
                Debug.LogError($"Handlers of {type} not found");
                return;
            }

            var handlers = _handlers[type];

            foreach (IHandler handler in handlers)
            {
                try
                {
                    action.Invoke((THandler)handler);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }

        private static List<Type> GetHandlersTypes(IHandler handler)
        {
            return handler.GetType().GetInterfaces().ToList();
        }
    }
}

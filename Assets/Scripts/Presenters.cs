using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/*
public static class Presenters
{
    private static Dictionary<Type, List<object>> _presenters = new();
    public static void Add<T>(T presenter) where T : IPresenter
    {
        if (!_presenters.ContainsKey(typeof(T)))
        {
            _presenters.Add(typeof(T), new List<object>());
        }

        var list = _presenters[typeof(T)];

        if (!list.Contains(presenter))
        {
            list.Add(presenter);
        }
    }

    public static List<T> Get<T>() where T : IPresenter
    {
        if (!_presenters.ContainsKey(typeof(T)))
        {
            return new List<T>();
        }

        return _presenters[typeof(T)].Select(p => (T)p).ToList();
    }
    
    public static T GetFirst<T>() where T : IPresenter
    {
        return Get<T>().First();
    }
}
*/
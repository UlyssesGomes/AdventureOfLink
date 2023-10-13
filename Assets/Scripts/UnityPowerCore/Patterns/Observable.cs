using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observable<T>
{
    private List<Observer<T>> observersList;

    public Observable()
    {
        observersList = new List<Observer<T>>();
    }

    /// <summary>
    /// Add observers to receive subject notification.
    /// </summary>
    /// <param name="observer"></param>
    public void addObservers(Observer<T> observer)
    {
        observersList.Add(observer);
        observer.setObservable(this);
    }

    /// <summary>
    /// Remove an observer from the notification list
    /// </summary>
    /// <param name="observer"></param>
    /// <returns></returns>
    public bool removeObserver(Observer<T> observer)
    {
        return observersList.Remove(observer);
    }

    /// <summary>
    /// Notify all observers with the new subjects
    /// </summary>
    public void notify(T subject)
    {
        foreach(Observer<T> o in observersList)
        {
            o.update(subject);
        }
    }
}

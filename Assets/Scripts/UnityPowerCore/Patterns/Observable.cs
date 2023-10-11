using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Observable<T>
{
    void addObservers(Observer<T> observer);

    void removeObserver(Observer<T> observer);
}

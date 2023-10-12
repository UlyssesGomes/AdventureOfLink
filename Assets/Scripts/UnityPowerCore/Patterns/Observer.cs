using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Observer<T>
{
   /// <summary>
   /// Update observer with new subject.
   /// </summary>
   /// <param name="subjectEvent"></param>
   void update(T subjectEvent);

   /// <summary>
   /// Set its parent Observable.
   /// </summary>
   /// <param name="observable"></param>
   void setObservable(Observable<T> observable);
}

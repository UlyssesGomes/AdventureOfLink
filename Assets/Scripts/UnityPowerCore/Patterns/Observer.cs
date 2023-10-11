using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Observer<T>
{
   void update(T subjectEvent);

   void setObservable(Observable<T> observable);
}

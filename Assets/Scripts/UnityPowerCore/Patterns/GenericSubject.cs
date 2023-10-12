using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSubject<T, S>
{
    public T type;          // type of subject
    public S subject;       // subject for notification
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbstractState
{
    public abstract void EnterState(EnemyController enemy);

    public abstract void Update(EnemyController enemy);

    public abstract void OnTriggerEnter2D(Collider2D other, EnemyController enemy);
}

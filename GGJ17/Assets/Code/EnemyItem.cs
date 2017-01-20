using UnityEngine;
using System.Collections;

public class EnemyItem {

    public Enemy enemy;
    public int id;

    public EnemyItem(Enemy _enemy, int _id)
    {
        enemy = _enemy;
        id = _id;
    }
    //A = 0
    //B = 1
    //X = 2
    //Y = 3
}

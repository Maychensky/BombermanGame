using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Bomberman.Enemy
{  

    internal enum TypeEnemy { Balloom, Oneal, Doll, Minyo, Kondoria, Ovapi, Pass, Pontan } 

    internal enum IntensityScale { VaryWeak, Weak, Medium, Hard }

    /* характеристики
    имя
    очков за убийство (100/200/400/800/1000/2000/4000/8000)
    скорость (оч.медленно/медлено/средне/быстро)
    настырность (низко/средне/высоко) 
    (bool) проход свозь стены (да/нет)
    */

    internal abstract class Enemy
    {
        
        /* харакатеристики */
        TypeEnemy _nameEnemy;
        int _pointsForKills;
        IntensityScale _speedMovement;
        IntensityScale _levelAgression; 
        bool _isPassesThroghWalls;
        
        static Dictionary <TypeEnemy, Sprite> _dictionarySprites;
        GameObject _objectEnemy;
        Vector3 _staetWorldPosition;
        internal Enemy( TypeEnemy nameEnemy, int pointsForKills, IntensityScale speedMovement, IntensityScale levelAgression, bool isPassesThroghWalls)
        {
            _nameEnemy = nameEnemy;
            _pointsForKills = pointsForKills;
            _speedMovement = speedMovement;
            _levelAgression = levelAgression; 
            _isPassesThroghWalls = isPassesThroghWalls;
            CheckFullInitializationSprites();
        }

        internal static void SetSprites(Sprite sprite, TypeEnemy typeEnemy) => _dictionarySprites[typeEnemy] = sprite;
        protected abstract void PathAlgotithm();

        void CheckFullInitializationSprites()
        {
            foreach (var typeEnemy in Enum.GetValues(typeof(TypeEnemy)).Cast<TypeEnemy>())
                if (!_dictionarySprites.ContainsKey(typeEnemy))
                    throw new Exception("не все спрайты инициализированы");
        }

    }
}
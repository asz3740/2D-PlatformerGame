﻿namespace Assets.FantasyMonsters.Scripts
{
    /// <summary>
    /// Monster state. The same parameter controls animation transition.
    /// </summary>
    public enum MonsterState
    {
        Idle 	= 0,
        Ready 	= 1,
        Walk 	= 2,
        Run 	= 3,
        Jump 	= 4,
        Climb 	= 5,
        Death   = 9
    }
}
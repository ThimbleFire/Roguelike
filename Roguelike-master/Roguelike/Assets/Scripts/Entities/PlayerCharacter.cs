﻿using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Entity
{
    private void Start()
    {
        Name = "Player Chacter";
        Speed = 4;
    }

    public override void Action( Vector3Int playerCharacterCoordinates )
    {
        chain = Pathfind.GetPath( coordinates, TileMapCursor.SelectedTileCoordinates, false );
        TileMapCursor.Hide();
        HUDControls.Hide();
        base.Action( playerCharacterCoordinates );
    }

    protected override void OnStep()
    {
        base.OnStep();
    }

    protected override void OnArrival()
    {
        base.OnArrival();
    }
}
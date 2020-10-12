using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BoardView: BoardElement
{
    public void animateCellToNewPosition(BoardTile cell)
    {
        Vector3 current = cell.tile.transform.localPosition;
        
    }
}

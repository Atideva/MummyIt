using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnPositions : MonoBehaviour
{
    public List<SpawnLine> lines = new();
    int _current;
   
    //  3  1  0  2  4
    public Vector3 GetRandom()
    {
        int r = Random.Range(0, lines.Count);
        int k = Random.Range(0, lines[r].Positions.Length);
        return lines[r].Positions[k].position;
    }
    
    public List<Vector3> Get(int count)
    {
        var result = new List<Vector3>();
        
        switch (count)
        {
            case 1:
            {
                var r = Random.Range(0, 2);
                var pos = lines[_current].Positions[r];
                result.Add(pos.position);
                break;
            }
            case 2:
            {
                var pos1 = lines[_current].Positions[1];
                var pos2 = lines[_current].Positions[2];
                result.Add(pos1.position); 
                result.Add(pos2.position);
                break;
            }
            case 3:
            {
                var pos1 = lines[_current].Positions[1];
                var pos2 = lines[_current].Positions[0];
                var pos3 = lines[_current].Positions[2];
                result.Add(pos1.position); 
                result.Add(pos2.position); 
                result.Add(pos3.position);
                break;
            }
            case 4:
            {
                var pos1 = lines[_current].Positions[3];
                var pos2 = lines[_current].Positions[1];
                var pos3 = lines[_current].Positions[2];
                var pos4 = lines[_current].Positions[4];
                result.Add(pos1.position); 
                result.Add(pos2.position); 
                result.Add(pos3.position); 
                result.Add(pos4.position);
                break;
            }
            case 5:
                result.AddRange(lines[_current].Positions.Select(pos => pos.position));
                break;
        }

        _current++;
        if (_current >= lines.Count) _current = 0;

        return result;
    }
    
}
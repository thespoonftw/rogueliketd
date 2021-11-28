using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureUpdateManager : Singleton<StructureUpdateManager>
{
    private List<Tower> towers = new List<Tower>();

    public void AddTower(Tower tower)
    {
        towers.Add(tower);
    }

    private void FixedUpdate()
    {
        towers.ForEach(t => t.Tick(Time.fixedDeltaTime));
    }

}

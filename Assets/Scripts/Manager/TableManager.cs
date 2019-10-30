using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoSingleton<TableManager>
{
    public Table[] tables;

    public Vector3[,] route = new Vector3[3,3];

    void Awake()
    {
        int a = 8;
        //테이블로 가는 위치 route 포지션을 받아옴
        for (int i = 2; i >= 0; i--)
        {
            for (int j = 2; j >= 0; j--)
            {
                    route[i,j] = transform.GetChild(1).GetChild(a).transform.position;
                    a--;
            }
        }
    }

    public void GetTablePos(int tableIndex , Guest guest)
    {
        switch (tableIndex)
        {
            
            case 0:
            guest.Co_MoveToTable(route[0,0] , route[1,0] , tables[0].seat.position , tableIndex);
            break;
            
            case 1:
            guest.Co_MoveToTable(route[0,0] , route[2,0] , tables[1].seat.position , tableIndex);
            break;
            
            case 2:
            guest.Co_MoveToTable(route[0,1] , route[1,1] , tables[2].seat.position , tableIndex);
            break;
            
            case 3:
            guest.Co_MoveToTable(route[0,1] , route[2,1] , tables[3].seat.position , tableIndex);
            break;
        }
    }

    public void GetStaffTablePos(int tableIndex)
    {
        Staff staff = StaffManager.Instance().staff;
        switch (tableIndex)
        {
            case 0:
            staff.Co_MoveToTable(route[0,1] , route[1,1] , tableIndex);
            break;
            
            case 1:
            staff.Co_MoveToTable(route[0,1] , route[2,1] , tableIndex);
            break;
            
            case 2:
            staff.Co_MoveToTable(route[0,1] , route[1,1] , tableIndex);
            break;
            
            case 3:
            staff.Co_MoveToTable(route[0,1] , route[2,1] , tableIndex);
            break;
        }
    }

    //빈 테이블 목록 받아옴
    public Table GetEmptyTable()
    {
        foreach (var table in tables)
        {
            if(table.IsEmpty)
            {
                return table;
            }
        }
        return null;
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_.net_.Models
{
    public class Department
    {
        public  int Id { get; set; }
        public string DepartmentName { get; set; }
        public Department(int id, string departmentName)
        {
            Id = id;
            DepartmentName = departmentName;
        }
    }
}

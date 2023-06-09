using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace webapi_01
{
    public class Response
    {
        public string? Result { get; set; }
        public string? Message { get; set; }
        public List<Employee>? Employees { get; set; }

        public List<MusicSheet>? MusicSheets { get; set; }

        public List<Tutorial>? Tutorials { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace XMR.Gallery.Model
{
    public class Picture
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public Picture(string path, string name, DateTime creationDate) 
        { 
            Path = path;
            Name = name;
            CreationDate = creationDate;    
        }
    }
}

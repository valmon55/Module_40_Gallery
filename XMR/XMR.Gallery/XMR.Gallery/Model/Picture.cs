using System;
using System.Collections.Generic;
using System.Text;

namespace XMR.Gallery.Model
{
    public class Picture
    {
        public string FullFileName { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public Picture(string fullFileName, string name, DateTime creationDate) 
        {
            FullFileName = fullFileName;
            Name = name;
            CreationDate = creationDate;    
        }
    }
}

﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace GrassHopper.Models
{
    [NotMapped]
    public class GroupVM
    {
        public GroupVM(PhotoGroup g, PhotoSize size)
        {
            GroupId = g.GroupId;
            Name = g.GroupName;
            Description = g.GroupDescription;
            foreach(Photo p in g.Photos)
            {
                Photos.Add(new(p, size));
            }
        }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PhotoVM> Photos { get; set; } = new List<PhotoVM>();
    }

    public static class GVMMaker
    {
        public static List<GroupVM> MakeGroupVM(List<PhotoGroup> groups, PhotoSize size)
        {
            List<GroupVM> groupVMs = new List<GroupVM>();
            foreach(PhotoGroup g in groups)
            {
                groupVMs.Add(new(g, size));
            }
            return groupVMs;
        }
    }
}

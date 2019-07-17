using RubiksCubeStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RubiksCubeStore.WebUI.Models
{
    public class CubesListViewModel
    {
        public IEnumerable<Cube> Cubes { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
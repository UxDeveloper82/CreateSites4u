using icreatesites4u.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icreatesites4u.api.Dtos
{
    public class PostForCreationDto
    {
        public int Id { get; set; }

        public int PostTitle { get; set; }

        public string ImageUrl { get; set; }

        public string PostDetails { get; set; }

        public DateTime Created { get; set; }

        public User Creator { get; set; }

        public int CreatorId { get; set; }
    }
}

﻿using Shortly.Data.Models;

namespace Shortly.Client.Data.ViewMoels
{
    public class GetUrlVM
    {
        public int Id { get; set; }
        public string OriginalLink { get; set; }
        public string ShortLink { get; set; }
        public int NrOfClicks { get; set; }
        public int? UserId { get; set; }
        public GetUserVM? User { get; set; }
    }
}

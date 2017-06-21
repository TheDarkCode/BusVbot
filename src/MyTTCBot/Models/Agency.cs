﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTTCBot.Models
{
    [Table("agency")]
    public class Agency
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        [Column("tag")]
        public string Tag { get; set; }

        [Required]
        [MaxLength(70)]
        [Column("title")]
        public string Title { get; set; }

        [Required]
        [MaxLength(25)]
        [Column("region")]
        public string Region { get; set; }

        [Required]
        [MaxLength(15)]
        [Column("country")]
        public string Country { get; set; }

        [MaxLength(25)]
        [Column("short_title")]
        public string ShortTitle { get; set; }

        [Required]
        [Column("lat_max")]
        public double MaxLatitude { get; set; }

        [Required]
        [Column("lat_min")]
        public double MinLatitude { get; set; }

        [Required]
        [Column("lon_max")]
        public double MaxLongitude { get; set; }

        [Required]
        [Column("lon_min")]
        public double MinLongitude { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("modified_at")]
        public DateTime? ModifiedAt { get; set; }

        public static explicit operator NextBus.NET.Models.Agency(Agency agency)
        {
            if (agency is null)
                return null;

            return new NextBus.NET.Models.Agency
            {
                Tag = agency.Tag,
                Title = agency.Title,
                ShortTitle = agency.ShortTitle,
                RegionTitle = agency.Region,
            };
        }

        public static explicit operator Agency(NextBus.NET.Models.Agency nxbAgency)
        {
            if (nxbAgency is null)
                return null;

            string country;

            switch (nxbAgency.RegionTitle.ToUpper())
            {
                case "ONTARIO":
                case "QUEBEC":
                    country = "Canada";
                    break;
                default:
                    country = "U.S.";
                    break;
            }

            return new Agency
            {
                Tag = nxbAgency.Tag,
                Title = nxbAgency.Title,
                ShortTitle = string.IsNullOrWhiteSpace(nxbAgency.ShortTitle) ? null : nxbAgency.ShortTitle,
                Region = nxbAgency.RegionTitle,
                Country = country,
            };
        }
    }
}

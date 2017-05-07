﻿using System;

namespace MyTTCBot.Models
{
    public class UserContext
    {
        public UserLocation Location { get; set; }
    }

    public class UserLocation
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public static explicit operator UserLocation(NetTelegramBotApi.Types.Location location)
        {
            return new UserLocation
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude,
            };
        }
    }

    public struct UserChat
    {
        public UserChat(long userId, long chatId)
        {
            UserId = userId;
            ChatId = chatId;
        }

        public long UserId { get; set; }

        public long ChatId { get; set; }
    }

    public enum BusDirection
    {
        North,
        East,
        South,
        West
    }
}